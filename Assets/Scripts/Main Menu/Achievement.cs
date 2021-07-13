using UnityEngine;

public class Achievement : MonoBehaviour {
    public string reference;
    public string nameOfAchievement;
    public string description;
    public Sprite[] sprites; //0 for not completed, 1 for completed
    public bool gotIt;
    private SpriteRenderer spriteRenderer;
    private void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        gotIt = PlayerPrefs.HasKey(reference);

        if (gotIt) {
            spriteRenderer.sprite = sprites[1];
        } else {
            spriteRenderer.sprite = sprites[0];
        }
    }
}
