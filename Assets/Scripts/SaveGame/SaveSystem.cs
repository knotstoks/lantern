using UnityEngine;

public class SaveSystem : MonoBehaviour {
    private Player player;

    private void Start() {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }
    public void Save(float posX, float posY, float facingDirection, string currScene) {
        PlayerPrefs.SetInt("health", player.health);
        PlayerPrefs.SetInt("maxHealth", player.maxHealth);
        PlayerPrefs.SetFloat("positionX", posX);
        PlayerPrefs.SetFloat("positionY", posY);
        PlayerPrefs.SetFloat("facingDirection", facingDirection);
        PlayerPrefs.SetInt("introSceneDone", (int) DataStorage.saveValues["introSceneDone"]);
        PlayerPrefs.SetInt("tutorialDojo", (int) DataStorage.saveValues["tutorialDojo"]);
        PlayerPrefs.SetInt("progress", (int) DataStorage.saveValues["progress"]);
        PlayerPrefs.SetInt("completedWaxDungeon", (int) DataStorage.saveValues["completedWaxDungeon"]);
        PlayerPrefs.SetString("currScene", currScene);
    }
}