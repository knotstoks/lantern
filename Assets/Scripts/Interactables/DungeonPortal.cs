using UnityEngine;
using UnityEngine.SceneManagement;

public class DungeonPortal : Interactable {
    private Vector2 pos;
    private int facingWay;
    private string nextScene;
    private static Vector2[] positions = {
        new Vector2(), //1.1
        new Vector2(), //1.2
        new Vector2(), //1.3
        new Vector2(), //Golem
        new Vector2(), //1.4
        new Vector2(), //1.5
        new Vector2(), //1.6
        new Vector2(), //Four Arms
        new Vector2(), //1.7
        new Vector2(), //1.8
        new Vector2(), //1.9
        new Vector2() //Final Boss
    };
    private static int[] facingDirections = {
        //1.1
        //1.2
        //1.3
        //Golem
        //1.4
        //1.5
        //1.6
        //Four Arms
        //1.7
        //1.8
        //1.9
        //Final Boss
    };
    private void Start() {
        int[] array = (int[]) DataStorage.saveValues["waxDungeonRandomArray"];
        int currRoom = (int) DataStorage.saveValues["waxDungeonRoom"];

        if (array[currRoom] == 18) { //Golem
            pos = positions[3];
            facingWay = facingDirections[3];
        } else if (array[currRoom] == 19) { //FourArms
            pos = positions[3];
            facingWay = facingDirections[3];
        } else if (array[currRoom] == 20) { //Final Boss
            pos = positions[3];
            facingWay = facingDirections[3];
        } else {
            pos = positions[3];
            facingWay = facingDirections[3];
        }

        if (array[currRoom] == 9) {
            nextScene = "WD1.1";
        } else if (array[currRoom] == 10) {
            nextScene = "WD1.2";
        } else if (array[currRoom] == 11) {
            nextScene = "WD1.3";
        } else if (array[currRoom] == 12) {
            nextScene = "BossGolem";
        } else if (array[currRoom] == 13) {
            nextScene = "WD1.4";
        } else if (array[currRoom] == 14) {
            nextScene = "WD1.5";
        } else if (array[currRoom] == 15) {
            nextScene = "WD1.6";
        } else if (array[currRoom] == 16) {
            nextScene = "WD1.1"; //edit!!!!!!!!!!!!
        } else if (array[currRoom] == 17) {
            nextScene = "WD1.7";
        } else if (array[currRoom] == 18) {
            nextScene = "WD1.8";
        } else if (array[currRoom] == 19) {
            nextScene = "WD1.9";
        } else if (array[currRoom] == 20) {
            nextScene = "WD1.1"; //edit!!!!!!!!!!!!!
        }
    }
    public override void Interact() {
        DataStorage.saveValues["facingDirection"] = facingWay;
        DataStorage.saveValues["position"] = pos;
        DataStorage.saveValues["currScene"] = nextScene;
        SceneManager.LoadScene("LoadingScreen");
    }
}