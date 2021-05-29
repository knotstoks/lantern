using System.Collections;
using UnityEngine;

public class BedroomTutorial : MonoBehaviour {

    private Player player;
    private DialogueManager dialogueManager;
    [SerializeField] private Dialogue lines;
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
    } 

    private void Update() {
        if ((int) DataStorage.saveValues["introSceneDone"] == 0 && Input.GetKeyDown(KeyCode.E)) {
            if (line == 2) {
                DataStorage.saveValues["introSceneDone"] = 1;
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