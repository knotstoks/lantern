using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TrialToggle : MonoBehaviour {
    [SerializeField] private string stringRef;
    [SerializeField] private string completedStringRef;
    [SerializeField] private Sprite[] sprites; //0 for unchecked, 1 for checked
    [SerializeField] private GameObject buttonObject;
    [SerializeField] private Sprite[] buttonSprites; //0 for not done, 1 for done
    private Toggle toggle;
    private Image image;
    private Image buttonImage;
    private void Start() {
        toggle = GetComponent<Toggle>();
        image = GetComponent<Image>();
        buttonImage = buttonObject.GetComponent<Image>();
    }
    private void Update() {
        if (!((int) DataStorage.saveValues[stringRef] == 1)) {
            image.sprite = sprites[0];
        } else {
            image.sprite = sprites[1];
        }
        
        if (!((int) DataStorage.saveValues[completedStringRef] == 1)) {
            buttonImage.sprite = buttonSprites[0];
        } else {
            buttonImage.sprite = buttonSprites[1];
        }
    }
    public void OnTargetToggleValueChanged(bool on) {
        if (on) {
            DataStorage.saveValues[stringRef] = 1;
        } else {
            DataStorage.saveValues[stringRef] = 0;
        }
    }
}
