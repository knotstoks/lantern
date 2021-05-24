using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : Interactable {
    [SerializeField] private string nextScene;
    [SerializeField] private Vector2 position;
    [SerializeField] private int facingDirection;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip sfx;
    private Player player;
    private void Start() {
        audioSource.loop = false;
        audioSource.clip = sfx;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }
    public override void Interact() {
        DataStorage.saveValues["position"] = position;
        DataStorage.saveValues["currScene"] = nextScene;
        DataStorage.saveValues["facingDirection"] = facingDirection;
        audioSource.Play();
        StartCoroutine(Next());
    }
    private IEnumerator Next() {
        player.inDialogue = true;
        yield return new WaitForSeconds(1);
        player.inDialogue = false;
        SceneManager.LoadScene("LoadingScreen");
    }
}
