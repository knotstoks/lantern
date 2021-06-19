using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GabrielBossRoom : MonoBehaviour {
    [SerializeField] private Dialogue[] introDialogues; //0 for haven't seen, 1 for seen
    [SerializeField] private Dialogue outroDialogue;
    private int line;
    private bool inIntro;
    private bool inOutro;
    private Player player;
    private DialogueManager dialogueManager;
    private Gabriel gabriel;
    private void Start() {
        line = 0;
        inIntro = false;
        inOutro = false;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        dialogueManager = GameObject.FindGameObjectWithTag("DialogueManager").GetComponent<DialogueManager>();
        gabriel = GameObject.FindGameObjectWithTag("Boss").GetComponent<Gabriel>();
        StartIntroDialogue();
    }
    private void Update() {
        if (inIntro && Input.GetKeyDown(KeyCode.E)) {
            if (line == introDialogues[(int) DataStorage.saveValues["waxDungeonGabriel"]].names.Length - 1) {
                inIntro = false;
                line = 0;
                dialogueManager.DisplayNextSentence();
                //Start Gabriel Boss Fight
                gabriel.start = true;
            } else {
                line++;
                dialogueManager.DisplayNextSentence();
            }
        }

        if (inOutro && Input.GetKeyDown(KeyCode.E)) {
            if (line == outroDialogue.names.Length - 1) {
                inOutro = false;
                line = 0;
                dialogueManager.DisplayNextSentence();
            } else {
                line++;
                dialogueManager.DisplayNextSentence();
            }
        }
    }
    private void StartIntroDialogue() {
        inIntro = true;
        dialogueManager.StartDialogue(introDialogues[(int) DataStorage.saveValues["waxDungeonGabriel"]]);
    }
    public void WinFight() {
        inOutro = true;
        dialogueManager.StartDialogue(outroDialogue);
    }
}