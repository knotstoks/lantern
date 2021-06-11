using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PriestOffice : MonoBehaviour {
    [SerializeField] private GameObject headPriest;
    [SerializeField] private GameObject blessingMenu;
    [SerializeField] private GameObject blessingOrb;
    [SerializeField] private Dialogue blessingDialogue;
    [SerializeField] private Dialogue blacksmithDialogue;
    [SerializeField] private GameObject blessingInstructions;
    private Player player;
    private DialogueManager dialogueManager;
    private int line;
    private bool showingBlessingInstructions;
    private IEnumerator Start() {
        // DataStorage.saveValues["progress"] = 3; //DELETE AFTER!!!!!!!!!
        // DataStorage.saveValues["blessings"] = 1; //DELETE AFTER!!!!
        
        blessingMenu.SetActive(false);
        blessingInstructions.SetActive(false);
        line = 0;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        dialogueManager = GameObject.FindGameObjectWithTag("DialogueManager").GetComponent<DialogueManager>();
        showingBlessingInstructions = false;
        yield return new WaitForSeconds(0.1f);

        if ((int) DataStorage.saveValues["progress"] == 0) {
            headPriest.SetActive(false);
        } else {
            headPriest.SetActive(true);
        }

        if ((int) DataStorage.saveValues["blessings"] == 0) {
            blessingOrb.SetActive(false);
        } else {
            blessingOrb.SetActive(true);
        }
        
        if ((int) DataStorage.saveValues["blessings"] == 1) {
            //Start Intro to Blessings cutscene
            StartBlessingTutorial();
        }

        if ((int) DataStorage.saveValues["deaths"] == 3) {
            if ((int) DataStorage.saveValues["blacksmith"] == 0) {
                StartBlacksmithTutorial();
                DataStorage.saveValues["blacksmith"] = 1;
            }
        }
    }
    private void Update() {
        //Blessing Tutorial
        if ((int) DataStorage.saveValues["blessings"] == 1 && Input.GetKeyDown(KeyCode.E)) {
            if (line == blessingDialogue.names.Length - 1) {
                Debug.Log("hey!");
                DataStorage.saveValues["blessings"] = 2;
                dialogueManager.DisplayNextSentence();
                StartCoroutine(ShowBlessingInstructions());
            } else {
                line++;
                dialogueManager.DisplayNextSentence();
            }
        }

        if ((int) DataStorage.saveValues["blacksmith"] == 1 && Input.GetKeyDown(KeyCode.E)) {
            if (line == blacksmithDialogue.names.Length - 1) {
                DataStorage.saveValues["blacksmith"] = 2;
                dialogueManager.DisplayNextSentence();
            } else {
                line++;
                dialogueManager.DisplayNextSentence();
            }
        }

        //Save game if finished tutorial and talked to priest
        if ((int) DataStorage.saveValues["progress"] == 1 && headPriest.GetComponent<NPC>().repeat) {
            DataStorage.saveValues["progress"] = 2;
            player.SaveGame(-11.2f, 2.2f, 3, "PriestOffice");
        }

        if (showingBlessingInstructions && Input.GetKeyDown(KeyCode.E)) {
            showingBlessingInstructions = false;
            blessingInstructions.SetActive(false);
            player.inDialogue = false;
        }
    }
    private IEnumerator ShowBlessingInstructions() {
        Debug.Log("this is fine");
        yield return new WaitForSeconds(0.5f);
        showingBlessingInstructions = true;
        blessingInstructions.SetActive(true);
        player.inDialogue = true;
    }
    private void StartBlessingTutorial() {
        dialogueManager.StartDialogue(blessingDialogue);
    }
    private void StartBlacksmithTutorial() {
        dialogueManager.StartDialogue(blacksmithDialogue);
    }
}