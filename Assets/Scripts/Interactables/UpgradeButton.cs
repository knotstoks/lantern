using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeButton : MonoBehaviour {
    [SerializeField] private GameObject upgradeUIObject;
    [SerializeField] private int upgradeNumber;
    private UpgradeUI upgradeUI;
    private void Start() {
        upgradeUI = upgradeUIObject.GetComponent<UpgradeUI>();
    }
    public void ButtonUpgradeChanger(bool tog) {
        if (tog) {
            upgradeUI.ChangeUpgradeUsingButton(upgradeNumber);
        }
    }
}
