using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditScene : MonoBehaviour {
    public void BackToMainMenu() {
        SceneManager.LoadScene("MainMenu");
    }
}
