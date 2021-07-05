using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GabrielBossRoom : MonoBehaviour {
    [SerializeField] private GameObject bossHPBar;
    [SerializeField] private Dialogue[] introDialogues; //0 for haven't seen, 1 for seen
    [SerializeField] private Dialogue[] endGameIntroDialogues;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip[] music; //0 for intro music, 1 for fight music, 2 for finished
    private int[] turnAroundLine = new int[] {20, 1};
    private int[] wingsEmergeLine = new int[] {21, 3};
    private int[] endGameTurnAroundLine = new int[] {2, 1, 1};
    private int[] endGameEmergeLine = new int[] {5, 4, 4};
    private int line;
    private bool inIntro;
    private Player player;
    private DialogueManager dialogueManager;
    private Gabriel gabriel;
    private IEnumerator Start() {
        line = 0;
        inIntro = false;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        dialogueManager = GameObject.FindGameObjectWithTag("DialogueManager").GetComponent<DialogueManager>();
        gabriel = GameObject.FindGameObjectWithTag("Boss").GetComponent<Gabriel>();
        bossHPBar.SetActive(false);
        audioSource = GetComponent<AudioSource>();
        audioSource.loop = true;
        audioSource.clip = music[0];
        audioSource.Play();
        yield return new WaitForSeconds(0.05f);
        StartIntroDialogue();
    }
    private void Update() {
        if (inIntro && Input.GetKeyDown(KeyCode.E) && (int) DataStorage.saveValues["finalBossBeatenCount"] == 0) {
            if (line == introDialogues[(int) DataStorage.saveValues["waxDungeonGabriel"]].names.Length - 1) {
                inIntro = false;
                line = 0;
                dialogueManager.DisplayNextSentence();
                DataStorage.saveValues["waxDungeonGabriel"] = 1;
                audioSource.clip = music[1];
                audioSource.Play();
                player.allowCombat = true;
                bossHPBar.SetActive(true);
                //Start Gabriel Boss Fight
                gabriel.StartBossBattle();
            } else if (line == turnAroundLine[(int) DataStorage.saveValues["waxDungeonGabriel"]]) { //Turn around
                StartCoroutine(gabriel.TurnAround());
                line++;
                dialogueManager.DisplayNextSentence();
            } else if (line == wingsEmergeLine[(int) DataStorage.saveValues["waxDungeonGabriel"]]) {
                StartCoroutine(gabriel.WingsEmerge());
                line++;
                dialogueManager.DisplayNextSentence();
            } else {
                line++;
                dialogueManager.DisplayNextSentence();
            }
        }

        if (inIntro && Input.GetKeyDown(KeyCode.E) && (int) DataStorage.saveValues["finalBossBeatenCount"] > 0) {
            if (line == endGameIntroDialogues[(int) DataStorage.saveValues["finalBossBeatenCount"]].names.Length - 1) {
                inIntro = false;
                line = 0;
                dialogueManager.DisplayNextSentence();
                audioSource.clip = music[1];
                audioSource.Play();
                player.allowCombat = true;
                bossHPBar.SetActive(true);
                //Start Gabriel Boss Fight
                gabriel.StartBossBattle();
            } else if (line == endGameTurnAroundLine[(int) DataStorage.saveValues["finalBossBeatenCount"]]) { //Turn around
                StartCoroutine(gabriel.TurnAround());
                line++;
                dialogueManager.DisplayNextSentence();
            } else if (line == endGameEmergeLine[(int) DataStorage.saveValues["finalBossBeatenCount"]]) {
                StartCoroutine(gabriel.WingsEmerge());
                line++;
                dialogueManager.DisplayNextSentence();
            } else {
                line++;
                dialogueManager.DisplayNextSentence();
            }
        }
    }
    private void StartIntroDialogue() {
        inIntro = true;
        if ((int) DataStorage.saveValues["finalBossBeatenCount"] > 3) {
            dialogueManager.StartDialogue(endGameIntroDialogues[3]);
        } else if ((int) DataStorage.saveValues["finalBossBeatenCount"] > 0) {
            dialogueManager.StartDialogue(endGameIntroDialogues[(int) DataStorage.saveValues["finalBossBeatenCount"]]);
        } else if ((int) DataStorage.saveValues["waxDungeonGabriel"] == 0 || (int) DataStorage.saveValues["waxDungeonGabriel"] == 1) {
            dialogueManager.StartDialogue(introDialogues[(int) DataStorage.saveValues["waxDungeonGabriel"]]);
        }
    }
    public void CompleteFight() {
        player.Heal((int) DataStorage.saveValues["healAfterBosses"]);
        audioSource.clip = music[1];
        audioSource.loop = false;
        audioSource.Play();
        bossHPBar.SetActive(false);
    }
}