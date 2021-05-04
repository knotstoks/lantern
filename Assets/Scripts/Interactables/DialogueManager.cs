using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour {
    [SerializeField] private Text nameText;
    [SerializeField] private Text dialogueText;
    private Queue<string> sentences;
    private Player player;
    private void Start() {
        sentences = new Queue<string>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    public void StartDialogue(Dialogue dialogue) {
        player.ToggleDialogue();

        nameText.text = dialogue.name;

        sentences.Clear();

        foreach(string sentence in dialogue.sentences) {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence() {
        if (sentences.Count == 0) {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        dialogueText.text = sentence;
    }

    private void EndDialogue() {
        player.ToggleDialogue();
        player.dialogueBox.enabled = false;
        player.dialogueText.SetActive(false);
    }
}