using UnityEngine;
using UnityEngine.SceneManagement;

public class DungeonPortal : Interactable {
    private Vector2 pos;
    private int facingWay;
    private string nextScene;
    private int nextRoom; //Keeps track what number room the player is on
    private static Vector2[] positions = {
        new Vector2(-9, 0), //1.1
        new Vector2(-9, 0), //1.2
        new Vector2(), //1.3
        new Vector2(7, -8), //Golem
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
        1, //1.1
        1, //1.2
        //1.3
        0 //Golem
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
        nextRoom = (int) DataStorage.saveValues["waxDungeonRoom"] + 1;

        if (array[nextRoom] == 18) { //Golem
            pos = positions[3];
            facingWay = facingDirections[3];
        } else if (array[nextRoom] == 19) { //FourArms
            pos = positions[3];
            facingWay = facingDirections[3];
        } else if (array[nextRoom] == 20) { //Final Boss
            pos = positions[3];
            facingWay = facingDirections[3];
        } else {
            pos = positions[3];
            facingWay = facingDirections[3];
        }

        if (array[nextRoom] == 9) {
            nextScene = "WD1";
        } else if (array[nextRoom] == 10) {
            nextScene = "WD2";
        } else if (array[nextRoom] == 11) {
            nextScene = "WD3";
        } else if (array[nextRoom] == 12) {
            nextScene = "BossGolem";
        } else if (array[nextRoom] == 13) {
            nextScene = "WD4";
        } else if (array[nextRoom] == 14) {
            nextScene = "WD5";
        } else if (array[nextRoom] == 15) {
            nextScene = "WD6";
        } else if (array[nextRoom] == 16) {
            nextScene = "WD1"; //edit!!!!!!!!!!!!
        } else if (array[nextRoom] == 17) {
            nextScene = "WD7";
        } else if (array[nextRoom] == 18) {
            nextScene = "WD8";
        } else if (array[nextRoom] == 19) {
            nextScene = "WD9";
        } else if (array[nextRoom] == 20) {
            nextScene = "WD1"; //edit!!!!!!!!!!!!!
        }
    }
    public override void Interact() {
        DataStorage.saveValues["waxDungeonRoom"] = nextRoom + 1;
        DataStorage.saveValues["facingDirection"] = facingWay;
        DataStorage.saveValues["position"] = pos;
        DataStorage.saveValues["currScene"] = nextScene;
        SceneManager.LoadScene("LoadingScreen");
    }
}