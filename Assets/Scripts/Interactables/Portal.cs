using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : Interactable {
    [SerializeField] private string nextSceneDesc;
    [SerializeField] private string nextScene;
    [SerializeField] private Vector2 position;

    public void Start() {
        objDesc = nextSceneDesc;
    }
    public override void Interact() {
        DataStorage.saveValues["position"] = position;
        DataStorage.saveValues["currScene"] = nextScene;
        SceneManager.LoadScene(nextScene);
    }
}
