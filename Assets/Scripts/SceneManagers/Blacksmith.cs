using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Blacksmith : MonoBehaviour {
    [SerializeField] private GameObject canvas;
    [SerializeField] private Text upgradeText;
    [SerializeField] private GameObject blacksmithEnzio;
    private Player player;
    private NPC enzio;
    private DialogueManager dialogueManager;
    private void Start() {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        dialogueManager = GameObject.FindGameObjectWithTag("DialogueManager").GetComponent<DialogueManager>();
        upgradeText.text = "";
    }
    private void Update() {
        if ((int) DataStorage.saveValues["blacksmith"] == 2 && enzio.repeat) {
            DataStorage.saveValues["blacksmith"] = 3;
        }
    }
    public IEnumerator DisplayChangeUpgrade(int n) {
        string t = "";
        if (n == 0) {
            t = "[Upgrade Removed]";
        } else if (n == 1) {
            t = "[Upgraded] Vampric Embrace";
        } else if (n == 2) {
            t = "[Upgraded] Fleet Foot";
        } else {
            t = "[Upgraded] Nova Impact";
        }


        upgradeText.text = t;
        yield return new WaitForSeconds(2f);
        upgradeText.text = "";
    }
}