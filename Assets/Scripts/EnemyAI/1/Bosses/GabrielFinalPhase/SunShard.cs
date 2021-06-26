using UnityEngine;
using UnityEngine.SceneManagement;

public class SunShard : Interactable {
    [SerializeField] private Dialogue[] sunShardDialogue;
    private DialogueManager dialogueManager;
    private Player player;
    private int line;
    private void Start() {
        line = 0;
        dialogueManager = GameObject.FindGameObjectWithTag("DialogueManager").GetComponent<DialogueManager>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }
    public override void Interact() {
        if (!player.inDialogue) {
            dialogueManager.StartDialogue(sunShardDialogue[(int) DataStorage.saveValues["finalBossBeatenCount"]]);
        } else {
            if (line == sunShardDialogue[(int) DataStorage.saveValues["finalBossBeatenCount"]].names.Length - 1) { //Finish Dialogue and teleport the player
                //Add 1 beaten attempt to the boss beaten count
                DataStorage.saveValues["finalBossBeatenCount"] = (int) DataStorage.saveValues["finalBossBeatenCount"] + 1;
                EndRun();
            } else {
                line++;
                dialogueManager.DisplayNextSentence();
            }
        }
    }
    private void EndRun() {
        DataStorage.saveValues["position"] = new Vector2(); //EDIT!!!!!!!!!!!!
        DataStorage.saveValues["currScene"] = ""; //EDIT!!!!!!!!!!!!!
        DataStorage.saveValues["facingDirection"] = 0;

        SceneManager.LoadScene("LoadingScreen");
    }
}
