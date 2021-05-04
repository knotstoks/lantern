using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : Interactable {
    [SerializeField] private string nextScene;

    public override void Interact() {
        SceneManager.LoadScene(nextScene);
    }
}
