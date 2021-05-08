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
}
