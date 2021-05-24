using System.Collections;
using UnityEngine;

public class BlessingsUI : Interactable {
    [SerializeField] private GameObject blessingMenu;
    private bool isOpen;
    private Player player;
    private void Start() {
        blessingMenu.SetActive(false); // will disable the blessingMenu when u enter the scene
        isOpen = false;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }
    private void ToggleBlessingMenu() { // just toggles true false
        isOpen = !isOpen;
        blessingMenu.SetActive(isOpen);
        player.inDialogue = isOpen;
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
    }
    public override void Interact() {
        if (!isOpen) {
            ToggleBlessingMenu();
        }
    }
}