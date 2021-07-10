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
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        pauseMenu = GameObject.FindGameObjectWithTag("DialogueManager").GetComponent<PauseMenu>();
        cameraFollow = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraFollow>();
        timer.SetActive(false);
        yield return new WaitForSeconds(0.1f);
        if (SceneManager.GetActiveScene().buildIndex >= 9 && SceneManager.GetActiveScene().buildIndex <= 21) {
            if ((int) DataStorage.saveValues["timeTrial"] == 1) {
                timer.SetActive(true);
                timeLeft = (float) DataStorage.saveValues["timeTrialTime"];
            }
        }
        DataStorage.saveValues["timeTrialTime"] = 1200f;
    }
    private void Update() {
        if ((int) DataStorage.saveValues["timeTrial"] == 1) {
            if (!pauseMenu.isGamePaused && SceneManager.GetActiveScene().buildIndex >= 9 && SceneManager.GetActiveScene().buildIndex <= 21 && !player.inDialogue) {
                if ((float) DataStorage.saveValues["timeTrialTime"] <= 0) {
                    player.Damage(100);
                } else {
                    DataStorage.saveValues["timeTrialTime"] = (float) DataStorage.saveValues["timeTrialTime"] - Time.deltaTime;
                }
            }
        }

        if ((int) DataStorage.saveValues["timeTrial"] == 1) {
            if (SceneManager.GetActiveScene().buildIndex == 8) {
                mins.text = "20";
                seconds.text = "00";
                DataStorage.saveValues["timeTrialTime"] = 1200;
            } else if (SceneManager.GetActiveScene().buildIndex >= 9 && SceneManager.GetActiveScene().buildIndex <= 21) {
                mins.text = ((float) DataStorage.saveValues["timeTrialTime"] / 60).ToString();
                seconds.text = ((float) DataStorage.saveValues["timeTrialTime"] % 60).ToString();
            }
        }
    }
}