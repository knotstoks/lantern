using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(LineRenderer))]
public class Supernova : MonoBehaviour {
    public int vertexCount = 40;
    public float lineWidth = 0.2f;
    public float radius;
    private LineRenderer lineRenderer;
    private void Awake() {
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
        float deltaTheta = (2f * Mathf.PI) / 40f;
        float theta = 0f;

        Vector3 oldPos = Vector3.zero;
        for (int i = 0; i < vertexCount + 1; i++) {
            Vector3 pos = new Vector2(radius * Mathf.Cos(theta), radius * Mathf.Sin(theta));
            Gizmos.DrawLine(oldPos, transform.position + pos);

            theta += deltaTheta;
        }
    }
}
