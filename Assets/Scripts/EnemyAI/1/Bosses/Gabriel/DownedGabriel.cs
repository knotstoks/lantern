using UnityEngine;
using UnityEngine.SceneManagement;

public class DownedGabriel : Interactable {
    public override void Interact() {
        DataStorage.saveValues["position"] = new Vector2(); //EDIT!!!!!!!!!!
        DataStorage.saveValues["currScene"] = ""; //Final Boss Scene
        DataStorage.saveValues["facingDirection"] = 0;
        //Goes to cutscene with animation
        SceneManager.LoadScene(""); //EDIT!!!!!!!!!!!!!!
    }
}
