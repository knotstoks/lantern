using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Cutscene : MonoBehaviour {
    [SerializeField] private float timeOfScene;
    private IEnumerator Start() {
        yield return new WaitForSeconds(timeOfScene);
        SceneManager.LoadScene("SunShardRoom");
    }
}
