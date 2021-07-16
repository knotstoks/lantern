using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BlessingsUI : Interactable {
    [SerializeField] private GameObject noBlessing;
    [SerializeField] private GameObject blessingMenu;
    [SerializeField] private Slider healSlider;
    [SerializeField] private Sprite[] heartContainers; //0 for empty, 1 for half, 2 for full
    [SerializeField] private Image[] hearts;
    private bool isOpen;
    private Player player;
    private void Start() {
        blessingMenu.SetActive(false); // will disable the blessingMenu when u enter the scene
        isOpen = false;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        healSlider.value = (int) DataStorage.saveValues["healAfterBosses"];
    }
    private void ToggleBlessingMenu() { // just toggles true false
        isOpen = !isOpen;
        blessingMenu.SetActive(isOpen);
        player.inDialogue = isOpen;
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(noBlessing);
    }   
    public void GiveBlessing(int n) {
        player.SetMaxHealth(n);
        DataStorage.saveValues["health"] = n;
        DataStorage.saveValues["maxHealth"] = n;
        ToggleBlessingMenu();
    }
    private void Update() {
        if (isOpen && Input.GetKeyDown(KeyCode.Escape)) {
            ToggleBlessingMenu();
        }

        for (int i = 0; i < hearts.Length; i++) {
            if ((i + 1) * 2 <= healSlider.value) {
                hearts[i].sprite = heartContainers[2];
            } else if (i * 2 < healSlider.value && healSlider.value % 2 == 1) {
                hearts[i].sprite = heartContainers[1];
            } else {
                hearts[i].sprite = heartContainers[0];
            }
        }
    }
    public override void Interact() {
        if (!isOpen) {
            ToggleBlessingMenu();
        }
    }
    public void ChangeHealAfterBosses() {
        DataStorage.saveValues["healAfterBosses"] = healSlider.value;
    }
}