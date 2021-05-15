using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class NPC : Interactable {
    [SerializeField] private Dialogue[] dialogue;
    [SerializeField] private Dialogue[] repeatedDialogue;
    [SerializeField] private string reference;
    public Animator animator;
    public float[] defaultFacing;
    private Player player;
    [HideInInspector] public bool repeat;
    public void Start() {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        repeat = false;
        animator.SetFloat("FacingHori", defaultFacing[0]);
        animator.SetFloat("FacingVert", defaultFacing[1]);
    }
    
    public override void Interact() {
        FindObjectOfType<DialogueManager>().talkingTo = this;

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
