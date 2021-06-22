using UnityEngine;

public class MainHall : MonoBehaviour {
    [SerializeField] private GameObject fastTravelUI;
    [SerializeField] private GameObject fastTravelDoor;
    [SerializeField] private GameObject lockedBlacksmith;
    [SerializeField] private GameObject blacksmithDoor;
    private void Start() {
        fastTravelUI.SetActive(false);

        if ((int) DataStorage.saveValues["progress"] < 2) {
            fastTravelDoor.SetActive(false);
            blacksmithDoor.SetActive(false);
        } else {
            fastTravelDoor.SetActive(true);
            lockedBlacksmith.SetActive(false);
        }
    }
}