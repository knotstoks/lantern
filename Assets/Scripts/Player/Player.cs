﻿using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour{
    [SerializeField] private int health;
    [SerializeField] private int maxHealth;
    [SerializeField] private Image[] hearts;
    [SerializeField] private Sprite fullHeart;
    [SerializeField] private Sprite halfHeart;
    [SerializeField] private Sprite emptyHeart;
    [SerializeField] private float resetInvulTime;
    [SerializeField] private float speed;
    [SerializeField] private GameObject bullet;
    [SerializeField] private float bulletSpeed; 
    [SerializeField] private float fireDelay;
    [SerializeField] private Image interactIcon; //Image for the interactable check
    public Image dialogueBox; //Dialogue Box for Text
    public GameObject dialogueText; //Text Object
    public bool allowCombat; //Scene will set this
    public bool inDialogue; //Scene will set this
    private float invulTime; //if its > 0, cannot be damaged. < 0 can be damaged.
    private Vector2 boxSize = new Vector2(2.5f, 2.5f); //Box for raycasting
    private Rigidbody2D rb;
    private float lastFire;
    private Vector2 move;

    void Start() {
        invulTime = 0.5f;
        rb = gameObject.GetComponent<Rigidbody2D>();
        interactIcon.enabled = false;
        dialogueBox.enabled = false;
        dialogueText.SetActive(false);
        updateHealth();
    }

    void Update() {
        if (invulTime > 0) {
            invulTime -= Time.deltaTime;
        }

        if (allowCombat) {
            float shootHori = Input.GetAxis("HorizontalShoot");
            float shootVert = Input.GetAxis("VerticalShoot");
            if ((shootHori != 0 || shootVert != 0) && Time.time > lastFire + fireDelay) {
                    Shoot(shootHori, shootVert);
                    lastFire = Time.time;
            }
        }

        if (Input.GetKeyDown(KeyCode.E)) { //Checks when you press 'E' if you can interact with anything
            CheckInteraction();
        }
    }

    private void updateHealth() {
        //Handles overheal
        if (health > maxHealth) {
            health = maxHealth;
        }

        //Sets current Health
        for (int i = 0; i < hearts.Length; i++) {
            if ((i + 1) * 2 <= health) {
                hearts[i].sprite = fullHeart;
            } else if (i * 2 < health && health % 2 == 1) {
                hearts[i].sprite = halfHeart;
            } else {
                hearts[i].sprite = emptyHeart;
            }


            //Sets max Health
            if (i < maxHealth / 2) {
                hearts[i].enabled = true;
            } else {
                hearts[i].enabled = false;
            }
        }
    }

    public void FixedUpdate() {
        if (!inDialogue) {
            //Movement
            move.x = Input.GetAxisRaw("Horizontal");
            move.y = Input.GetAxisRaw("Vertical");

            Vector2 clampedMovement = Vector2.ClampMagnitude(move, 1);
            rb.MovePosition(rb.position + (clampedMovement * speed * Time.fixedDeltaTime));
        }
    }

    public void Heal(int amt) {
        if (health + amt > maxHealth) {
            health = maxHealth;
        } else {
            health += amt;
        }
        updateHealth();
    }

    public void Damage(int amt) {
        if (invulTime < 0) {
            invulTime = resetInvulTime;
            health -= amt;
            updateHealth();
        }
    }

    public void IncreaseMaxHealth() {
        maxHealth++;
        health++;
        updateHealth();
    }

    void Shoot(float x, float y) {
        GameObject bullet = Instantiate(this.bullet, transform.position, transform.rotation) as GameObject;
        bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(
            (x < 0) ? Mathf.Floor(x) * bulletSpeed : Mathf.Ceil(x) * bulletSpeed,
            (y < 0) ? Mathf.Floor(y) * bulletSpeed : Mathf.Ceil(y) * bulletSpeed);
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
                    rc.Interact();
                    return;
                }
            }
        }
    }

    public int getMaxHP() { //for saving
        return this.maxHealth;
    }

    //Saves the Player Data
    public void SavePlayer() {
        SaveSystem.SavePlayer(this);
        this.health = maxHealth; //Heals to full
    }

    //Loads the Player Data
    public void LoadPlayer() {
        PlayerData data = SaveSystem.LoadPlayer();

        this.health = data.maxHealth;
        this.maxHealth = data.maxHealth;

        transform.position = new Vector2(data.position[0], data.position[1]);
    }

    //Toggles whether player is in dialogue or not
    public void ToggleDialogue() {
        if (!inDialogue) {
            dialogueBox.enabled = true;
            dialogueText.SetActive(true);
        } 

        inDialogue = !inDialogue;
    }

    //Does the logic to transfer health from scene to scene
    public void HealthSetter(int health, int maxHealth) {
        this.health = health;
        this.maxHealth = maxHealth;
    }
}