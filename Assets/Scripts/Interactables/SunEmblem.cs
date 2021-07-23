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
    [SerializeField] private Dialogue endGameDialogue;
    [SerializeField] private Sprite[] sprites; //0 for 4 shards missing, 1 for 3, 2 for 2, 3 for 1, 4 for complete
    [SerializeField] private Dialogue interactDialogue;
    [SerializeField] private GameObject fadeToWhiteObject;
    private DialogueManager dialogueManager;
    private bool inCutscene;
    private bool inDialogue;
    private Player player;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private FadeToWhite fadeToWhite;
    private int line;
    private void Start() {
        dialogueManager = GameObject.FindGameObjectWithTag("DialogueManager").GetComponent<DialogueManager>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        inCutscene = false;
        spriteRenderer.sprite = sprites[(int) DataStorage.saveValues["sunShardsInserted"]];
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        animator = GetComponent<Animator>();
        animator.SetInteger("shardsIn", (int) DataStorage.saveValues["sunShardsInserted"]);
        fadeToWhite = fadeToWhiteObject.GetComponent<FadeToWhite>();
        line = 0;

        if ((int) DataStorage.saveValues["sunShardsCollected"] < (int) DataStorage.saveValues["sunShardsInserted"]) { //cant insert shard
            objDesc = "Examine Glowing Tree";
        } else { //can insert shard
            objDesc = "Insert Sun Shard";
        }
    }
    public IEnumerator InsertShard() {
        DataStorage.saveValues["sunShardsInserted"] = (int) DataStorage.saveValues["sunShardsInserted"] + 1;
        spriteRenderer.sprite = sprites[(int) DataStorage.saveValues["sunShardsInserted"]];

        //Put player in front of sun when he comes back from cutscene and save the game
        DataStorage.saveValues["position"] = new Vector2(0, -1.5f);
        DataStorage.saveValues["facingDirection"] = 0;
        DataStorage.saveValues["currScene"] = "SunShardRoom";

        //Fade to White
        if ((int) DataStorage.saveValues["sunShardsInserted"] == 2) {
            StartCoroutine(fadeToWhite.FadeNow());
            yield return new WaitForSeconds(1.5f);
            player.SaveGame(0, -1.5f, 0, "SunShardRoom");
            SceneManager.LoadScene("EndGameCutscene1");
        }

        if ((int) DataStorage.saveValues["sunShardsInserted"] == 3) {
            StartCoroutine(fadeToWhite.FadeNow());
            yield return new WaitForSeconds(1.5f);
            player.SaveGame(0, -1.5f, 0, "SunShardRoom");
            SceneManager.LoadScene("EndGameCutscene2");
        }

        if ((int) DataStorage.saveValues["sunShardsInserted"] == 4) {
            inDialogue = true;
            DataStorage.saveValues["finishGame"] = 2;
            player.SaveGame(0, -1.5f, 0, "SunShardRoom");
            dialogueManager.StartDialogue(endGameDialogue);
        }
    }
    public override void Interact() {
        if (!inCutscene) {
            if ((int) DataStorage.saveValues["sunShardsCollected"] <= (int) DataStorage.saveValues["sunShardsInserted"]) { //No shards
                if (!player.inDialogue) {
                    dialogueManager.StartDialogue(interactDialogue);
                } else {
                    dialogueManager.DisplayNextSentence();
                }
            } else { //Got shards
                inCutscene = true;
                InsertShard();
            }
        }

        if (inDialogue) {
            if (line == endGameDialogue.sentences.Length - 1) {
                SceneManager.LoadScene("Credits");
            } else {
                line++;
                dialogueManager.DisplayNextSentence();
            }
        }
    }
    public void PriestInserted() {
        spriteRenderer.sprite = sprites[1];
        animator.SetInteger("shardsIn", 1);
        DataStorage.saveValues["sunShardsInserted"] = 1;
    }
}
