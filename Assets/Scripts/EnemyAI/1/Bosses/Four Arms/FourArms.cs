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
    //Arms
    private FireArm fireArm;
    private WaterArm waterArm;
    private AirArm airArm;
    private EarthArm earthArm;
    public bool targeting;
    private bool start;
    private IEnumerator Start() {
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
        arms[2] = earthArm;
        arms[3] = airArm;
    }
    private void Update() {
        if (!dead && health <= 0) {
            dead = true;
            StartCoroutine(Death());
        }

        if (start) {
            if (homingTime <= 0) {
                ShootHomingMissle();
            } else {
                homingTime = resetHomingTime;
            }
        }
    }
    public void Damage(int n) {
        health -= n;
        slider.value = health;
        upgradeManager.ChargeUpgradeBar();
    }
    public void LockArms(int n) { //0 for fire, 1 for water, 2 for air, 3 for earth
        targeting = true;
        for (int i = 0; i < 4; i++) {
            if (i != n && !arms[i].dead) {
                arms[i].invulnerable = true;
            }
        }
    }
    public void UnlockArms() {
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
    private void ShootHomingMissle() { //0 for fire, 1 for water, 2 for air, 3 for earth
        for (int i = 0; i < arms.Length; i++) {
            if (arms[i].dead) {
                Instantiate(homingBullet, armPositions[i], Quaternion.identity);
            }
        }
    }
    private IEnumerator Death() {
        Destroy(fireArm);
        Destroy(waterArm);
        Destroy(airArm);
        Destroy(earthArm);
        animator.SetTrigger("Death");
        yield return new WaitForSeconds(2f);
        Instantiate(deadFourArms, transform.position, Quaternion.identity);
    }
}