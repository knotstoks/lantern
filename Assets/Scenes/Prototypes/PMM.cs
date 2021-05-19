using UnityEngine;
using UnityEngine.SceneManagement;

public class PMM : MonoBehaviour {
    public void PlayGame() {
        SceneManager.LoadScene("CombatPrototype");
    }

    public void QuitGame() {
        Application.Quit();
    }

    public void ToMainMenu() {
        SceneManager.LoadScene("PlayTestingMainMenu");
    }
}
