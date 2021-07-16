using UnityEngine;

public class SunShardRoom : MonoBehaviour {
    [SerializeField] private GameObject talkingPriest;
    [SerializeField] private GameObject portalOut;
    [SerializeField] private GameObject[] finalPeople;
    private DialogueManager dialogueManager;
    private Player player;
    private void Start() {
        talkingPriest.SetActive(false);
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        dialogueManager = GameObject.FindGameObjectWithTag("DialogueManager").GetComponent<DialogueManager>();

        //Disable the way out and make priest appear
        if ((int) DataStorage.saveValues["introToEnd"] == 2) {
            talkingPriest.SetActive(true);
            portalOut.SetActive(false);
        }

        if ((int) DataStorage.saveValues["finishGame"] == 0) {
            for (int i = 0; i < finalPeople.Length; i++) {
                finalPeople[i].SetActive(false);
            }
        } else {
            for (int i = 0; i < finalPeople.Length; i++) {
                finalPeople[i].SetActive(true);
            }
        }
    }
}
