using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : Interactable {
    [SerializeField] private string nextScene;
    [SerializeField] private Vector2 position;
    [SerializeField] private int facingDirection;
    public override void Interact() {
        DataStorage.saveValues["position"] = position;
        DataStorage.saveValues["currScene"] = nextScene;
        DataStorage.saveValues["facingDirection"] = facingDirection;
        SceneManager.LoadScene(nextScene);
    }
}
