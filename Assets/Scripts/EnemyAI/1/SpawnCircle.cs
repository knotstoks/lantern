using System.Collections;
using UnityEngine;

public class SpawnCircle : MonoBehaviour {
    public GameObject enemy;
    private IEnumerator Start() {
        yield return new WaitForSeconds(1.55f);
        Instantiate(enemy, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
