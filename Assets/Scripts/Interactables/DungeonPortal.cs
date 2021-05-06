using UnityEngine;
using UnityEngine.SceneManagement;

public class DungeonPortal : Interactable {
    [SerializeField] private string nextScene;
    [SerializeField] private Vector2 position;
    [SerializeField] private bool open;

    private void Start() {
        open = false;
    }

    public override void Interact() {
        if (open) {
            DataStorage.position = position;
            SceneManager.LoadScene(nextScene);
        }
    }

    public void OpenPortal() {
        open = true;
    }
}
