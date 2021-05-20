using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FastTravel : MonoBehaviour {
    [SerializeField] private GameObject fastTravelUI;
    private bool isOpen;

    private void Start() {
        fastTravelUI.SetActive(false);
        isOpen = false;
    }

    private void Update() {
        if (!isOpen && Input.GetKeyDown(KeyCode.X)) {
            ToggleFastTravel();
        }

        
        if (isOpen && Input.GetKeyDown(KeyCode.Escape)) {
            ToggleFastTravel();
        }
    }
    public void ToggleFastTravel() {
        isOpen = !isOpen;
        fastTravelUI.SetActive(isOpen);
    }

    public void MoveToScene(string sceneName) {
        Debug.Log(sceneName);
        // SceneManager.LoadScene(sceneName);
    }
}