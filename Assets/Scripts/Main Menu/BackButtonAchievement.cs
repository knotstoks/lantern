using UnityEngine;
using UnityEngine.EventSystems;

public class BackButtonAchievement : MonoBehaviour {
[SerializeField] private GameObject whiteBox;
    private void Start() {
        whiteBox.SetActive(false);
    }
    private void Updated() {
        if (EventSystem.current.currentSelectedGameObject == this) {
            whiteBox.SetActive(true);
        } else {
            whiteBox.SetActive(false);
        }
    }
}
