using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Blacksmith : MonoBehaviour {
    [SerializeField] private Dialogue tutorialDialogue;
    [SerializeField] private GameObject canvas;
    [SerializeField] private Text upgradeText;
    private Player player;
    private DialogueManager dialogueManager;
    private bool inTutorial;
    private int line;
    private void Start() {
        line = 0;
        inTutorial = false;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        dialogueManager = GameObject.FindGameObjectWithTag("DialogueManager").GetComponent<DialogueManager>();
        upgradeText.text = "";
        if ((int) DataStorage.saveValues["blacksmith"] == 2) {
            StartBlacksmithTutorial();
        }
    }
    private void Update() {
        if (inTutorial && Input.GetKeyDown(KeyCode.E)) {
            if (line == tutorialDialogue.names.Length - 1) {
                DataStorage.saveValues["blacksmith"] = 3;
                dialogueManager.DisplayNextSentence();
                inTutorial = false;
            } else {
                dialogueManager.DisplayNextSentence();
            }
        }
    }
    public IEnumerator DisplayChangeUpgrade(string t) {
        upgradeText.text = t;
        yield return new WaitForSeconds(2f);
        upgradeText.text = "";
    }
    private void StartBlacksmithTutorial() {
        inTutorial = true;
        dialogueManager.StartDialogue(tutorialDialogue);
    }
}