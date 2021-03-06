using System.Collections;
using UnityEngine;

public class FourArmsBossRoom : MonoBehaviour {
    [SerializeField] private GameObject doorOut;
    [SerializeField] private Dialogue[] introDialogues; //0 for havent seen, 1 for seen but lost, 2 for seen and beat before
    [SerializeField] private Dialogue outroDialogue;
    [SerializeField] private GameObject bossHPBar;
    [SerializeField] private AudioClip[] music; //0 for boss theme, 1 for end theme
    [SerializeField] private GameObject gate;
    [SerializeField] private GameObject fourArmsBossObject;
    [SerializeField] private GameObject fourArmsLight;
    [SerializeField] private GameObject[] stuffToKill;
    [SerializeField] private GameObject savePoint;
    private bool introDone;
    private bool fightCompleted;
    private FourArms fourArmsBoss;
    private Player player;
    private DialogueManager dialogueManager;
    private int line;
    private AudioSource audioSource;
    private IEnumerator Start() {
        yield return new WaitForSeconds(0.01f);
        if ((int) DataStorage.saveValues["savedFourArms"] == 1) {
            savePoint.GetComponent<SavePoint>().Activate();
            introDone = true;
            for (int i = 0; i < stuffToKill.Length; i++) {
                Destroy(stuffToKill[i]);
            }
            doorOut.SetActive(true);
            gate.GetComponent<Animator>().SetTrigger("Open");
        } else {
            introDone = false;
            fightCompleted = false;
            line = 0;
            bossHPBar.SetActive(false);
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
            fourArmsBoss = fourArmsBossObject.GetComponent<FourArms>();
            dialogueManager = GameObject.FindGameObjectWithTag("DialogueManager").GetComponent<DialogueManager>();
            audioSource = GetComponent<AudioSource>();
            audioSource.loop = true;
            audioSource.clip = music[0];
            audioSource.Play();
            yield return new WaitForSeconds(0.05f);
            StartFourArmsIntro();
            yield return new WaitForSeconds(2f);
            doorOut.SetActive(false);
        }
    } 
    private void Update() {
        if (!introDone && Input.GetKeyDown(KeyCode.E)) { //Intro
            if (line == introDialogues[(int) DataStorage.saveValues["waxDungeonFourArms"]].names.Length - 1) {
                if ((int) DataStorage.saveValues["waxDungeonFourArms"] == 0) {
                    DataStorage.saveValues["waxDungeonFourArms"] = 1;
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
            } else {
                line++;
                dialogueManager.DisplayNextSentence();
            }
        }
    }
    private void StartFourArmsIntro() {
        dialogueManager.StartDialogue(introDialogues[(int) DataStorage.saveValues["waxDungeonFourArms"]]);
    }
    private void StartBossFight() {
        //Set Four Arms Mini Boss Active with HP Bar
        bossHPBar.SetActive(true);
        fourArmsBoss.StartBoss();
    }
    public void CompleteFight() {
        if (!PlayerPrefs.HasKey("fourArmsSlain")) {
            PlayerPrefs.SetInt("fourArmsSlain", 1);
            GameObject.FindGameObjectWithTag("AchievementManager").GetComponent<AchievementManager>().NewAchievement(1);
        }
        player.Heal((int) DataStorage.saveValues["healAfterBosses"]);
        audioSource.clip = music[1];
        audioSource.loop = false;
        audioSource.Play();
        fightCompleted = true;
        bossHPBar.SetActive(false);
        dialogueManager.StartDialogue(outroDialogue);
        doorOut.SetActive(true);
        gate.GetComponent<Animator>().SetTrigger("Open");
        savePoint.GetComponent<SavePoint>().Activate();
    }
    public void DestroyAll() {
        Destroy(fourArmsLight);
        GameObject[] bullets = GameObject.FindGameObjectsWithTag("Bullet");
        GameObject[] bloodBullets = GameObject.FindGameObjectsWithTag("BloodBullet");
        if (bullets != null) {
            for (int j = 0; j < bullets.Length; j++) {
                Destroy(bullets[j]);
            }
        }

        if (bloodBullets != null) {
            for (int j = 0; j < bloodBullets.Length; j++) {
                Destroy(bloodBullets[j]);
            }
        }
    }
}
