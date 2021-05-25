using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GolemBossRoom : MonoBehaviour {
    [SerializeField] private Dialogue[] introDialogues;
    [SerializeField] private Dialogue outroDialogue;
    [SerializeField] private Slider bossHPBar;
    private bool introDone;
    private bool fightCompleted;
    private Golem golemBoss;
    private Player player;
    private DialogueManager dialogueManager;
    private int line;
    private int reference;

    private IEnumerator Start() {
        reference = (int) DataStorage.saveValues["waxDungeonGolem"];
        introDone = false;
        fightCompleted = false;
        yield return 0.5;
        line = 0;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        golemBoss = GameObject.FindGameObjectWithTag("Boss").GetComponent<Golem>();
        dialogueManager = GameObject.FindGameObjectWithTag("DialogueManager").GetComponent<DialogueManager>();
        StartGolemIntro();
    } 
    private void Update() {
        if (!introDone && Input.GetKeyDown(KeyCode.E)) {
            if (line == introDialogues[reference].names.Length) {
                introDone = true;
                line = 0;
                dialogueManager.DisplayNextSentence();
                StartBossFight();
            } else {
                line++;
                dialogueManager.DisplayNextSentence();
            }
        }

        if (fightCompleted && Input.GetKeyDown(KeyCode.E)) {
            if (line == outroDialogue.names.Length) { //edit this number!!!!!!!!!!!!!!!!!!!!!
                fightCompleted = false;
                dialogueManager.DisplayNextSentence();
            } else {
                line++;
                dialogueManager.DisplayNextSentence();
            }
        }
    }
    private void StartGolemIntro() {
        dialogueManager.StartDialogue(introDialogues[reference]);
    }
    private void StartBossFight() {
        //Set Golem Mini Boss Active with HP Bar
        bossHPBar.enabled = true;
        golemBoss.start = true;
        golemBoss.StartMoving();
    }
    public IEnumerator CompleteFight() {
        fightCompleted = true;
        bossHPBar.enabled = false;
        GameObject[] candlings = GameObject.FindGameObjectsWithTag("Enemy");
        for (int i = 0; i < candlings.Length; i++) {
            candlings[i].GetComponent<Candling>().Damage(100);
        }
        yield return new WaitForSeconds(5);
        dialogueManager.StartDialogue(outroDialogue);
    }
}
