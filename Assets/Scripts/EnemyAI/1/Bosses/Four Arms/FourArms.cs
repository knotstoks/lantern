using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**
Animations Reference:
Each Arm controls 1 element and after a certain period of time,
FourArms will fire off a random elemental attack after a certain period of time.
Attacks will be decided randomly.

Instantiate the elemental attack when it triggers
**/

public class FourArms : MonoBehaviour { //0 for fire, 1 for water, 2 for air, 3 for earth
    [SerializeField] private Slider slider;
    [SerializeField] private int health;
    [SerializeField] private float resetHomingTime;
    [SerializeField] private GameObject homingBullet;
    [SerializeField] private Vector2[] armPositions;
    [SerializeField] private GameObject deadFourArms;
    [SerializeField] private GameObject puddle;
    //Four arms needed
    [SerializeField] private GameObject fire;
    [SerializeField] private GameObject water;
    [SerializeField] private GameObject air;
    [SerializeField] private GameObject earth;
    private Upgrades upgradeManager;
    private int[] attacks;
    private Arm[] arms;
    private bool dead;
    private Transform target;
    private Animator animator;
    private float homingTime;
    private FourArmsBossRoom sceneManager;
    //Arms
    private FireArm fireArm;
    private WaterArm waterArm;
    private AirArm airArm;
    private EarthArm earthArm;
    public bool targeting;
    private bool start;
    private IEnumerator Start() {
        sceneManager = GameObject.FindGameObjectWithTag("DungeonSceneManager").GetComponent<FourArmsBossRoom>();
        upgradeManager = GameObject.FindGameObjectWithTag("Upgrades").GetComponent<Upgrades>();
        targeting = false;
        dead = false;
        start = false;
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        animator = GetComponent<Animator>();
        attacks = new int[0];
        arms = new Arm[4];

        //Assigning the arms
        fireArm = fire.GetComponent<FireArm>();
        waterArm = water.GetComponent<WaterArm>();
        earthArm = earth.GetComponent<EarthArm>();
        airArm = air.GetComponent<AirArm>();

        yield return new WaitForSeconds(0.02f);

        arms[0] = fireArm;
        arms[1] = waterArm;
        arms[2] = airArm;
        arms[3] = earthArm;
    }
    private void Update() {
        if (!dead && health <= 0) {
            dead = true;
            StartCoroutine(Death());
        }

        if (start && !dead && arms[2].dead) {
            if (homingTime <= 0) {
                homingTime = resetHomingTime;
                StartCoroutine(ShootHomingMissle());
            } else {
                homingTime -= Time.deltaTime;
            }
        }
    }
    public void Damage(int n) {
        health -= n;
        slider.value = health;
        upgradeManager.ChargeUpgradeBar();
    }
    public void LockArms(int n) { //0 for fire, 1 for water, 2 for air, 3 for earth
        for (int i = 0; i < 4; i++) {
            if (i != n && !arms[i].dead) {
                arms[i].invulnerable = true;
            }
        }
    }
    public void UnlockArms() {
        targeting = false;
        for (int i = 0; i < 4; i++) {
            if (!arms[i].dead) {
                arms[i].invulnerable = false;
            }
        }
    }
    public void StartBoss() {
        start = true;
        for (int i = 0; i < 4; i++) {
            arms[i].start = true;
        }
    }
    private IEnumerator ShootHomingMissle() { //0 for fire, 1 for water, 2 for air, 3 for earth
        for (int i = 0; i < arms.Length; i++) {
            yield return new WaitForSeconds(2f);
            if (arms[i].dead) {
                Instantiate(homingBullet, armPositions[i], Quaternion.identity);
            }
        }
    }
    private IEnumerator Death() {
        GameObject[] bullets = GameObject.FindGameObjectsWithTag("Bullet");
        GameObject[] homingMissles = GameObject.FindGameObjectsWithTag("HomingMissle");
        for (int i = 0; i < bullets.Length; i ++) {
            Destroy(bullets[i]);
        }
        for (int i = 0; i < homingMissles.Length; i++) {
            Destroy(homingMissles[i]);
        }
        animator.SetTrigger("Death");
        sceneManager.CompleteFight();
        yield return new WaitForSeconds(2f);
        Destroy(fire);
        Destroy(water);
        Destroy(air);
        Destroy(earth);
        Destroy(puddle);
        Instantiate(deadFourArms, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}