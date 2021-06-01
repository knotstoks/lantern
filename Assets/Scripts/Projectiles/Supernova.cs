using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Supernova : MonoBehaviour {
    public int vertexCount = 40;
    public float lineWidth;
    public float radius;
    private LineRenderer lineRenderer;
    private void Start() {
        lineRenderer = GetComponent<LineRenderer>();
    }
    //     SetupCircle();
    //     StartCoroutine(DeathDelay());
    // }
    // private void SetupCircle() {
    //     lineRenderer.widthMultiplier = lineWidth;
    // }
    // private IEnumerator DeathDelay() {
    //     yield return new WaitForSeconds(2f);
    //     Destroy(gameObject);
    // }

    private void OnDrawGizmos() {
        // float deltaTheta = (2f * Mathf.PI) / 40f;
        float theta = 0f;

        Vector2 oldPos = Vector2.zero;
        for (int i = 0; i < vertexCount + 1; i++) {
            Vector2 pos = new Vector2(radius * Mathf.Cos(theta), radius * Mathf.Sin(theta));
            Gizmos.DrawLine(oldPos, (Vector2) transform.position + pos);

            theta += Time.deltaTime;
        }
    }
}
