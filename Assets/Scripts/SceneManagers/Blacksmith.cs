using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Blacksmith : MonoBehaviour {
    [SerializeField] private GameObject canvas;
    [SerializeField] private Text upgradeText;
    private void Start() {
        canvas.SetActive(false);
        upgradeText.text = "";
    }
    public IEnumerator DisplayChangeUpgrade(string t) {
        upgradeText.text = t;
        canvas.SetActive(true);
        yield return new WaitForSeconds(2f);
        canvas.SetActive(false);
    }
}