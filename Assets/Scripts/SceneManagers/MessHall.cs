using UnityEngine;

public class MessHall : MonoBehaviour {
    [SerializeField] private GameObject[] finalChars;
    private void Start() {
        if ((int) DataStorage.saveValues["finishGame"] == 0) {
            for (int i = 0; i < finalChars.Length; i++) {
                finalChars[i].SetActive(true);
            }
        } else {
            for (int i = 0; i < finalChars.Length; i++) {
                finalChars[i].SetActive(false);
            }
        }
    }
}
