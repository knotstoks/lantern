using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : Interactable {
    [SerializeField] private string nextScene;
    [SerializeField] private Vector2 position;
    [SerializeField] private int facingDirection;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip sfx;
    private void Start() {
        audioSource.loop = false;
        audioSource.clip = sfx;
    }
    public override void Interact() {
        DataStorage.saveValues["position"] = position;
        DataStorage.saveValues["currScene"] = nextScene;
        DataStorage.saveValues["facingDirection"] = facingDirection;
        audioSource.Play();
        StartCoroutine(Next());
    }
    private IEnumerator Next() {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("LoadingScreen");
    }
}
