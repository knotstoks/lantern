using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BedroomTutorial : MonoBehaviour {

    private Player player;
    private DialogueManager dialogueManager;
    [SerializeField] private Dialogue lines;
    [SerializeField] private Dialogue endGameMonologue;
    private int line;

    private IEnumerator Start() {
        yield return new WaitForSeconds(0.05f);
        line = 0;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        dialogueManager = GameObject.FindGameObjectWithTag("DialogueManager").GetComponent<DialogueManager>();

        if ((int) DataStorage.saveValues["introSceneDone"] == 0) {
            //Start the Intro Cutscene and bedroom starting dialogue
            StartBedRoomTutorial();
        }

        if ((int) DataStorage.saveValues["newMission"] == 1) {
            dialogueManager.StartDialogue(endGameMonologue);
        } 
    } 

    private void Update() {
        if ((int) DataStorage.saveValues["introSceneDone"] == 0 && Input.GetKeyDown(KeyCode.E)) {
            if (line == 2) {
                line = 0;
                DataStorage.saveValues["introSceneDone"] = 1;
                dialogueManager.DisplayNextSentence();
            } else {
                line++;
                dialogueManager.DisplayNextSentence();
            }
        }

        if ((int) DataStorage.saveValues["newMission"] == 1 && Input.GetKeyDown(KeyCode.E)) {
            if (line == endGameMonologue.sentences.Length - 1) {
                line = 0;
                DataStorage.saveValues["newMission"] = 2;
                dialogueManager.DisplayNextSentence();
            } else {
                line++;
                dialogueManager.DisplayNextSentence();
            }
        }
    }

    private void StartBedRoomTutorial() {
        dialogueManager.StartDialogue(lines);
    }
}