using UnityEngine;

public class Enzio : Interactable {
    [SerializeField] private Dialogue introDialogue;
    private DialogueManager dialogueManager;
    private Player player;
    private UpgradeUI upgradeUI;
    private bool talking;
    private int line;
    [HideInInspector] public bool menuOpen;
    private void Start() {
        menuOpen = false;
        dialogueManager = GameObject.FindGameObjectWithTag("DialogueManager").GetComponent<DialogueManager>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        line = 0;
    }
    private void Update() {
        if (talking && Input.GetKeyDown(KeyCode.E)) {
            if (line == introDialogue.names.Length - 1) {
                dialogueManager.DisplayNextSentence();
                line = 0;
                DataStorage.saveValues["blacksmith"] = 1;
                talking = false;
            } else {
                dialogueManager.DisplayNextSentence();
            }
        }
    }
    public override void Interact() {
        if ((int) DataStorage.saveValues["blacksmith"] == 0 && !talking) {
            dialogueManager.StartDialogue(introDialogue);
            talking = true;
        }

        if ((int) DataStorage.saveValues["blacksmith"] == 1 && !menuOpen) {
            upgradeUI.OpenMenu();
            menuOpen = true;
            player.inDialogue = true;
        }
    }
}
