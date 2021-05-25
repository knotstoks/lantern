using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaxDungeonIntro : MonoBehaviour {
    [SerializeField] private Dialogue[] dialogues; //0 & 1 for tutorial (start and end), 2 for repeating it
    [SerializeField] private GameObject spawnling;
    private int reference;
    private bool inTutorial;
    private int line;
    private Player player;
    private DialogueManager dialogueManager;
    private void Start() {
        inTutorial = false;
        reference = (int) DataStorage.saveValues["completedWaxDungeon"];
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        dialogueManager = GameObject.FindGameObjectWithTag("DialogueManager").GetComponent<DialogueManager>();

        if (reference == 0) { //Trigger first time animation
            StartDungeonTutorial();
        }
    }
    private void Update() {
        if (inTutorial && Input.GetKeyDown(KeyCode.E)) {
            if (line == dialogues[0].names.Length) {

            }
        }

    }
    private void StartDungeonTutorial() {
        inTutorial = true;
        
    }
}
