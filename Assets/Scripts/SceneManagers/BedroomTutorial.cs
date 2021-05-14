using System.Collections;
using UnityEngine;

public class BedroomTutorial : ManageScene {

    private Player player;
    private DialogueManager dialogueManager;
    [TextArea(3, 10)]
    [SerializeField] private string[] lines;
    private int line;

    private IEnumerator Start() {
        DataStorage.saveValues["introSceneDone"] = 0; //DELETE AFTER
        reference = "introSceneDone";
        yield return 0.2;
        line = -1;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        dialogueManager = GameObject.FindGameObjectWithTag("DialogueManager").GetComponent<DialogueManager>();

        if (referenceInt == 0) {
            //Start the Intro Cutscene and bedroom starting dialogue
            StartBedRoomTutorial();
        }
    } 

    private void Update() {
        if (referenceInt == 0 && Input.GetKeyDown(KeyCode.E)) {
            if (line == lines.Length - 1) {
                //Sets the marker for finishing the intro scene and disables the text boxes
                player.inDialogue = false;
                player.dialogueBox.enabled = false;
                player.dialogueText.SetActive(false);
                player.dialogueImage.enabled = false;
                DataStorage.saveValues["introSceneDone"] = 1;
                player.inDialogue = false;
            } else {
                NextLine();
            }
        }
    }

    private void StartBedRoomTutorial() {
        line = 0;
        player.inDialogue = true;
        player.dialogueBox.enabled = true;
        player.dialogueText.SetActive(true);
        player.dialogueImage.enabled = true;
        dialogueManager.dialogueText.text = lines[line];
        dialogueManager.nameText.text = "Lumin";
    }

    private void NextLine() {
        line += 1;
        if (line != 2) {
            dialogueManager.dialogueText.text = lines[line];
        } else {
            dialogueManager.nameText.text = "";
            dialogueManager.dialogueText.text = lines[line];
        }
    }
}