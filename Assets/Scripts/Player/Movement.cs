using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//TODO: Sprite, Animations


public class Movement : MonoBehaviour {
    private enum currState {InDialogue, Normal, Staggering}; //This is good practice I guess
    [SerializeField] private float speed;
    private Rigidbody2D rb;
    [SerializeField] private GameObject[] bulletPrefabs;
    [SerializeField] private int bulletType; //specifies which weapon to use
    [SerializeField] private float bulletSpeed; //change speed when changing weapons: Normal, Claw
    [SerializeField] private float lastFire;
    [SerializeField] private float fireDelay;
    [SerializeField] private float[] bSpeeds; //Normal and Claw
    [SerializeField] private Image interactIcon; //Image for the interactable check
    private Vector2 boxSize = new Vector2(0.1f, 1f); //Box for raycasting
    [SerializeField] private bool allowCombat;

    void Start() {
        rb = gameObject.GetComponent<Rigidbody2D>();
        ChangeWeapon(bulletType);
        interactIcon.enabled = false;
    }

    void Update() {
        //Movement
        float hori = Input.GetAxisRaw("Horizontal");
        float vert = Input.GetAxisRaw("Vertical");
        
        if (allowCombat) {
            float shootHori = Input.GetAxis("HorizontalShoot");
            float shootVert = Input.GetAxis("VerticalShoot");
            if ((shootHori != 0 || shootVert != 0) && Time.time > lastFire + fireDelay) {
                    Shoot(shootHori, shootVert);
                    lastFire = Time.time;
            }
        }

        rb.velocity = new Vector2(hori * speed, vert * speed);

        if (Input.GetKeyDown(KeyCode.E)) { //Checks when you press 'E' if you can interact with anything
            CheckInteraction();
        }
    }

    void Shoot(float x, float y) {
        GameObject bullet = Instantiate(bulletPrefabs[bulletType], transform.position, transform.rotation) as GameObject;
        bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(
            (x < 0) ? Mathf.Floor(x) * bulletSpeed : Mathf.Ceil(x) * bulletSpeed,
            (y < 0) ? Mathf.Floor(y) * bulletSpeed : Mathf.Ceil(y) * bulletSpeed);
    }

    // Changes Weapon
    public void ChangeWeapon(int num) {
        bulletType = num;
        bulletSpeed = bSpeeds[num];
    }

    //Interactions Part
    public void OpenInteractableIcon() {
        interactIcon.enabled = true;
    }

    public void CloseInteractableIcon() {
        interactIcon.enabled = false;
    }
    public void CheckInteraction() {
        RaycastHit2D[] hits = Physics2D.BoxCastAll(transform.position, boxSize, 0, Vector2.zero);

        if (hits.Length > 0) {
            foreach (RaycastHit2D rc in hits) {
                if (rc.IsInteractable()) {
                    rc.transform.GetComponent<Interactable>().Interact();
                    return;
                }
            }
        }
    }
}
