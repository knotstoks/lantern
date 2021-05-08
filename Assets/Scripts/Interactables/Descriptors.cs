using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Descriptors : Interactable {
    [SerializeField] private string descriptorDesc;
    [SerializeField] private Dialogue noChangeDialogue;
    [SerializeField] private Dialogue[] changeDialogue;
    public bool changeOvertime;
    public string reference;
    private Player player;
    public void Start() {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        objDesc = descriptorDesc;
    }
    public override void Interact() {
        if (!changeOvertime) {
            if (!player.inDialogue) {
                FindObjectOfType<DialogueManager>().StartDialogue(noChangeDialogue);
            } else {
                FindObjectOfType<DialogueManager>().DisplayNextSentence();
            }
        } else {
            if (!player.inDialogue) {
                FindObjectOfType<DialogueManager>().StartDialogue(changeDialogue[(int) DataStorage.saveValues[reference]]);
            } else {
                FindObjectOfType<DialogueManager>().DisplayNextSentence();
            } 
        }
    }
}
