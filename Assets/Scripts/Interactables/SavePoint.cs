using UnityEngine;

public class SavePoint : Interactable {
    [SerializeField] private float posX;
    [SerializeField] private float posY;
    [SerializeField] private GameObject saveScreen;
    private bool open;

    public void Start() {
        posX = this.transform.position.x;
        posY = this.transform.position.y;
    }
    public override void Interact() {
        if (!open) {
            saveScreen.SetActive(true);
        } else {
            saveScreen.SetActive(false);
        }

        open = !open;
    }
}
