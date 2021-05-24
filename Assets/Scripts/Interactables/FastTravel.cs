using UnityEngine;
using UnityEngine.SceneManagement;

public class FastTravel : Interactable {
    [SerializeField] private GameObject fastTravelUI;
    private bool isOpen;
    private Player player;

    private void Start() {
        fastTravelUI.SetActive(false); // will disable the blessingMenu when u enter the scene
        isOpen = false;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }
    private void Update() {    
        if (isOpen && Input.GetKeyDown(KeyCode.Escape)) {
            ToggleFastTravel();
        }
    }
    public void ToggleFastTravel() {
        isOpen = !isOpen;
        fastTravelUI.SetActive(isOpen);
        player.inDialogue = isOpen;
    }
    public void MoveToWaxDungeon() {
        DataStorage.saveValues["currScene"] = "WD1.1";
        DataStorage.saveValues["position"] = new Vector2(-9f, 0f);
        DataStorage.saveValues["facingDirection"] = 1;
        if ((int) DataStorage.saveValues["maxHealth"] != 6 && (int) DataStorage.saveValues["usedBlessings"] == 0) {
            DataStorage.saveValues["usedBlessings"] = 1;
        }
        SceneManager.LoadScene("LoadingScreen");
    }
    public override void Interact() {
        if (!isOpen) {
            ToggleFastTravel();
        }
    }
}