using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChurchMusicDestroyer : MonoBehaviour {
    public List<string> sceneNames;
    public string instanceName;
    private void Start() {
        DontDestroyOnLoad(this.gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;
        AudioSource audioSource = GetComponent<AudioSource>();
        audioSource.volume = PlayerPrefs.GetFloat("volume");
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
        CheckForDuplicateInstances();
        CheckIfSceneInList();
    }

    private void CheckForDuplicateInstances() {
        ChurchMusicDestroyer[] collection = FindObjectsOfType<ChurchMusicDestroyer>();
        foreach (ChurchMusicDestroyer obj in collection) {
            if (obj != this) {
                if (obj.instanceName == instanceName) {
                    DestroyImmediate(obj.gameObject);
                }
            }
        }
    }

    void CheckIfSceneInList() {
        string currentScene = SceneManager.GetActiveScene().name;

        if (sceneNames.Contains(currentScene)) {
            //Do Nothing
        } else {
            SceneManager.sceneLoaded -= OnSceneLoaded;
            Destroy(this.gameObject);
        }
    }
}