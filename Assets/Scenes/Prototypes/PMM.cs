using UnityEngine;
using UnityEngine.SceneManagement;

public class PMM : MonoBehaviour {
    public void QuitGame() {
        Application.Quit();
    }
    public void ToMainMenu() {
        SceneManager.LoadScene("MainMenu");
    }
}
