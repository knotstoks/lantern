using UnityEngine;

public class SavePoint : Interactable {
    [SerializeField] private Sprite[] sprites; //0 for not activated, 1 for activated
    [SerializeField] private float posX;
    [SerializeField] private float posY;
    [SerializeField] private int facingDirection;
    [SerializeField] private string currScene;
    public bool activated;
    private SpriteRenderer spriteRenderer;
    private Player player;
    private void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        objDesc = "";
        spriteRenderer.sprite = sprites[0];
        activated = false;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }
    public override void Interact() {
        if (activated) {
            player.SaveGame(posX, posY, facingDirection, currScene);
        }
    }
    public void Activate() {
        objDesc = "Save Game";
        spriteRenderer.sprite = sprites[1];
        activated = true;
    }
}
