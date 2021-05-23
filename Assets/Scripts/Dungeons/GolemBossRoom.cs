using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GolemBossRoom : MonoBehaviour {
    [SerializeField] private Dialogue lines;
    [SerializeField] private Slider bossHPBar;
    private bool introDone;
    private Player player;
    private DialogueManager dialogueManager;
    private int line;

    private IEnumerator Start() {
        introDone = false;
        yield return 0.5;
        line = 0;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        dialogueManager = GameObject.FindGameObjectWithTag("DialogueManager").GetComponent<DialogueManager>();
        StartGolemIntro();
    } 

    private void FixedUpdate() {
        if (!introDone && Input.GetKeyDown(KeyCode.E)) {
            if (line == 2) {

                introDone = true;
                dialogueManager.DisplayNextSentence();
                //Set Golem Mini Boss Active with HP Bar
            } else {
                line++;
                dialogueManager.DisplayNextSentence();
            }
        }
    }

    private void StartGolemIntro() {
        dialogueManager.StartDialogue(lines);
    }
}
