using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour {
    public Text nameText;
    public Text dialogueText;
    public Text interactText;
    private Queue<string> names;
    private Queue<string> sentences;
    private Player player;
    private void Start() {
        sentences = new Queue<string>();
        names = new Queue<string>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    public void StartDialogue(Dialogue dialogue) {
        player.interactIcon.enabled = false;
        player.interactText.enabled = false;
        player.ToggleDialogue();

        sentences.Clear();
        names.Clear();

        foreach(string sentence in dialogue.sentences) {
            sentences.Enqueue(sentence);
        }

        foreach(string name in dialogue.names) {
            names.Enqueue(name);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence() {
        if (sentences.Count == 0) {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        string name = names.Dequeue();
        dialogueText.text = sentence;
        nameText.text = name;
    }

    private void EndDialogue() {
        player.ToggleDialogue();
        player.dialogueBox.enabled = false;
        player.dialogueText.SetActive(false);
        player.dialogueImage.enabled = false;
        player.interactIcon.enabled = false;
        player.interactName.SetActive(false);
        player.interactText.enabled = true;
    }
}