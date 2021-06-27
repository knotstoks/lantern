using UnityEngine;
using UnityEngine.SceneManagement;

public class SunShard : Interactable {
    [SerializeField] private Dialogue sunShardDialogue;
    private DialogueManager dialogueManager;
    private Player player;
    private int line;
    private void Start() {
        line = 0;
        dialogueManager = GameObject.FindGameObjectWithTag("DialogueManager").GetComponent<DialogueManager>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }
    public override void Interact() {
        // if (!player.inDialogue) {
        //     dialogueManager.StartDialogue(sunShardDialogue);
        // } else {
        //     if (line == sunShardDialogue.names.Length - 1) { //Finish Dialogue and teleport the player
        //         //Add 1 beaten attempt to the boss beaten count
        //         DataStorage.saveValues["finalBossBeatenCount"] = (int) DataStorage.saveValues["finalBossBeatenCount"] + 1;
        //         EndRun();
        //     } else {
        //         line++;
        //         dialogueManager.DisplayNextSentence();
        //     }
        // }

        //ONLY FOR MILESTONE 2
        SceneManager.LoadScene("PlayTestingMainMenu");
    }
    private void EndRun() {
        DataStorage.saveValues["position"] = new Vector2(); //EDIT!!!!!!!!!!!!
        DataStorage.saveValues["currScene"] = ""; //EDIT!!!!!!!!!!!!!
        DataStorage.saveValues["facingDirection"] = 0;

        SceneManager.LoadScene("LoadingScreen");
    }
}
