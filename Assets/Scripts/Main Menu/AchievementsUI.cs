using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class AchievementsUI : MonoBehaviour {
    [SerializeField] private Text nameText;
    [SerializeField] private Text descText;
    private void Update() {
        if (EventSystem.current.currentSelectedGameObject.GetComponent<Achievement>() != null) {
            if (EventSystem.current.currentSelectedGameObject.GetComponent<Achievement>().gotIt) {
                nameText.text = EventSystem.current.currentSelectedGameObject.GetComponent<Achievement>().nameOfAchievement;
                descText.text = EventSystem.current.currentSelectedGameObject.GetComponent<Achievement>().description;
            } else {
                nameText.text = "???";
                descText.text = "This achievement is locked.";
            }
        } else {
            nameText.text = "";
            descText.text = "";
        }
    }
}
