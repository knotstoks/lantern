using System.Collections;
using UnityEngine;

//0 for tutorial to trigger, 1 for talking to the Senior Warden to fire at the dummies, 2 for finishing the tutorial
public class DojoTutorial : MonoBehaviour {
    public GameObject portalOut;
    public bool triggerDummies; //boolean to see if dummies were triggered
    public GameObject dummy;
    public Vector2[] dummyPositions;
    public int numOfDummies;
    public GameObject seniorWardenFrankie;
    private Player player;
    private IEnumerator Start() {
        yield return 0.2;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        if ((int) DataStorage.saveValues["tutorialDojo"] == 0) {
            triggerDummies = false;
            portalOut.SetActive(false);
        } else {
            triggerDummies = true;
        }
    }
    private void Update() {
        //Just came in and talked to Frankie
        if ((int) DataStorage.saveValues["tutorialDojo"] == 0 && seniorWardenFrankie.GetComponent<NPC>().repeat) {
            DataStorage.saveValues["tutorialDojo"] = 1;
            numOfDummies = 3;
        }

        //Finish destroying the dummies
        if (numOfDummies == 0 && (int) DataStorage.saveValues["tutorialDojo"] == 1) {
            seniorWardenFrankie.GetComponent<NPC>().repeat = false;
            DataStorage.saveValues["tutorialDojo"] = 2;
        }

        //Finished the tutorial
        if (seniorWardenFrankie.GetComponent<NPC>().repeat && (int) DataStorage.saveValues["tutorialDojo"] == 2) {
            portalOut.SetActive(true);
            player.allowCombat = false;
            DataStorage.saveValues["progress"] = 1;
        }
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player") {
            if (!triggerDummies && (int) DataStorage.saveValues["tutorialDojo"] == 1) { //if talked to frankie and havent summoned dummies
                Instantiate(dummy, dummyPositions[0], Quaternion.identity);
                Instantiate(dummy, dummyPositions[1], Quaternion.identity);
                Instantiate(dummy, dummyPositions[2], Quaternion.identity);
                DataStorage.saveValues["tutorialDojo"] = 1;
                triggerDummies = true;
                player.allowCombat = true;
            }
        }
    }
}