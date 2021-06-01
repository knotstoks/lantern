using UnityEngine;

public class MainHall : MonoBehaviour {
    [SerializeField] private GameObject fastTravelUI;
    [SerializeField] private GameObject fastTravelDoor;
    private void Start() {
        // DataStorage.saveValues["progress"] = 2;

        fastTravelUI.SetActive(false);

        if ((int) DataStorage.saveValues["progress"] < 2) {
            fastTravelDoor.SetActive(false);
        } else {
            fastTravelDoor.SetActive(true);
        }
    }
}