using System.Collections;
using UnityEngine;

public class GolemBossRoom : MonoBehaviour {
    [SerializeField] private GameObject doorOut;
    [SerializeField] private Dialogue[] introDialogues;
    [SerializeField] private Dialogue outroDialogue;
    [SerializeField] private GameObject bossHPBar;
    [SerializeField] private AudioClip[] music; //0 for boss theme, 1 for end theme
    [SerializeField] private GameObject gate;
    [SerializeField] private GameObject golemObject;
    [SerializeField] private GameObject savePoint;
    [SerializeField] private GameObject savePointInstructions;
    private bool introDone;
    private bool fightCompleted;
    private Golem golemBoss;
    private Player player;
    private DialogueManager dialogueManager;
    private int line;
    private AudioSource audioSource;
    private bool inInstructions;
    private IEnumerator Start() {
        inInstructions = false;
        savePointInstructions.SetActive(false);
        yield return new WaitForSeconds(0.01f);
        if ((int) DataStorage.saveValues["savedWaxGolem"] == 0) {
            introDone = false;
            fightCompleted = false;
            line = 0;
            bossHPBar.SetActive(false);
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
            golemBoss = GameObject.FindGameObjectWithTag("Boss").GetComponent<Golem>();
            dialogueManager = GameObject.FindGameObjectWithTag("DialogueManager").GetComponent<DialogueManager>();
            audioSource = GetComponent<AudioSource>();
            audioSource.loop = true;
            audioSource.clip = music[0];
            audioSource.Play();
            yield return new WaitForSeconds(0.05f);
            StartGolemIntro();
            yield return new WaitForSeconds(2f);
            doorOut.SetActive(false);
        } else {
            savePoint.GetComponent<SavePoint>().Activate();
            introDone = true;
            Destroy(golemObject);
            bossHPBar.SetActive(false);
            doorOut.SetActive(true);
            gate.GetComponent<Animator>().SetTrigger("Open");
        }
    } 
    private void Update() {
        if (!introDone && Input.GetKeyDown(KeyCode.E)) { //Intro
            if (line == introDialogues[(int) DataStorage.saveValues["waxDungeonGolem"]].names.Length - 1) {
                if ((int) DataStorage.saveValues["waxDungeonGolem"] == 0) {
                    DataStorage.saveValues["waxDungeonGolem"] = 1;
                }

                introDone = true;
                line = 0;
                dialogueManager.DisplayNextSentence();
                player.allowCombat = true;
                StartBossFight();
            } else {
                line++;
                dialogueManager.DisplayNextSentence();
            }
        }

        if (fightCompleted && Input.GetKeyDown(KeyCode.E)) { //Outro
            if (line == outroDialogue.names.Length - 1) {
                fightCompleted = false;
                dialogueManager.DisplayNextSentence();
                line = 0;
                if ((int) DataStorage.saveValues["seenSavePoint"] == 0) {
                    StartCoroutine(StartSavePointIntro());
                }
            } else {
                line++;
                dialogueManager.DisplayNextSentence();
            }
        }

        if (inInstructions && Input.GetKeyDown(KeyCode.E)) {
            savePointInstructions.SetActive(false);
            inInstructions = false;
            DataStorage.saveValues["seenSavePoint"] = 1;
        }
    }
    private IEnumerator StartSavePointIntro() {
        savePointInstructions.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        inInstructions = true;
    }
    private void StartGolemIntro() {
        dialogueManager.StartDialogue(introDialogues[(int) DataStorage.saveValues["waxDungeonGolem"]]);
    }
    private void StartBossFight() {
        //Set Golem Mini Boss Active with HP Bar
        bossHPBar.SetActive(true);
        golemBoss.start = true;
        golemBoss.StartMoving();
        
    }
    public void CompleteFight() {
        if (!PlayerPrefs.HasKey("golemSlain")) {
            PlayerPrefs.SetInt("golemSlain", 1);
            GameObject.FindGameObjectWithTag("AchievementManager").GetComponent<AchievementManager>().NewAchievement(0);
        }
        player.Heal((int) DataStorage.saveValues["healAfterBosses"]);
        audioSource.clip = music[1];
        audioSource.loop = false;
        audioSource.Play();
        fightCompleted = true;
        bossHPBar.SetActive(false);
        GameObject[] bullets = GameObject.FindGameObjectsWithTag("BloodBullet");
        if (bullets.Length != 0) {
            for (int i = 0; i < bullets.Length; i++) {
                Destroy(bullets[i]);
            }
        }
        GameObject[] candlings = GameObject.FindGameObjectsWithTag("Enemy");
        if (candlings.Length != 0) {
            for (int i = 0; i < candlings.Length; i++) {
                candlings[i].GetComponent<GolemCandling>().Damage(100);
            }
        }
        dialogueManager.StartDialogue(outroDialogue);
        doorOut.SetActive(true);
        gate.GetComponent<Animator>().SetTrigger("Open");
        savePoint.GetComponent<SavePoint>().Activate();
    }
}