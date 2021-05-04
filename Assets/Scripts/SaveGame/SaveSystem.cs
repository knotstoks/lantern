using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveSystem : MonoBehaviour {
    public void Save(int health, int maxHealth, Vector2 position) {
        PlayerPrefs.DeleteKey("health");
        PlayerPrefs.DeleteKey("maxHealth");
        PlayerPrefs.DeleteKey("positionX");
        PlayerPrefs.DeleteKey("positionY");
        PlayerPrefs.SetInt("health", health);
        PlayerPrefs.SetInt("maxHealth", maxHealth);
        PlayerPrefs.SetFloat("positionX", position.x);
        PlayerPrefs.SetFloat("positionY", position.y);
    }

    public void Load() {
        DataStorage.health = PlayerPrefs.GetInt("health");
        DataStorage.maxHealth = PlayerPrefs.GetInt("maxHealth");
        DataStorage.position = new Vector2(PlayerPrefs.GetFloat("positionX"), PlayerPrefs.GetFloat("positionY"));
    }
}
