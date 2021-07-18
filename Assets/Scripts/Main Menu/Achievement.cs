using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Achievement : MonoBehaviour {
    public string reference;
    public string nameOfAchievement;
    public string description;
    public Sprite[] sprites; //0 for not completed, 1 for completed
    public bool gotIt;
    public Image boxAround;
    private Image image;
    private void Start() {
        image = GetComponent<Image>();
        gotIt = PlayerPrefs.HasKey(reference);

        if (gotIt) {
            image.sprite = sprites[1];
        } else {
            image.sprite = sprites[0];
        }
    }
    private void Update() {
        if (EventSystem.current.currentSelectedGameObject == gameObject) {
            boxAround.enabled = true;
        } else {
            boxAround.enabled = false;
        }
    }
}
