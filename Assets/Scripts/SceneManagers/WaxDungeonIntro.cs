using System.Collections;
using UnityEngine;

public class WaxDungeonIntro : MonoBehaviour {
    [SerializeField] private Dialogue[] dialogues; //0 & 1 for tutorial (start and end), 2 for repeating it
    [SerializeField] private Dialogue trialIntro;
    [SerializeField] private GameObject spawnling;
    [SerializeField] private Vector2 spawnLocation;
    private bool inTutorialIntro;
    private bool inTutorialOutro;
    private int line;
    private Player player;
    private DialogueManager dialogueManager;
    private int tutorialInt;
    private AudioSource audioSource;
    private IEnumerator Start() {
        tutorialInt = 1;
        line = 0;
        inTutorialIntro = false;
        inTutorialOutro = false;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        dialogueManager = GameObject.FindGameObjectWithTag("DialogueManager").GetComponent<DialogueManager>();
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = PlayerPrefs.GetFloat("volume");
        //Generates the random array
        int[] randArray = new int[9]; //9 - 11, 13 - 15, 17 - 19 || Bosses: 12, 16, 20
        randArray[0] = Random.Range(9, 12);
        randArray[1] = Random.Range(9, 12);
        while (randArray[0] == randArray[1]) {
            randArray[1] = Random.Range(9, 12);
        }
        randArray[2] = 12;
        randArray[3] = Random.Range(13, 16);
        randArray[4] = Random.Range(13, 16);
        while (randArray[3] == randArray[4]) {
            randArray[4] = Random.Range(13, 16);
        }
        randArray[5] = 16;
        randArray[6] = Random.Range(17, 20);
        randArray[7] = Random.Range(17, 20);
        while (randArray[6] == randArray[7]) {
            randArray[7] = Random.Range(17, 20);
        }
        randArray[8] = 20;
        DataStorage.saveValues["waxDungeonRandomArray"] = randArray;
        yield return new WaitForSeconds(0.2f);

        if ((int) DataStorage.saveValues["completedWaxDungeon"] == 0) { //Trigger first time
            StartDungeonTutorial();
        }

        if ((int) DataStorage.saveValues["introToTrials"] == 1) { //Trigger Trials Intro
            StartTrialIntro();
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

        if ((int) DataStorage.saveValues["introToTrials"] == 1 && Input.GetKeyDown(KeyCode.E)) {
            if (line == trialIntro.sentences.Length - 1) {
                dialogueManager.DisplayNextSentence();
                line = 0;
                DataStorage.saveValues["introToTrials"] = 2;
            } else {
                dialogueManager.DisplayNextSentence();
                line++;
            }
        }
    }
    private void StartTrialIntro() {
        dialogueManager.StartDialogue(trialIntro);
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
        tutorialInt -= 1;
    }
}
