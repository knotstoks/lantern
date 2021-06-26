using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DownedGabriel : Interactable {
    private bool pressed;
    private Animator animator;
    private GameObject player;
    private void Start() {
        pressed = false;
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
    }
    public override void Interact() {
        if (!pressed) {
            pressed = true;
            player.transform.position = new Vector2(0, -2);
            //Plays the vomit animation
            animator.SetTrigger("Vomit");
            StartCoroutine(EnterFinalPhase());
        }
    }
    private IEnumerator EnterFinalPhase() {
        yield return new WaitForSeconds(8.8f);
        DataStorage.saveValues["position"] = new Vector2(); //EDIT!!!!!!!!!!
        DataStorage.saveValues["facingDirection"] = 0;
        SceneManager.LoadScene(""); //EDIT!!!!!!!!!!!
    }
}
