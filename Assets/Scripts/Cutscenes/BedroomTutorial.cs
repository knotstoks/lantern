using System.Collections;
using UnityEngine;

public class BedroomTutorial : MonoBehaviour {

    private Player player;
    private DialogueManager dialogueManager;
    [TextArea(3, 10)]
    [SerializeField] private string[] lines;
    private int line;

    private IEnumerator Start() {
        yield return 0.2;
        Debug.Log(PlayerPrefs.GetInt("introSceneDone"));
        line = -1;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        dialogueManager = GameObject.FindGameObjectWithTag("DialogueManager").GetComponent<DialogueManager>();
        player.inDialogue = true;

        if (DataStorage.introSceneDone == 0) {
            //Start the Intro Cutscene and bedroom starting dialogue
            StartBedRoomTutorial();
        }
    } 

    private void Update() {
        if (DataStorage.introSceneDone == 0 && Input.GetKeyDown(KeyCode.E)) {
            if (line == lines.Length - 1) {
                //Sets the marker for finishing the intro scene and disables the text boxes
                player.inDialogue = false;
                player.dialogueBox.enabled = false;
                player.dialogueText.SetActive(false);
                DataStorage.introSceneDone = 1;
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