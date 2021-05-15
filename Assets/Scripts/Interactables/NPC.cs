using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class NPC : Interactable {
    [SerializeField] private Dialogue[] dialogue;
    [SerializeField] private Dialogue[] repeatedDialogue;
    [SerializeField] private string reference; 
    private Player player;
    public bool repeat;
    public void Start() {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        repeat = false;
    }
    
    public override void Interact() {
        if (!player.inDialogue) {
            if (!repeat) {
                repeat = true;
                FindObjectOfType<DialogueManager>().StartDialogue(dialogue[(int) DataStorage.saveValues[reference]]);
            } else {
                FindObjectOfType<DialogueManager>().StartDialogue(repeatedDialogue[(int) DataStorage.saveValues[reference]]);
            }
        } else {
            FindObjectOfType<DialogueManager>().DisplayNextSentence();
        }
    }
}
