using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour{
    private Player player;
    private bool isGamePaused; //boolean to check if game is paused
    private bool onQuitScreen; //boolean to check if player is on quit screen
    private void Start() {
        isGamePaused = false;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape) && player.canPause) {
            if (!isGamePaused && !onQuitScreen) {
                Pause();
            } else if (isGamePaused && !onQuitScreen) {
                Resume();
            }
        }
    }

    public void Pause() {
        isGamePaused = true;
        player.blackBackground.enabled = true;
        player.pauseMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Resume() {
        isGamePaused = false;
        player.blackBackground.enabled = false;
        player.pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    public void GoToQuitMenu() {
        player.pauseMenu.SetActive(false);
        player.quitMenu.SetActive(true);
        onQuitScreen = true;
    }

    //Function to quit game and go to Main Menu
    public void QuitGame() {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void BackToPauseMenu() {
        onQuitScreen = false;
        player.quitMenu.SetActive(false);
        player.pauseMenu.SetActive(true);
    }

}
