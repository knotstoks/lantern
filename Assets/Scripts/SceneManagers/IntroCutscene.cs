using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroCutscene : MonoBehaviour {
    // Start is called before the first frame update
    void Start() {
        StartCoroutine(DelayForVideo());
    }

    private IEnumerator DelayForVideo() {
        yield return new WaitForSeconds(56);
        SceneManager.LoadScene("Bedroom");
    }
}
