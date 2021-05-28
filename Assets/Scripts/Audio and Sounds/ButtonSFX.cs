using UnityEngine;

public class ButtonSFX : MonoBehaviour {
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip hover;
    public void HoverSound() {
        audioSource.PlayOneShot(hover);
    }
}
