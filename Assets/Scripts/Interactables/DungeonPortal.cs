using System.Collections;
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
        new Vector2(0, -4), //1.3
        new Vector2(7, -8), //Golem
        new Vector2(-0.5f, -5), //1.4
        new Vector2(23, -6.5f), //1.5
        new Vector2(-0.5f, -4), //1.6
        new Vector2(9.5f, -19.6f), //Four Arms
        new Vector2(-0.5f, -4), //1.7
        new Vector2(-0.5f, -28.5f), //1.8
        new Vector2(-5, -5), //1.9
        new Vector2(0, -2) //Final Boss
    };
    private static int[] facingDirections = {
        1, //1.1
        1, //1.2
        0, //1.3
        0, //Golem
        0, //1.4
        3, //1.5
        0, //1.6
        0, //Four Arms
        0, //1.7
        0, //1.8
        1, //1.9
        0  //Final Boss
    };
    private IEnumerator Start() {
        yield return new WaitForSeconds(1);
        int[] array = (int[]) DataStorage.saveValues["waxDungeonRandomArray"];
        nextRoom = (int) DataStorage.saveValues["waxDungeonRoom"] + 1;
        if (array[nextRoom] == 9) {
            nextScene = "WD1";
            pos = positions[0];
            facingWay = facingDirections[0];
        } else if (array[nextRoom] == 10) {
            nextScene = "WD2";
            pos = positions[1];
            facingWay = facingDirections[1];
        } else if (array[nextRoom] == 11) {
            nextScene = "WD3";
            pos = positions[2];
            facingWay = facingDirections[2];
        } else if (array[nextRoom] == 12) {
            nextScene = "BossGolem";
            pos = positions[3];
            facingWay = facingDirections[3];
        } else if (array[nextRoom] == 13) {
            nextScene = "WD4";
            pos = positions[4];
            facingWay = facingDirections[4];
        } else if (array[nextRoom] == 14) {
            nextScene = "WD5";
            pos = positions[5];
            facingWay = facingDirections[5];
        } else if (array[nextRoom] == 15) {
            nextScene = "WD6";
            pos = positions[6];
            facingWay = facingDirections[6];
        } else if (array[nextRoom] == 16) {
            nextScene = "BossFourArms";
            pos = positions[7];
            facingWay = facingDirections[7];
        } else if (array[nextRoom] == 17) {
            nextScene = "WD7";
            pos = positions[8];
            facingWay = facingDirections[8];
        } else if (array[nextRoom] == 18) {
            nextScene = "WD8";
            pos = positions[9];
            facingWay = facingDirections[9];
        } else if (array[nextRoom] == 19) {
            nextScene = "WD9";
            pos = positions[10];
            facingWay = facingDirections[10];
        } else if (array[nextRoom] == 20) {
            nextScene = "BossGabriel";
            pos = positions[11];
            facingWay = facingDirections[11];
        }
    }
    public override void Interact() {
        if (SceneManager.GetActiveScene().buildIndex == 8) {
            DataStorage.saveValues["timeTrialTime"] = 1200f;
        }
        DataStorage.saveValues["waxDungeonRoom"] = nextRoom;
        DataStorage.saveValues["facingDirection"] = facingWay;
        DataStorage.saveValues["position"] = pos;
        DataStorage.saveValues["currScene"] = nextScene;
        Debug.Log(nextScene);
        SceneManager.LoadScene("LoadingScreen");
    }
}