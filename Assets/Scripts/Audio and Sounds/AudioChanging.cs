using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioChanging : MonoBehaviour {
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip[] musicArray;
    [SerializeField] private int selection;
    private void Start() {
        selection = 0;
        audioSource.clip = musicArray[selection];
        audioSource.Play();
    }
    public void ChangeMusic(int n) {
        audioSource.Stop();
        selection = n;
        audioSource.clip = musicArray[selection];
        audioSource.Play();
    }
}
