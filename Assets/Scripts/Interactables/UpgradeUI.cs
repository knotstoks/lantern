using UnityEngine;
using UnityEngine.EventSystems;
public class UpgradeUI : Interactable {
    [SerializeField] private GameObject upgradeMenu;
    [SerializeField] private GameObject firstBlessingButtonSelected;
    private Player player;
    private Upgrades upgradeManager;
    private Blacksmith blacksmith;
    private DialogueManager dialogueManager;
    private bool isOpen;
    private void Start() {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        dialogueManager = GameObject.FindGameObjectWithTag("DialogueManager").GetComponent<DialogueManager>();
        upgradeManager = GameObject.FindGameObjectWithTag("Upgrades").GetComponent<Upgrades>();
        blacksmith = GameObject.FindGameObjectWithTag("SceneManager").GetComponent<Blacksmith>();
        upgradeMenu.SetActive(false);
        isOpen = false;
    }
    public override void Interact() {
        if (!isOpen) {
            ToggleUpgradeMenu();
        }
    }
    public void ChangeUpgradeUsingButton(int n) {
        if (upgradeManager.upgrade != n) {
            upgradeManager.ChangeUpgrade(n);
            StartCoroutine(blacksmith.DisplayChangeUpgrade(n));
        }

        ToggleUpgradeMenu();
    }
    private void ToggleUpgradeMenu() {
        isOpen = !isOpen;
        upgradeMenu.SetActive(isOpen);
        player.inDialogue = isOpen;
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(firstBlessingButtonSelected);
    }
}