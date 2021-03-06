using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Player : MonoBehaviour {
    public int health;
    public int maxHealth;
    [SerializeField] private Image[] hearts;
    [SerializeField] private Sprite fullHeart;
    [SerializeField] private Sprite halfHeart;
    [SerializeField] private Sprite emptyHeart;
    [SerializeField] private float resetInvulTime;
    public float speed;
    [SerializeField] private float slowSpeed;
    [SerializeField] private float resetSlowTime;
    [SerializeField] private GameObject bullet;
    [SerializeField] private float bulletSpeed; 
    [SerializeField] private float fireDelay;
    [SerializeField] private Color slowColour;
    [HideInInspector] public SaveSystem saveSystem;
    [SerializeField] private GameObject saveText;
    [SerializeField] private GameObject upgradeManager; //Manages upgrades for the player
    public Image interactIcon; //Image for the interactable check
    public GameObject interactName; //GameObject for the interact icon
    public Text interactText; //Text for the interact icon
    public Image dialogueBox; //Dialogue Box for Text
    public GameObject dialogueText; //Text Object
    public Image dialogueImage; //Lantern image in text box
    public bool allowCombat; //Scene will set this
    public bool inDialogue; //Scene will set this
    public bool canPause; //checks if can pause the game
    public Animator animator;
    public GameObject pauseMenu;
    public GameObject quitMenu;
    public Image blackBackground; //for pause menu
    public bool isSpeeding; //For Fleet Foot Upgrade
    private float invulTime; //if its > 0, cannot be damaged. < 0 can be damaged.
    private Vector2 boxSize = new Vector2(1.8f, 1.8f); //Box for raycasting interactables
    private Rigidbody2D rb;
    private float lastFire;
    private Vector2 move; //for movement and animation
    private Vector2 shootVector; //for shooting and animation
    private bool dead;
    private int[][] directions = new int[][] {
        new int[] {0, 1}, //North
        new int[] {1, 0}, //East
        new int[] {0, -1}, //South
        new int[] {-1, 0}, //West
    };
    private float tempSpeed;
    private float slowTime;
    public SpriteRenderer spriteRenderer;
    private bool blinking; //States if player is blinking if damaged
    private AudioSource audioSource;
    private void Start() {
        //Destroy Later!!!!!!!!!!!!!!!!!!!!!!!!!
        // DataStorage.saveValues["health"] = 6;
        // DataStorage.saveValues["maxHealth"] = 6;
        // DataStorage.saveValues["position"] = new Vector2(-9.8f, 2.2f);
        // DataStorage.saveValues["facingDirection"] = 3;
        // PlayerPrefs.SetFloat("volume", 1f);
        // DataStorage.saveValues["progress"] = 1;
        // DataStorage.saveValues["blessings"] = 2;
        // DataStorage.saveValues["tutorialDojo"] = 0;
        // DataStorage.saveValues["waxDungeonGolem"] = 0;
        // DataStorage.saveValues["completedWaxDungeon"] = 0;
        // DataStorage.saveValues["blacksmith"] = 0;
        // DataStorage.saveValues["deaths"] = 4;
        // DataStorage.saveValues["waxDungeonFourArms"] = 0;
        // DataStorage.saveValues["upgrade"] = 1;
        // DataStorage.saveValues["upgradeBar"] = 20;
        // DataStorage.saveValues["waxDungeonRoom"] = 2;
        // DataStorage.saveValues["waxDungeonGabriel"] = 2;
        // DataStorage.saveValues["finalBossBeatenCount"] = 1;
        // DataStorage.saveValues["introSceneDone"] = 0;
        // DataStorage.saveValues["waxDungeonRandomArray"] = new int[] {9, 10, 12, 13, 14, 16, 17, 18, 20};
        // DataStorage.saveValues["reversedControls"] = 0;
        // DataStorage.saveValues["blackOut"] = 0;
        // DataStorage.saveValues["timeTrial"] = 0;
        // DataStorage.saveValues["timeTrialTime"] = 1200f;
        // DataStorage.saveValues["sunShardsCollected"] = 0;
        // DataStorage.saveValues["sunShardsInserted"] = 0;
        // DataStorage.saveValues["completedReversedControls"] = 0;
        // DataStorage.saveValues["completedBlackOut"] = 0;
        // DataStorage.saveValues["completedTimeTrial"] = 0;
        // DataStorage.saveValues["healAfterBosses"] = 2;
        // DataStorage.saveValues["savedFourArms"] = 1;
        // DataStorage.saveValues["savedWaxGolem"] = 0;
        // DataStorage.saveValues["introToEnd"] = 1;
        // DataStorage.saveValues["introToTrials"] = 0;
        // DataStorage.saveValues["newMission"] = 1;
        // DataStorage.saveValues["finishGame"] = 1;
        // DataStorage.saveValues["seenSavePoint"] = 1;

        invulTime = 0.5f;
        rb = GetComponent<Rigidbody2D>();
        saveSystem = GetComponent<SaveSystem>();
        health = (int) DataStorage.saveValues["health"];
        maxHealth = (int) DataStorage.saveValues["maxHealth"];
        transform.position = (Vector2) DataStorage.saveValues["position"];
        audioSource = GetComponent<AudioSource>();
        updateHealth();
        interactIcon.enabled = false;
        interactName.SetActive(false);
        dialogueBox.enabled = false;
        dialogueImage.enabled = false;
        inDialogue = false;
        allowCombat = false;
        pauseMenu.SetActive(false);
        quitMenu.SetActive(false);
        saveText.SetActive(false);
        blackBackground.enabled = false;
        animator.SetFloat("LastMoveX", directions[(int) DataStorage.saveValues["facingDirection"]][0]);
        animator.SetFloat("LastMoveY", directions[(int) DataStorage.saveValues["facingDirection"]][1]);
        canPause = true;
        slowTime = 0;
        tempSpeed = speed;
        dead = false;
    }
    private void Update() {
        if (blinking) {
            if (invulTime > resetInvulTime * 0.99f) {
                spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 0f);
            } else if (invulTime > resetInvulTime * 0.91f) {
                spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 1f);
            } else if (invulTime > resetInvulTime * 0.83f) {
                spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 0f);
            } else if (invulTime > resetInvulTime * 0.75f) {
                spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 1f);
            } else if (invulTime > resetInvulTime * 0.67f) {
                spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 0f);
            } else if (invulTime > resetInvulTime * 0.59f) {
                spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 1f);
            } else if (invulTime > resetInvulTime * 0.51f) {
                spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 0f);
            } else if (invulTime > resetInvulTime * 0.43f) {
                spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 1f);
            } else if (invulTime > resetInvulTime * 0.35f) {
                spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 0f);
            } else if (invulTime > resetInvulTime * 0.27f) {
                spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 1f);
            } else if (invulTime > resetInvulTime * 0.19f) {
                spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 0f);
            } else if (invulTime > resetInvulTime * 0.11f) {
                spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 1f);
            }
        }

        if (invulTime > 0) {
            blinking = true;
            invulTime -= Time.deltaTime;
        } else {
            blinking = false;
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 1f);
        }

        if (Input.GetKeyDown(KeyCode.E)) { //Checks when you press 'E' if you can interact with anything
            CheckInteraction();
        }

        if (health <= 0 && !dead) {
            dead = true;
            animator.SetTrigger("Dead");
            StartCoroutine(KillPlayer());
        }

        // if (Input.GetKeyDown(KeyCode.T)) {
        //     Heal(10);
        // }
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
        if (slowTime > 0) {
            slowTime -= Time.deltaTime;
            speed = slowSpeed;
        } else if (isSpeeding) {
            speed = 5;
        } else {
            speed = tempSpeed;
            spriteRenderer.color = Color.white;
        }

        if (!inDialogue) {
            if (!((int) DataStorage.saveValues["reversedControls"] == 1)) {
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
            } else {
                //Reversed Movement
                move.x = -Input.GetAxisRaw("Horizontal");
                move.y = -Input.GetAxisRaw("Vertical");

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
        }

        if (allowCombat && !inDialogue) {
            shootVector.x = Input.GetAxisRaw("HorizontalShoot");
            shootVector.y = Input.GetAxisRaw("VerticalShoot");

            if (!((int) DataStorage.saveValues["reversedControls"] == 1)) {
                animator.SetFloat("HoriShoot", shootVector.x);
                animator.SetFloat("VertShoot", shootVector.y);
            } else {
                animator.SetFloat("HoriShoot", -shootVector.x);
                animator.SetFloat("VertShoot", -shootVector.y);
            }
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
        if (amt > 0) {
            if (invulTime < 0) {
                invulTime = resetInvulTime;
                health -= amt;
                updateHealth();
                audioSource.Play();

                if (upgradeManager.GetComponent<Upgrades>().upgrade != 0) {
                    upgradeManager.GetComponent<Upgrades>().LoseProgress();
                } 
            }
        }
    }
    public void SetMaxHealth(int n) {
        maxHealth = n;
        health = n;
        updateHealth();
    }
    private void Shoot(float x, float y) {
        if ((int) DataStorage.saveValues["reversedControls"] == 0) {
            //Normal Controls
            Vector2 tempVector = new Vector2(transform.position.x, transform.position.y - 0.3f);
            GameObject bullet = Instantiate(this.bullet, tempVector, transform.rotation) as GameObject;
            bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(
                (x < 0) ? Mathf.Floor(x) * bulletSpeed : Mathf.Ceil(x) * bulletSpeed,
                (y < 0) ? Mathf.Floor(y) * bulletSpeed : Mathf.Ceil(y) * bulletSpeed);
        } else {
            //Reversed Controls
            Vector2 tempVector = new Vector2(transform.position.x, transform.position.y - 0.3f);
            GameObject bullet = Instantiate(this.bullet, tempVector, transform.rotation) as GameObject;
            bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(
                (x < 0) ? Mathf.Floor(x) * bulletSpeed * -1 : Mathf.Ceil(x) * bulletSpeed * -1,
                (y < 0) ? Mathf.Floor(y) * bulletSpeed * -1 : Mathf.Ceil(y) * bulletSpeed * -1);
        }
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
    public void ToggleDialogue(){
        if (!inDialogue) {
            interactIcon.enabled = false;
            dialogueBox.enabled = true;
            dialogueText.SetActive(true);
            dialogueImage.enabled = true;
        }

        inDialogue = !inDialogue;
    }
    public void SlowPlayer() {
        slowTime = resetSlowTime;
        spriteRenderer.color = slowColour;
    }
    //Coroutine to kill Player
    private IEnumerator KillPlayer() {
        int deaths = (int) DataStorage.saveValues["deaths"];
        DataStorage.saveValues["deaths"] = deaths + 1;
        if (deaths + 1 == 5) {
            if (!PlayerPrefs.HasKey("died5")) {
                GameObject.FindGameObjectWithTag("AchievementManager").GetComponent<AchievementManager>().NewAchievement(13);
                PlayerPrefs.SetInt("died5", 1);
            }
        }

        if ((int) DataStorage.saveValues["progress"] == 2) {
            DataStorage.saveValues["progress"] = 3;
        }

        if (deaths + 1 == 10) {
            if (!PlayerPrefs.HasKey("died10")) {
                GameObject.FindGameObjectWithTag("AchievementManager").GetComponent<AchievementManager>().NewAchievement(14);
                PlayerPrefs.SetInt("died10", 1);
            }
        }
        
        inDialogue = true;
        //ONLY FOR CURRENT BUILD!!!!!!!!!!
        if ((int) DataStorage.saveValues["completedWaxDungeon"] == 0) {
            DataStorage.saveValues["completedWaxDungeon"] = 1;
        } 

        //First time you die
        if ((int) DataStorage.saveValues["blessings"] == 0) {
            DataStorage.saveValues["blessings"] = 1;
        }

        DataStorage.saveValues["health"] = DataStorage.saveValues["maxHealth"];
        DataStorage.saveValues["position"] = new Vector2(-9.8f, 2.2f);
        DataStorage.saveValues["facingDirection"] = 3;
        DataStorage.saveValues["currScene"] = "PriestOffice";
        DataStorage.saveValues["waxDungeonRoom"] = 0;
        DataStorage.saveValues["savedWaxGolem"] = 0;
        DataStorage.saveValues["savedFourArms"] = 0;
        SaveGame(-9.8f, 2.2f, 3, "PriestOffice");

        //Let the animation Play Out
        yield return new WaitForSeconds(2);

        SceneManager.LoadScene("LoadingScreen");
    }
    public void SaveGame(float posX, float posY, int facingDirection, string currScene) {
        saveSystem.Save(posX, posY, facingDirection, currScene);
        StartCoroutine(ShowSaveText());
    }
    private IEnumerator ShowSaveText() {
        saveText.SetActive(true);
        yield return new WaitForSeconds(2f);
        saveText.SetActive(false);
    }
}