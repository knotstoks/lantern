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
        PlayerPrefs.SetInt("introSceneDone", DataStorage.introSceneDone);
        PlayerPrefs.SetInt("tutorialDojo", DataStorage.tutorialDojo);
        PlayerPrefs.SetInt("progress", DataStorage.progress);
        PlayerPrefs.SetInt("completedDungeonOne", DataStorage.completedDungeonOne);
        PlayerPrefs.SetInt("completedDungeonTwo", DataStorage.completedDungeonTwo);
    }

    public void Load() {
        DataStorage.health = PlayerPrefs.GetInt("health");
        DataStorage.maxHealth = PlayerPrefs.GetInt("maxHealth");
        DataStorage.position = new Vector2(PlayerPrefs.GetFloat("positionX"), PlayerPrefs.GetFloat("positionY"));
        DataStorage.introSceneDone = PlayerPrefs.GetInt("introSceneDone");
        DataStorage.tutorialDojo = PlayerPrefs.GetInt("tutorialDojo");
        DataStorage.progress = PlayerPrefs.GetInt("progress");
        DataStorage.completedDungeonOne = PlayerPrefs.GetInt("completedDungeonOne");
        DataStorage.completedDungeonTwo = PlayerPrefs.GetInt("completedDungeonTwo");
    }
}
