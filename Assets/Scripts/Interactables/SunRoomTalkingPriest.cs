using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SunRoomTalkingPriest : Interactable {
    [SerializeField] private Dialogue dialogue;
    [SerializeField] private GameObject fadeObject;
    private FadeToWhite fadeToWhite;
    private DialogueManager dialogueManager;
    private bool talked;
    private bool delay;
    private int line;
    private SunShardRoom sceneManager;
    private Player player;
    private SunEmblem sunEmblem;
    private void Start() {
        delay = false;
        talked = false;
        line = 0;
        dialogueManager = GameObject.FindGameObjectWithTag("DialogueManager").GetComponent<DialogueManager>();
        sceneManager = GameObject.FindGameObjectWithTag("SceneManager").GetComponent<SunShardRoom>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        sunEmblem = GameObject.FindGameObjectWithTag("SunEmblem").GetComponent<SunEmblem>();
        fadeToWhite = fadeObject.GetComponent<FadeToWhite>();
    }
    public override void Interact() {
        if (!delay) {
            if (!talked) {
                talked = true;
                delay = true;
                player.inDialogue = true;
                StartCoroutine(PutIn());
            } else {
                if (line == dialogue.sentences.Length - 1) { //Delete priest and go to bed and save
                    DataStorage.saveValues["introToEnd"] = 3;
                    DataStorage.saveValues["introToTrials"] = 1;
                    //player.SaveGame(3, -0.45f, 0, "Bedroom");
                    DataStorage.saveValues["position"] = new Vector2(3, -0.45f);
                    DataStorage.saveValues["facingDirection"] = 0;
                    DataStorage.saveValues["currScene"] = "Bedroom";
                    StartCoroutine(fadeToWhite.FadeNow());
                    StartCoroutine(GoSleep());
                } else { //continue dialogue
                    dialogueManager.DisplayNextSentence();
                    line++;
                }
            }
        }
    }
    private IEnumerator PutIn() {
        sunEmblem.PriestInserted();
        yield return new WaitForSeconds(1);
        player.inDialogue = false;
        dialogueManager.StartDialogue(dialogue);
        delay = false;
    }
    private IEnumerator GoSleep() {
        yield return new WaitForSeconds(1.6f);
        SceneManager.LoadScene("LoadingScreen");
    }
}
