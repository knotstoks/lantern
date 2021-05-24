using UnityEngine;

public class SaveSystem : MonoBehaviour {
    private Player player;

    private void Start() {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }
    public void Save(float posX, float posY, int facingDirection, string currScene) {
        PlayerPrefs.SetInt("health", player.maxHealth);
        PlayerPrefs.SetInt("maxHealth", player.maxHealth);
        PlayerPrefs.SetFloat("positionX", posX);
        PlayerPrefs.SetFloat("positionY", posY);
        PlayerPrefs.SetString("currScene", currScene);
        PlayerPrefs.SetFloat("facingDirection", facingDirection);
        PlayerPrefs.SetInt("introSceneDone", (int) DataStorage.saveValues["introSceneDone"]);
        PlayerPrefs.SetInt("tutorialDojo", (int) DataStorage.saveValues["tutorialDojo"]);
        PlayerPrefs.SetInt("messHall", (int) DataStorage.saveValues["messHall"]);
        PlayerPrefs.SetInt("progress", (int) DataStorage.saveValues["progress"]);
        PlayerPrefs.SetInt("blessings", (int) DataStorage.saveValues["blessings"]);
        PlayerPrefs.SetInt("usedBlessings", (int) DataStorage.saveValues["usedBlessings"]);

        //For Wax Dungeon
        PlayerPrefs.SetInt("waxDungeonRoom", (int) DataStorage.saveValues["waxDungeonRoom"]);
        PlayerPrefs.SetInt("waxDungeonGolem", (int) DataStorage.saveValues["waxDungeonGolem"]);
        PlayerPrefs.SetInt("waxDungeonFourArms", (int) DataStorage.saveValues["waxDungeonFourArms"]);
        PlayerPrefs.SetInt("completedWaxDungeon", (int) DataStorage.saveValues["completedWaxDungeon"]);

    }
}