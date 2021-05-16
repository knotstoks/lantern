using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SetVolume : MonoBehaviour
{
    public AudioMixer mixer;

    public void SetMasterVolume(float sliderValue) {
        AudioListener.volume = sliderValue;
        mixer.SetFloat("Master Volume", Mathf.Log10(sliderValue) * 20); // takes the sliderValue and turns it into a value on a log scale.
    }

    public void SetMusicVolume(float sliderValue)
    {
        mixer.SetFloat("Music Volume", Mathf.Log10(sliderValue) * 20); // takes the sliderValue and turns it into a value on a log scale.
    }

    public void SetSfxVolume(float sliderValue)
    {
        mixer.SetFloat("Sfx Volume", Mathf.Log10(sliderValue) * 20); // takes the sliderValue and turns it into a value on a log scale.
    }
}
