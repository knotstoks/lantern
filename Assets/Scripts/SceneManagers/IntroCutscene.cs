using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class IntroCutscene : MonoBehaviour {
    [SerializeField] private Text skipText;
    private bool canSkip;
    void Start() {
        canSkip = true;
        skipText.enabled = true;
        StartCoroutine(DelayForVideo());
        StartCoroutine(FadeSkipText());
    }
    private void Update() {
        //Skip scene when 'Space' is pressed
        if (canSkip && Input.GetKeyDown(KeyCode.Space)) {
            SceneManager.LoadScene("Bedroom");
        }
    }
    //Coroutine to wait for the video to finish and then load the next scene
    private IEnumerator DelayForVideo() {
        yield return new WaitForSeconds(50); //Wait for cutscene to play out
        SceneManager.LoadScene("Bedroom");
    }
    //After 30s, fade the skip text away and disable skipping cutscene
    private IEnumerator FadeSkipText() {
        yield return new WaitForSeconds(108);
        skipText.enabled = false;
        canSkip = false;
    }
}