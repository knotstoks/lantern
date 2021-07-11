using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
Scene references for cutscenes:
EndGameCutscene1
EndGameCutscene2
EndGameCutsceneFinal
*/
public class SunEmblem : Interactable {
    [SerializeField] private Sprite[] sprites; //0 for 4 shards missing, 1 for 3, 2 for 2, 3 for 1, 4 for complete
    [SerializeField] private Dialogue interactDialogue;
    private DialogueManager dialogueManager;
    private bool inCutscene;
    private bool interacting;
    private Player player;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private void Start() {
        interacting = false;
        spriteRenderer = GetComponent<SpriteRenderer>();
        inCutscene = false;
        spriteRenderer.sprite = sprites[(int) DataStorage.saveValues["sunShardsInserted"]];
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        animator = GetComponent<Animator>();
        animator.SetInteger("shardsIn", (int) DataStorage.saveValues["sunShardsInserted"]);

        if ((int) DataStorage.saveValues["sunShardsInserted"] == 0 || (int) DataStorage.saveValues["sunShardsCollected"] < (int) DataStorage.saveValues["sunShardsInserted"]) { //cant insert shard
            objDesc = "Examine Glowing Tree";
        } else { //can insert shard
            objDesc = "Insert Sun Shard";
        }
    }
    private void Update() {
        if (interacting && Input.GetKeyDown(KeyCode.E)) {
            interacting = false;
            dialogueManager.DisplayNextSentence();
        }
    }
    public IEnumerator InsertShard() {
        DataStorage.saveValues["sunShardsInserted"] = (int) DataStorage.saveValues["sunShardsInserted"] + 1;
        spriteRenderer.sprite = sprites[(int) DataStorage.saveValues["sunShardsInserted"]];
        //Fade to White
        yield return new WaitForSeconds(1.5f);
        if ((int) DataStorage.saveValues["sunShardsInserted"] == 2) {
            SceneManager.LoadScene("EndGameCutscene1");
        }

        if ((int) DataStorage.saveValues["sunShardsInserted"] == 3) {
            SceneManager.LoadScene("EndGameCutscene2");
        }

        if ((int) DataStorage.saveValues["sunShardsInserted"] == 4) {
            SceneManager.LoadScene("EndGameCutsceneFinal");
        }

        //Put player in front of sun when he comes back from cutscene and save the game
        DataStorage.saveValues["position"] = new Vector2(0, -1.5f);
        DataStorage.saveValues["facingDirection"] = 0;
        DataStorage.saveValues["currScene"] = "SunShardRoom";
        player.SaveGame(0, -1.5f, 0, "SunShardRoom");
    }
    public override void Interact() {
        if (!inCutscene) {
            if ((int) DataStorage.saveValues["sunShardsCollected"] <= (int) DataStorage.saveValues["sunShardsInserted"]) { //No shards
                if (!player.inDialogue) {
                    interacting = true;
                    dialogueManager.StartDialogue(interactDialogue);
                }
            } else { //Got shards
                inCutscene = true;
                InsertShard();
            }
        }
    }
    public void PriestInserted() {
        spriteRenderer.sprite = sprites[1];
        animator.SetInteger("shardsIn", 1);
        DataStorage.saveValues["sunShardsInserted"] = 1;
    }
}
