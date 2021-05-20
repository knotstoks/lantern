using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlessingsUI : MonoBehaviour {
    [SerializeField] private GameObject blessingMenu;
    private bool isOpen;
    private Player player;

    private void Start() {
        blessingMenu.SetActive(false); // will disable the blessingMenu when u enter e scene
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
        ToggleBlessingMenu();
    }
    
    private void Update() {
        if (isOpen && Input.GetKeyDown(KeyCode.Escape)) {
            ToggleBlessingMenu();
        }
        if (Input.GetKeyDown(KeyCode.X)) {
            ToggleBlessingMenu();
        }
    }
}