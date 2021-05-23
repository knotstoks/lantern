using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GolemBossRoom : MonoBehaviour {
    [SerializeField] private Dialogue introDialogue;
    [SerializeField] private Dialogue outroDialogue;
    [SerializeField] private Slider bossHPBar;
    private Animator bossAnimator;
    private bool introDone;
    private bool fightCompleted;
    private Player player;
    private DialogueManager dialogueManager;
    private int line;

    private IEnumerator Start() {
        introDone = false;
        fightCompleted = false;
        yield return 0.5;
        line = 0;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        dialogueManager = GameObject.FindGameObjectWithTag("DialogueManager").GetComponent<DialogueManager>();
        bossAnimator = GameObject.FindGameObjectWithTag("Boss").GetComponent<Animator>();
        StartGolemIntro();
    } 
    private void FixedUpdate() {
        if (!introDone && Input.GetKeyDown(KeyCode.E)) {
            if (line == 2) { //edit this number!!!!!!!!!!!!!!!!!!!!!!

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
            if (line == 2) { //edit this number!!!!!!!!!!!!!!!!!!!!!
                fightCompleted = false;
                dialogueManager.DisplayNextSentence();
            } else {
                line++;
                dialogueManager.DisplayNextSentence();
            }
        }
    }
    private void StartGolemIntro() {
        dialogueManager.StartDialogue(introDialogue);
    }
    private void StartBossFight() {
        //Set Golem Mini Boss Active with HP Bar
        bossHPBar.enabled = true;
        bossAnimator.SetTrigger("Start");

    }
    public void CompleteFight() {
        fightCompleted = true;
        dialogueManager.StartDialogue(outroDialogue);
    }
}
