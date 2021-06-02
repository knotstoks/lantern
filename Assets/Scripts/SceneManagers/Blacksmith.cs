using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Blacksmith : MonoBehaviour {
    [SerializeField] private GameObject canvas;
    [SerializeField] private Text upgradeText;
    private void Start() {
        upgradeText.text = "";
    }
    public IEnumerator DisplayChangeUpgrade(string t) {
        upgradeText.text = t;
        yield return new WaitForSeconds(2f);
        upgradeText.text = "";
    }
}