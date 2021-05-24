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
    public void MoveToScene(string sceneName) {
        Debug.Log(sceneName);
        DataStorage.saveValues["currScene"] = sceneName;
        SceneManager.LoadScene("LoadingScreen");
    }
    public override void Interact() {
        if (!isOpen) {
            ToggleFastTravel();
        }
    }
}