﻿using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour{
    public int health;
    public int maxHealth;
    [SerializeField] private Image[] hearts;
    [SerializeField] private Sprite fullHeart;
    [SerializeField] private Sprite halfHeart;
    [SerializeField] private Sprite emptyHeart;
    [SerializeField] private float resetInvulTime;
    [SerializeField] private float speed;
    [SerializeField] private GameObject bullet;
    [SerializeField] private float bulletSpeed; 
    [SerializeField] private float fireDelay;
    public Image interactIcon; //Image for the interactable check
    public GameObject interactName; //GameObject for the interact icon
    public Text interactText; //Text for the interact icon
    public Image dialogueBox; //Dialogue Box for Text
    public GameObject dialogueText; //Text Object
    public Image dialogueImage; //Lantern image in text box
    public bool allowCombat; //Scene will set this
    public bool inDialogue; //Scene will set this
    public Animator animator;
    public GameObject pauseMenu;
    public GameObject quitMenu;
    public Image blackBackground; //for pause menu
    private float invulTime; //if its > 0, cannot be damaged. < 0 can be damaged.
    private Vector2 boxSize = new Vector2(2.5f, 2.5f); //Box for raycasting interactables
    private Rigidbody2D rb;
    private float lastFire;
    private Vector2 move; //for movement and animation
    private Vector2 shootVector; //for shooting and animation
    
    void Start() {
        //Destroy Later
        DataStorage.saveValues["health"] = 6;
        DataStorage.saveValues["maxHealth"] = 6;
        DataStorage.saveValues["position"] = new Vector2(0, 0);


        invulTime = 0.5f;
        rb = gameObject.GetComponent<Rigidbody2D>();
        health = (int) DataStorage.saveValues["health"];
        maxHealth = (int) DataStorage.saveValues["maxHealth"];
        transform.position = (Vector2) DataStorage.saveValues["position"];
        updateHealth();
        interactIcon.enabled = false;
        dialogueBox.enabled = false;
        dialogueImage.enabled = false;
        inDialogue = false;
        allowCombat = false;
        pauseMenu.SetActive(false);
        quitMenu.SetActive(false);
        blackBackground.enabled = false;
    }

    void Update() {
        if (invulTime > 0) {
            invulTime -= Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.E)) { //Checks when you press 'E' if you can interact with anything
            CheckInteraction();
        }

        if (health <= 0) {
            animator.SetTrigger("Dead");
            StartCoroutine(KillPlayer());
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

        DataStorage.saveValues["health"] = this.health;
        DataStorage.saveValues["maxHealth"] = this.maxHealth;
    }

    public void FixedUpdate() {
        if (!inDialogue) {
            //Movement
            move.x = Input.GetAxisRaw("Horizontal");
            move.y = Input.GetAxisRaw("Vertical");

            animator.SetFloat("Horizontal", move.x);
            animator.SetFloat("Vertical", move.y);
            animator.SetFloat("Speed", move.sqrMagnitude);

            if (move.x == -1f || move.x == 1f || move.y == 1f || move.y == -1f) {
                animator.SetFloat("LastMoveX", move.x);
                animator.SetFloat("LastMoveY", move.y);
            }

            Vector2 clampedMovement = Vector2.ClampMagnitude(move, 1);
            rb.MovePosition(rb.position + (clampedMovement * speed * Time.fixedDeltaTime));
        }

        if (allowCombat) {
            shootVector.x = Input.GetAxisRaw("HorizontalShoot");
            shootVector.y = Input.GetAxisRaw("VerticalShoot");

            animator.SetFloat("HoriShoot", shootVector.x);
            animator.SetFloat("VertShoot", shootVector.y);
            animator.SetFloat("ShootSpeed", shootVector.sqrMagnitude);

            if ((shootVector.x != 0 || shootVector.y != 0) && Time.time > lastFire + fireDelay) {
                    Shoot(shootVector.x, shootVector.y);
                    lastFire = Time.time;
            }
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
        interactName.SetActive(true);
    }

    public void CloseInteractableIcon() {
        interactIcon.enabled = false;
        interactName.SetActive(false);
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
    
    //Toggles whether player is in dialogue or not
    public void ToggleDialogue() {
        if (!inDialogue) {
            interactIcon.enabled = false;
            interactText.enabled = false;
            dialogueBox.enabled = true;
            dialogueText.SetActive(true);
            dialogueImage.enabled = true;
        } 

        inDialogue = !inDialogue;
    }

    //Coroutine to kill Player
    private IEnumerator KillPlayer() {
        //Let the animation Play Out
        yield return new WaitForSeconds(2);
        //Go to "You Lose" screen
    }
}