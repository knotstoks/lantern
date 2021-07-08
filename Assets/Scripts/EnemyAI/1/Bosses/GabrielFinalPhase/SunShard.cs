using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

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
                    StartCoroutine(EndRun());
                } else {
                    line++;
                    dialogueManager.DisplayNextSentence();
                }
            }
        }
    }
    private IEnumerator EndRun() {
        if ((int) DataStorage.saveValues["introToEnd"] == 0) {
        DataStorage.saveValues["introToEnd"] = 1;
        }
        DataStorage.saveValues["position"] = new Vector2(-9.8f, 2.2f);
        DataStorage.saveValues["currScene"] = "PriestOffice";
        DataStorage.saveValues["facingDirection"] = 3f;
        DataStorage.saveValues["savedWaxGolem"] = 0;
        DataStorage.saveValues["savedFourArms"] = 0;
        //player.SaveGame(-9.8f, 2.2f, 3, "PriestOffice");

        //Fade to White
        gabrielFinalRoom.FadeOut();
        yield return new WaitForSeconds(1.6f);

        SceneManager.LoadScene("LoadingScreen");
    }
}
