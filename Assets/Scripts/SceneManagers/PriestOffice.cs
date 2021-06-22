﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PriestOffice : MonoBehaviour {
    [SerializeField] private GameObject headPriest;
    [SerializeField] private GameObject blessingMenu;
    [SerializeField] private GameObject blessingOrb;
    [SerializeField] private Dialogue blessingDialogue;
    [SerializeField] private GameObject blessingInstructions;
    private Player player;
    private DialogueManager dialogueManager;
    private int line;
    private bool showingBlessingInstructions;
    private IEnumerator Start() {        
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
    }
    private void Update() {
        //Blessing Tutorial
        if ((int) DataStorage.saveValues["blessings"] == 1 && Input.GetKeyDown(KeyCode.E)) {
            if (line == 7) {
                StartCoroutine(ShowBlessingInstructions());
                dialogueManager.DisplayNextSentence();
                line++;
            } else if (line == blessingDialogue.names.Length - 1) {
                DataStorage.saveValues["blessings"] = 2;
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
        }
    }
    private IEnumerator ShowBlessingInstructions() {
        blessingInstructions.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        showingBlessingInstructions = true;
    }
    private void StartBlessingTutorial() {
        dialogueManager.StartDialogue(blessingDialogue);
    }
}