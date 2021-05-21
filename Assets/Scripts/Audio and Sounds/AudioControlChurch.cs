using System.Collections;
using UnityEngine;

public class AudioControlChurch : MonoBehaviour {
    [SerializeField] private AudioSource audioSource;
    private static AudioControlChurch instance;
    private IEnumerator Start() {
        yield return new WaitForSeconds(0.5f);
        audioSource.volume = PlayerPrefs.GetFloat("volume");
    }
    private void Awake() {
        if (instance != null) {
            Destroy(gameObject);
        } else {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
}
