using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class TrialsUI : Interactable {
    [SerializeField] private Dialogue[] dialogues; //0 for first time, 1 for others
    [SerializeField] private GameObject actualTrialUI;
    [SerializeField] private GameObject reversedToggleObject;
    [SerializeField] private GameObject blindToggleObject;
    [SerializeField] private GameObject timedToggleObject;
    [SerializeField] private GameObject reverseSelected;
    [SerializeField] private GameObject blindSelected;
    [SerializeField] private GameObject timedSelected;
    private bool isOpen;
    private Player player;
    private DialogueManager dialogueManager;
    private int line;
    private bool talking;
    private void Start() {
        isOpen = false;
        line = 0;
        talking = false;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        dialogueManager = GameObject.FindGameObjectWithTag("DialogueManager").GetComponent<DialogueManager>();
        reverseSelected.SetActive(false);
        blindSelected.SetActive(false);
        timedSelected.SetActive(false);
        actualTrialUI.SetActive(false);
    }
    public override void Interact() {
        if (!isOpen) {
            if (!player.inDialogue) {
                if ((int) DataStorage.saveValues["introToTrials"] == 2) {
                    dialogueManager.StartDialogue(dialogues[0]);
                    talking = true;
                } else if ((int) DataStorage.saveValues["introToTrials"] == 3) {
                    dialogueManager.StartDialogue(dialogues[1]);
                    talking = true;
                }
            }
        }
    }
    private void Update() {
        if (EventSystem.current.currentSelectedGameObject == reversedToggleObject) {
            reverseSelected.SetActive(true);
            blindSelected.SetActive(false);
            timedSelected.SetActive(false);
        } else if (EventSystem.current.currentSelectedGameObject == blindToggleObject) {
            reverseSelected.SetActive(false);
            blindSelected.SetActive(true);
            timedSelected.SetActive(false);
        } else if (EventSystem.current.currentSelectedGameObject == timedToggleObject) {
            reverseSelected.SetActive(false);
            blindSelected.SetActive(false);
            timedSelected.SetActive(true);
        }

        if (talking && Input.GetKeyDown(KeyCode.E)) {
            if (line == dialogues[(int) DataStorage.saveValues["introToTrials"] - 2].sentences.Length - 1) {
                dialogueManager.DisplayNextSentence();
                line = 0;
                talking = false;
                ToggleUI();
                if ((int) DataStorage.saveValues["introToTrials"] == 2) {
                    DataStorage.saveValues["introToTrials"] = 3;
                }
            } else {
                dialogueManager.DisplayNextSentence();
                line++;
            }
        }
        if (isOpen && Input.GetKeyDown(KeyCode.Escape)) {
            ToggleUI();
        }
    }
    private void ToggleUI() {
        if (!isOpen) {
            isOpen = true;
            player.canPause = false;
            player.inDialogue = true;
            actualTrialUI.SetActive(true);
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(reversedToggleObject);
        } else {
            StartCoroutine(PauseDelay());
            isOpen = false;
            actualTrialUI.SetActive(false);
            player.inDialogue = false;
        }
    }
    private IEnumerator PauseDelay() {
        yield return new WaitForSeconds(1);
        player.canPause = true;
    }
}