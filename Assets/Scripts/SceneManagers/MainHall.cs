using UnityEngine;

public class MainHall : MonoBehaviour {
    [SerializeField] private GameObject fastTravelUI;
    [SerializeField] private GameObject fastTravelDoor;
    [SerializeField] private GameObject lockedBlacksmith;
    [SerializeField] private GameObject blacksmithDoor;
    [SerializeField] private GameObject walkingPriest;
    [SerializeField] private GameObject[] objectsToDisable;
    [SerializeField] private GameObject portalToSunRoom;
    [SerializeField] private GameObject barricadedDoorDesc;
    [SerializeField] private GameObject barricadedDoorSprite;
    [SerializeField] private Sprite[] doorSprites; //0 for closed, 1 for open
    [SerializeField] private GameObject[] finalChars;
    private Player player;
    private void Start() {
        if ((int) DataStorage.saveValues["finishGame"] == 0) {
            for (int i = 0; i < finalChars.Length; i++) {
                finalChars[i].SetActive(true);
            }
        } else {
            for (int i = 0; i < finalChars.Length; i++) {
                finalChars[i].SetActive(true);
            }
        }

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        fastTravelUI.SetActive(false);

        if ((int) DataStorage.saveValues["progress"] < 2) {
            fastTravelDoor.SetActive(false);
            blacksmithDoor.SetActive(false);
        } else {
            fastTravelDoor.SetActive(true);
            lockedBlacksmith.SetActive(false);
        }

        //If in cutscene of walking priest
        if ((int) DataStorage.saveValues["introToEnd"] == 2) {
            for (int i = 0; i < objectsToDisable.Length; i++) {
                objectsToDisable[i].SetActive(false);
            }

            WalkingPriest();
        }

        if ((int) DataStorage.saveValues["introToEnd"] == 0) {
            portalToSunRoom.SetActive(false);
            barricadedDoorDesc.SetActive(true);
            barricadedDoorSprite.GetComponent<SpriteRenderer>().sprite = doorSprites[0];
        } else {
            portalToSunRoom.SetActive(true);
            barricadedDoorDesc.SetActive(false);
            barricadedDoorSprite.GetComponent<SpriteRenderer>().sprite = doorSprites[1];
        }
    }
    private void WalkingPriest() { //TDOO!!!!!!!!!!!!!!
        walkingPriest.SetActive(true);
    }
}