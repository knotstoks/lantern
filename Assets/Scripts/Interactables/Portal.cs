using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : Interactable {
    [SerializeField] private string nextScene;
    [SerializeField] private Vector2 position;

    public override void Interact() {
        DataStorage.position = position;
        SceneManager.LoadScene(nextScene);
    }
}
