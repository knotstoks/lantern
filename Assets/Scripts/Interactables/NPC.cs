using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class NPC : Interactable {
    [SerializeField] private string NPCDesc;
    [SerializeField] private Dialogue[] dialogue;
    [SerializeField] private string reference;
    private Player player;
    public void Start() {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        objDesc = NPCDesc;
    }
    
    public override void Interact() {
        if (!player.inDialogue) {
            FindObjectOfType<DialogueManager>().StartDialogue(dialogue[PlayerPrefs.GetInt(reference)]);
        } else {
            FindObjectOfType<DialogueManager>().DisplayNextSentence();
        }
    }
}
