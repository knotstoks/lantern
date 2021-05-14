using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public abstract class Interactable : MonoBehaviour {
    [SerializeField] protected string objDesc;
    public abstract void Interact();

    public void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "Player") {
            other.gameObject.GetComponent<Player>().OpenInteractableIcon();
            other.gameObject.GetComponent<Player>().interactText.text = objDesc;
        }
    }

    public void OnCollisionExit2D(Collision2D other) {
        if (other.gameObject.tag == "Player") {
            other.gameObject.GetComponent<Player>().CloseInteractableIcon();
        }
    }
}