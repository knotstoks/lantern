using UnityEngine;

public class SaveSystem : MonoBehaviour {

    private Player player;
    private SavePoint savePoint;

    private void Start() {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        savePoint = GameObject.FindGameObjectWithTag("SavePoint").GetComponent<SavePoint>();
    }
    public void Save() {
        player.Heal(player.maxHealth);

        PlayerPrefs.SetInt("health", player.maxHealth);
        PlayerPrefs.SetInt("maxHealth", player.maxHealth);
        PlayerPrefs.SetFloat("positionX", savePoint.posX);
        PlayerPrefs.SetFloat("positionY", savePoint.posY);
        PlayerPrefs.SetInt("introSceneDone", (int) DataStorage.saveValues["introSceneDone"]);
        PlayerPrefs.SetInt("tutorialDojo", (int) DataStorage.saveValues["tutorialDojo"]);
        PlayerPrefs.SetInt("progress", (int) DataStorage.saveValues["progress"]);
        PlayerPrefs.SetInt("completedDungeonOne", (int) DataStorage.saveValues["completedDungeonOne"]);
        PlayerPrefs.SetInt("completedDungeonTwo", (int) DataStorage.saveValues["completedDungeonTwo"]);
    }

    public void Load() {
        DataStorage.saveValues["health"] = PlayerPrefs.GetInt("health");
        DataStorage.saveValues["maxHealth"] = PlayerPrefs.GetInt("maxHealth");
        DataStorage.saveValues["position"] = new Vector2(PlayerPrefs.GetFloat("positionX"), PlayerPrefs.GetFloat("positionY"));
        DataStorage.saveValues["introSceneDone"] = PlayerPrefs.GetInt("introSceneDone");
        DataStorage.saveValues["tutorialDojo"] = PlayerPrefs.GetInt("tutorialDojo");
        DataStorage.saveValues["progress"] = PlayerPrefs.GetInt("progress");
        DataStorage.saveValues["completedDungeonOne"] = PlayerPrefs.GetInt("completedDungeonOne");
        DataStorage.saveValues["completedDungeonTwo"] = PlayerPrefs.GetInt("completedDungeonTwo");
    }
}
