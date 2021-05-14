using System.Collections;
using UnityEngine;

//0 for tutorial to trigger, 1 for talking to the Senior Warden to fire at the dummies, 2 for finishing the tutorial
public class DojoTutorial : ManageScene {
    public GameObject portalOut;
    public bool triggerDummies; //boolean to see if dummies were triggered
    public GameObject dummy;
    public Vector2[] dummyPositions;
    private int numOfDummies;
    public GameObject seniorWardenFrankie;
    private Player player;
    private IEnumerator Start() {
        reference = "tutorialDojo";
        yield return 0.2;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        if (referenceInt == 0) {
            triggerDummies = false;
            portalOut.SetActive(false);
        } else {
            triggerDummies = true;
        }
    }

    private void Update() {
        if (numOfDummies == 0 && referenceInt == 1) {
            portalOut.SetActive(true);
            DataStorage.saveValues["tutorialDojo"] = 2;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player") {
            if (seniorWardenFrankie.GetComponent<NPC>().repeat && !triggerDummies) { //if talked to frankie and havent summoned dummies
                numOfDummies = 3;
                Instantiate(dummy, dummyPositions[0], Quaternion.identity);
                Instantiate(dummy, dummyPositions[1], Quaternion.identity);
                Instantiate(dummy, dummyPositions[2], Quaternion.identity);
                DataStorage.saveValues["tutorialDojo"] = 1;
                player.allowCombat = true;
            }
        }
    }
}
