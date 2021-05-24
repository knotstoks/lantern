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
        //Edit!!!!!!!!!!!!!!
        DataStorage.saveValues["currScene"] = "WaxDungeonIntro";
        DataStorage.saveValues["position"] = new Vector2(-9f, 0f);
        DataStorage.saveValues["facingDirection"] = 0;

        int[] randArray = new int[9]; //9 - 11, 12 - 14, 15 - 17 || Bosses: 18, 19, 20
        randArray[0] = Random.Range(9, 12);
        randArray[1] = Random.Range(9, 12);
        while (randArray[0] == randArray[1]) {
            randArray[1] = Random.Range(9,12);
        }
        randArray[2] = 18;
        randArray[3] = Random.Range(12, 15);
        randArray[4] = Random.Range(12, 15);
        while (randArray[3] == randArray[4]) {
            randArray[4] = Random.Range(12,15);
        }
        randArray[5] = 19;
        randArray[6] = Random.Range(9, 12);
        randArray[7] = Random.Range(9, 12);
        while (randArray[6] == randArray[7]) {
            randArray[7] = Random.Range(9,12);
        }
        randArray[8] = 20;
        DataStorage.saveValues["waxDungeonRandomArray"] = randArray;

        //Tracks if Player used Blessings
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