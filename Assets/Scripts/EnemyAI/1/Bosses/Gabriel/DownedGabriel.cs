using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
/*
Animation References:
PunchFloor
TakeOutShard
Eat
Transformed
*/
public class DownedGabriel : Interactable {
    private bool pressed;
    private Animator animator;
    private GameObject player;
    private int line;
    private DialogueManager dialogueManager;
    [SerializeField] private Dialogue[] dialogues; //0 - 4
    private int[] takeOutShardNumber = {0, 0, 0, 0};
    private int[] eatNumber = {2, 3, 1, 1};
    private int[] transformedNumber = {4, 4, 2, 2};
    private bool moreThanThree;
    private void Start() {
        moreThanThree = false;
        pressed = false;
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        line = 0;
        dialogueManager = GameObject.FindGameObjectWithTag("DialogueManager").GetComponent<DialogueManager>();
    }
    public override void Interact() {
        if (!pressed) {
            pressed = true;
            animator.SetTrigger("PunchFloor");
            player.transform.position = new Vector2(0, -2);
            if ((int) DataStorage.saveValues["finalBossBeatenCount"] < 4) {
                dialogueManager.StartDialogue(dialogues[(int) DataStorage.saveValues["finalBossBeatenCount"]]);
            } else {
                dialogueManager.StartDialogue(dialogues[3]);
                moreThanThree = true;
            }
        }
    }
    private void Update() {
        if (!moreThanThree) {
            if (pressed && Input.GetKeyDown(KeyCode.E)) {
                if (line == dialogues[(int) DataStorage.saveValues["finalBossBeatenCount"]].names.Length - 1) {
                    animator.SetTrigger("Transformed");
                    pressed = false;
                    dialogueManager.DisplayNextSentence();
                    line = 0;
                    player.GetComponent<Player>().inDialogue = true;
                    StartCoroutine(EnterFinalPhase());
                    Debug.Log("end");
                } else if (line == takeOutShardNumber[(int) DataStorage.saveValues["finalBossBeatenCount"]]) {
                    animator.SetTrigger("TakeOutShard");
                    dialogueManager.DisplayNextSentence();
                    line++;
                    Debug.Log("take out");
                } else if (line == eatNumber[(int) DataStorage.saveValues["finalBossBeatenCount"]]) {
                    animator.SetTrigger("Eat");
                    dialogueManager.DisplayNextSentence();
                    line++;
                    Debug.Log("eat");
                } else {
                    dialogueManager.DisplayNextSentence();
                    line++;
                    Debug.Log("else");
                }
            }
        }

        if (moreThanThree) {
            if (pressed && Input.GetKeyDown(KeyCode.E)) {
                if (line == dialogues[3].names.Length - 1) {
                    animator.SetTrigger("Transformed");
                    pressed = false;
                    dialogueManager.DisplayNextSentence();
                    line = 0;
                    player.GetComponent<Player>().inDialogue = true;
                    StartCoroutine(EnterFinalPhase());
                } else if (line == takeOutShardNumber[3]) {
                    animator.SetTrigger("TakeOutShard");
                    dialogueManager.DisplayNextSentence();
                    line++;
                } else if (line == eatNumber[3]) {
                    animator.SetTrigger("Transformed");
                    dialogueManager.DisplayNextSentence();
                    line++;
                } else {
                    dialogueManager.DisplayNextSentence();
                    line++;
                }
            }
        }
    }
    private IEnumerator EnterFinalPhase() {
        yield return new WaitForSeconds(1);
        DataStorage.saveValues["position"] = new Vector2(0, 0);
        DataStorage.saveValues["facingDirection"] = 0;
        SceneManager.LoadScene("FinalBossGabriel");
    }
}
