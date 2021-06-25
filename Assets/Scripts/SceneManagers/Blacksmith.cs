using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Blacksmith : MonoBehaviour {
    [SerializeField] private GameObject canvas;
    [SerializeField] private Text upgradeText;
    private Player player;
    private float showTextTime;
    private string textToShow;
    private int currentUpgrade;
    private void Start() {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        upgradeText.text = "";
    }
    private void Update() {
        if (showTextTime <= 0) {
            upgradeText.text = "";
        } else {
            showTextTime -= Time.deltaTime;
            upgradeText.text = textToShow;
        }
    }
    public void DisplayChangeUpgrade(int n) {
        //Hardcoding the texts
        if (n == 0) {
            textToShow = "[Upgrade Removed]";
        } else if (n == 1) {
            textToShow = "[Upgraded] Vampric Embrace";
        } else if (n == 2) {
            textToShow = "[Upgraded] Fleet Foot";
        } else {
            textToShow = "[Upgraded] Nova Impact";
        }

        showTextTime = 2f;
        currentUpgrade = n;
    }
}