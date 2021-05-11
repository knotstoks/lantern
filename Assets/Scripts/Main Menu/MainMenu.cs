using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {
    private bool hasSaveGame;
    [SerializeField] private Button loadGameButton;
    [SerializeField] private GameObject standardSet;
    [SerializeField] private GameObject newGameWarning;
    [SerializeField] private GameObject quitScreen;
    [SerializeField] private GameObject optionsMenu;
    [SerializeField] private GameObject credits;
    [SerializeField] private Animator animator;
    private void Start() {
        standardSet.SetActive(true);
        hasSaveGame = PlayerPrefs.HasKey("savePresent");
        if (!hasSaveGame) {
            loadGameButton.enabled = false;
        }
        newGameWarning.SetActive(false);
        quitScreen.SetActive(false);
        optionsMenu.SetActive(false);
        credits.SetActive(false);
    }

    public void PlayGame() {
        if (!hasSaveGame) {
            RestartGame();
        } else {
            standardSet.SetActive(false);
            newGameWarning.SetActive(true);
        }
    }

    private void RestartGame() {
        StartCoroutine(ReallyRestartGame());
    }

    private IEnumerator ReallyRestartGame() {
        //Pickup Lantern
        animator.SetBool("playGame", true);

        yield return new WaitForSeconds(3);

        PlayerPrefs.SetInt("savePresent", 1);
        DataStorage.saveValues["health"] = 6;
        DataStorage.saveValues["maxHealth"] = 6;
        DataStorage.saveValues["position"] = new Vector2(0, 0);
        DataStorage.saveValues["currScene"] = "Bedroom";
        DataStorage.saveValues["introSceneDone"] = 0;
        DataStorage.saveValues["tutorialDojo"] = 0;
        DataStorage.saveValues["progress"] = 0;
        DataStorage.saveValues["completedDungeonOne"] = 0;
        DataStorage.saveValues["completedDungeonTwo"] = 0;

        SceneManager.LoadScene("IntroCutscene");
    }

    public void LoadGame() {
        StartCoroutine(ReallyLoadGame());
    }

    private IEnumerator ReallyLoadGame() {
        //Pickup Lantern
        animator.SetBool("playGame", true);

        yield return new WaitForSeconds(3);

        DataStorage.saveValues["health"] = PlayerPrefs.GetInt("health");
        DataStorage.saveValues["maxHealth"] = PlayerPrefs.GetInt("maxHealth");
        DataStorage.saveValues["position"] = new Vector2(PlayerPrefs.GetFloat("positionX"), PlayerPrefs.GetFloat("positionY"));
        DataStorage.saveValues["introSceneDone"] = PlayerPrefs.GetInt("introSceneDone");
        DataStorage.saveValues["tutorialDojo"] = PlayerPrefs.GetInt("tutorialDojo");
        DataStorage.saveValues["progress"] = PlayerPrefs.GetInt("progress");
        DataStorage.saveValues["completedDungeonOne"] = PlayerPrefs.GetInt("completedDungeonOne");
        DataStorage.saveValues["completedDungeonTwo"] = PlayerPrefs.GetInt("completedDungeonTwo");

        SceneManager.LoadScene(PlayerPrefs.GetString("currScene"));
    }

    public void QuitScreenActive() {
        standardSet.SetActive(false);
        quitScreen.SetActive(true);
    }

    public void QuitGame() {
        StartCoroutine(ReallyQuitGame());
    }

    private IEnumerator ReallyQuitGame() {
        yield return new WaitForSeconds(2);
        Application.Quit();
    }

    public void CloseNewGameWarning() {
        newGameWarning.SetActive(false);
        standardSet.SetActive(true);
    }

    public void CloseQuitScreen() {
        quitScreen.SetActive(false);
        standardSet.SetActive(true);
    }

    public void OpenCredits() {
        standardSet.SetActive(false);
        credits.SetActive(true);
    }

    public void CloseCredits() {
        credits.SetActive(false);
        standardSet.SetActive(true);
    }

    public void OpenOptions() {
        standardSet.SetActive(false);
        optionsMenu.SetActive(true);
    }

    public void CloseOptions() {
        optionsMenu.SetActive(false);
        standardSet.SetActive(true);
    }
}