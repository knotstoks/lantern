using UnityEngine;

public class WaxDungeonIntro : MonoBehaviour {
    [SerializeField] private Dialogue[] dialogues; //0 & 1 for tutorial (start and end), 2 for repeating it
    [SerializeField] private GameObject spawnling;
    [SerializeField] private Vector2 spawnLocation;
    private bool inTutorialIntro;
    private bool inTutorialOutro;
    private int line;
    private Player player;
    private DialogueManager dialogueManager;
    private int tutorialInt;
    private AudioSource audioSource;
    private void Start() {
        tutorialInt = 1;
        line = 0;
        inTutorialIntro = false;
        inTutorialOutro = false;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        dialogueManager = GameObject.FindGameObjectWithTag("DialogueManager").GetComponent<DialogueManager>();
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = PlayerPrefs.GetFloat("volume");

        if ((int) DataStorage.saveValues["completedWaxDungeon"] == 0) { //Trigger first time
            StartDungeonTutorial();
        }
    }
    private void Update() {
        if (inTutorialIntro && Input.GetKeyDown(KeyCode.E)) {
            if (line == dialogues[0].names.Length - 1) { //End intro
                GolemCandling candling = GameObject.FindGameObjectWithTag("Enemy").GetComponent<GolemCandling>();
                player.GetComponent<Player>().allowCombat = true;
                candling.MoveAgain();
                dialogueManager.DisplayNextSentence();
                line = 0;
                inTutorialIntro = false;
            } else if (line == 1) { //When Player notices candle moving
                dialogueManager.DisplayNextSentence();
                Instantiate(spawnling, spawnLocation, Quaternion.identity);
                line++;
            } else {
                dialogueManager.DisplayNextSentence();
                line++;
            }
        }

        if (inTutorialOutro && Input.GetKeyDown(KeyCode.E)) {
            if (line == dialogues[1].names.Length - 1) {
                dialogueManager.DisplayNextSentence();
                line = 0;
                inTutorialOutro = false;
            } else {
                dialogueManager.DisplayNextSentence();
                line++;
            }
        }

        if (tutorialInt == 0) {
            tutorialInt = 1;
            EndDungeonTutorial();
        }
    }
    private void StartDungeonTutorial() {
        inTutorialIntro = true;
        dialogueManager.StartDialogue(dialogues[0]);
    }
    private void EndDungeonTutorial() {
        dialogueManager.StartDialogue(dialogues[1]);
        inTutorialOutro = true;
    }
    public void KillCandling() {
        tutorialInt -=1 ;
    }
}
