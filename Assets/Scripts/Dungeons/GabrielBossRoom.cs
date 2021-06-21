using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GabrielBossRoom : MonoBehaviour {
    [SerializeField] private GameObject bossHPBar;
    [SerializeField] private Dialogue[] introDialogues; //0 for haven't seen, 1 for seen
    [SerializeField] private Dialogue outroDialogue;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip[] music; //0 for intro music, 1 for fight music, 2 for finished
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
        if (inIntro && Input.GetKeyDown(KeyCode.E)) {
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
            } else if (line == 21) { //Turn around
                StartCoroutine(gabriel.TurnAround());
                line++;
                dialogueManager.DisplayNextSentence();
            } else if (line == 22) {
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
        dialogueManager.StartDialogue(introDialogues[(int) DataStorage.saveValues["waxDungeonGabriel"]]);
    }
    public void GoFinalScene() {
        DataStorage.saveValues["position"] = new Vector2(); //EDITTTT!!!!!!!!!!!!!
        DataStorage.saveValues["facingDirection"] = 0;
        SceneManager.LoadScene("FinalFormGabriel");
    }
    public void CompleteFight() {
        audioSource.clip = music[1];
        audioSource.loop = false;
        audioSource.Play();
        bossHPBar.SetActive(false);
        dialogueManager.StartDialogue(outroDialogue);
    }
}