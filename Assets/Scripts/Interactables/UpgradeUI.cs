using UnityEngine;
using UnityEngine.EventSystems;
public class UpgradeUI : MonoBehaviour {
    [SerializeField] private GameObject upgradeMenu;
    [SerializeField] private GameObject firstBlessingButtonSelected;
    [SerializeField] private GameObject enzioObject;
    [SerializeField] private GameObject[] infoPics;
    private Player player;
    private Upgrades upgradeManager;
    private Blacksmith blacksmith;
    private DialogueManager dialogueManager;
    private Enzio enzio;
    private bool isOpen;
    private void Start() {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        dialogueManager = GameObject.FindGameObjectWithTag("DialogueManager").GetComponent<DialogueManager>();
        upgradeManager = GameObject.FindGameObjectWithTag("Upgrades").GetComponent<Upgrades>();
        blacksmith = GameObject.FindGameObjectWithTag("SceneManager").GetComponent<Blacksmith>();
        upgradeMenu.SetActive(false);
        isOpen = false;
        enzio = enzioObject.GetComponent<Enzio>();
    }
    public void ChangeUpgradeUsingButton(int n) {
        if (upgradeManager.upgrade != n) {
            upgradeManager.ChangeUpgrade(n);
            blacksmith.DisplayChangeUpgrade(n);
        }
    }
    public void ToggleUpgradeMenu() {
        isOpen = !isOpen;
        upgradeMenu.SetActive(isOpen);
        player.inDialogue = isOpen;
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(firstBlessingButtonSelected);
        if (!isOpen) {
            enzio.menuOpen = false;
            player.inDialogue = false;
        }
    }
    public void OpenMenu() {
        ToggleUpgradeMenu();
    }
    public void ChangeImage(int n) {
        for (int i = 0; i < 4; i++) {
            if (i != n) {
                infoPics[i].SetActive(false);
            } else {
                infoPics[i].SetActive(true);
            }
        }
    }
}