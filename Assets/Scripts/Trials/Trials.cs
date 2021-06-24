using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Trials : MonoBehaviour {
    private Player player;
    private PauseMenu pauseMenu;
    private CameraFollow cameraFollow;
    private float timeLeft;
    [SerializeField] private GameObject timer;
    [SerializeField] private Text mins;
    [SerializeField] private Text seconds;
    private IEnumerator Start() {
        pauseMenu = GameObject.FindGameObjectWithTag("DialogueManager").GetComponent<PauseMenu>();
        cameraFollow = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraFollow>();
        timer.SetActive(false);
        yield return new WaitForSeconds(0.1f);
        if (SceneManager.GetActiveScene().buildIndex >= 9 && SceneManager.GetActiveScene().buildIndex <= 21) {
            cameraFollow.blackOut = (bool) DataStorage.saveValues["blackOut"];
            if ((bool) DataStorage.saveValues["timeTrial"]) {
                timer.SetActive(true);
                timeLeft = (float) DataStorage.saveValues["timeTrialTime"];
            }
        }
    }
    private void Update() {
        if ((bool) DataStorage.saveValues["timeTrial"]) {


            if (!pauseMenu.isGamePaused && SceneManager.GetActiveScene().buildIndex >= 9 && SceneManager.GetActiveScene().buildIndex <= 21) {
                if ((float) DataStorage.saveValues["timeTrialTime"] <= 0) {
                    player.Damage(100);
                } else {
                    DataStorage.saveValues["timeTrialTime"] = (float) DataStorage.saveValues["timeTrialTime"] - Time.deltaTime;
                }
            }
        }

        if ((bool) DataStorage.saveValues["timeTrial"]) {
            if (SceneManager.GetActiveScene().buildIndex == 8) {
                mins.text = "20";
                seconds.text = "00";
            } else if (SceneManager.GetActiveScene().buildIndex >= 9 && SceneManager.GetActiveScene().buildIndex <= 21) {
                mins.text = ((float) DataStorage.saveValues["timeTrialTime"] / 60).ToString();
                seconds.text = ((float) DataStorage.saveValues["timeTrialTime"] % 60).ToString();
            }
        }
    }
}