using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class AudioControlMainMenu : MonoBehaviour {
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private Slider volumeSlider;

    private IEnumerator Start() {
        yield return new WaitForSeconds(0.5f);
        audioSource.volume = PlayerPrefs.GetFloat("volume");
    }

    //adjusts the volume on slider
    private void adjustVolume() {
        PlayerPrefs.SetFloat("volume", volumeSlider.value);
    }
}