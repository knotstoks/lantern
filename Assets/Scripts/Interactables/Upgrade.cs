using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Upgrade : Interactable {
    [SerializeField] private int upgradeNumber;
    [SerializeField] private string upgradeTextDisplay;
    private Upgrades upgradeManager;
    private Blacksmith blacksmith;
    private DialogueManager dialogueManager;
    private void Start() {
        dialogueManager = GameObject.FindGameObjectWithTag("DialogueManager").GetComponent<DialogueManager>();
        upgradeManager = GameObject.FindGameObjectWithTag("Upgrades").GetComponent<Upgrades>();
        blacksmith = GameObject.FindGameObjectWithTag("SceneManager").GetComponent<Blacksmith>();
    }
    public override void Interact() {
        upgradeManager.ChangeUpgrade(upgradeNumber);
        blacksmith.DisplayChangeUpgrade(upgradeTextDisplay);
    }
}