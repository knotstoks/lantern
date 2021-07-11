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
        PlayerPrefs.SetInt("deaths", (int) DataStorage.saveValues["deaths"]);
        PlayerPrefs.SetInt("blacksmith", (int) DataStorage.saveValues["blacksmith"]);
        PlayerPrefs.SetInt("upgrade", (int) DataStorage.saveValues["upgrade"]);
        PlayerPrefs.SetInt("upgradeBar", (int) DataStorage.saveValues["upgradeBar"]);
        PlayerPrefs.SetInt("healAfterBosses", (int) DataStorage.saveValues["healAfterBosses"]);
        PlayerPrefs.SetString("currScene", currScene);
        PlayerPrefs.SetInt("facingDirection", facingDirection);
        PlayerPrefs.SetInt("introSceneDone", (int) DataStorage.saveValues["introSceneDone"]);
        PlayerPrefs.SetInt("tutorialDojo", (int) DataStorage.saveValues["tutorialDojo"]);
        PlayerPrefs.SetInt("messHall", (int) DataStorage.saveValues["messHall"]);
        PlayerPrefs.SetInt("progress", (int) DataStorage.saveValues["progress"]);
        PlayerPrefs.SetInt("blessings", (int) DataStorage.saveValues["blessings"]);
        PlayerPrefs.SetInt("usedBlessings", (int) DataStorage.saveValues["usedBlessings"]);

        //For Wax Dungeon
        PlayerPrefs.SetInt("waxDungeonRoom", (int) DataStorage.saveValues["waxDungeonRoom"]);
        PlayerPrefs.SetInt("waxDungeonGolem", (int) DataStorage.saveValues["waxDungeonGolem"]);
        PlayerPrefs.SetInt("savedWaxGolem", (int) DataStorage.saveValues["savedWaxGolem"]);
        PlayerPrefs.SetInt("waxDungeonFourArms", (int) DataStorage.saveValues["waxDungeonFourArms"]);
        PlayerPrefs.SetInt("savedFourArms", (int) DataStorage.saveValues["savedFourArms"]);
        PlayerPrefs.SetInt("waxDungeonGabriel", (int) DataStorage.saveValues["waxDungeonGabriel"]);
        PlayerPrefs.SetInt("completedWaxDungeon", (int) DataStorage.saveValues["completedWaxDungeon"]);

        //End Game
        PlayerPrefs.SetInt("finalBossBeatenCount", (int) DataStorage.saveValues["finalBossBeatenCount"]);
        PlayerPrefs.SetInt("completedReversedControls", (int) DataStorage.saveValues["completedReversedControls"]);
        PlayerPrefs.SetInt("completedBlackOut", (int) DataStorage.saveValues["completedBlackOut"]);
        PlayerPrefs.SetInt("completedTimeTrial", (int) DataStorage.saveValues["completedTimeTrial"]);
        PlayerPrefs.SetInt("introToEnd", (int) DataStorage.saveValues["introToEnd"]);
        PlayerPrefs.SetInt("newMission", (int) DataStorage.saveValues["newMission"]);
        PlayerPrefs.SetInt("sunShardsCollected", (int) DataStorage.saveValues["sunShardsCollected"]);
        PlayerPrefs.SetInt("sunShardsCollected", (int) DataStorage.saveValues["sunShardsInserted"]);
        PlayerPrefs.SetInt("introToTrials", (int) DataStorage.saveValues["introToTrials"]);
    }
}