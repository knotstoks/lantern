using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScreen : MonoBehaviour {
    private float waitTime;
    private IEnumerator Start() {
        waitTime = Random.Range(0.6f, 1.2f);
        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene((string) DataStorage.saveValues["currScene"]);
    }
}