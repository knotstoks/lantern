using UnityEngine;
using UnityEngine.SceneManagement;

public class GameFinalDialogue : MonoBehaviour {
    [SerializeField] private Dialogue dialogue;
    private int line;
    private DialogueManager dialogueManager;
    private bool inDialogue;
    private void Start() {
        inDialogue = false;
        dialogueManager = GameObject.FindGameObjectWithTag("DialogueManager").GetComponent<DialogueManager>();
        line = 0;
        StartEndDialogue();
    }
    private void Update() {
        if (inDialogue && Input.GetKeyDown(KeyCode.E)) {
            if (line == dialogue.sentences.Length - 1) {
                inDialogue = false;
                line = 0;
                EndGame();
            } else {
                line++;
                dialogueManager.DisplayNextSentence();
            }
        }
    }
    private void StartEndDialogue() {
        dialogueManager.StartDialogue(dialogue);
        inDialogue = true;
    }
    private void EndGame() {
        SceneManager.LoadScene("Credits");
    }
}
