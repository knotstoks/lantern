using UnityEngine;

public class SunShard : Interactable {
    [SerializeField] private Dialogue sunShardDialogue;
    private DialogueManager dialogueManager;
    private Player player;
    private int line;
    private bool pressed;
    private GabrielFinalRoom gabrielFinalRoom;
    private void Start() {
        gabrielFinalRoom = GameObject.FindGameObjectWithTag("SceneManager").GetComponent<GabrielFinalRoom>();
        line = 0;
        pressed = false;
        dialogueManager = GameObject.FindGameObjectWithTag("DialogueManager").GetComponent<DialogueManager>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }
    public override void Interact() {
        if (!pressed) {
            if (!player.inDialogue) {
                dialogueManager.StartDialogue(sunShardDialogue);
            } else {
                if (line == sunShardDialogue.names.Length - 1) { //Finish Dialogue and teleport the player
                    //Add 1 beaten attempt to the boss beaten count
                    DataStorage.saveValues["finalBossBeatenCount"] = (int) DataStorage.saveValues["finalBossBeatenCount"] + 1;
                    pressed = true;
                    StartCoroutine(gabrielFinalRoom.FadeOut());
                } else {
                    line++;
                    dialogueManager.DisplayNextSentence();
                }
            }
        }
    }
}
