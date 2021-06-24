using System.Collections;
using UnityEngine;

public class RhythmFeatherSpawner : MonoBehaviour {
    [SerializeField] private GameObject rhythmFeather;
    private float shootTime;
    private IEnumerator Start() {
        yield return new WaitForSeconds(1f);
        StartCoroutine(Demo1()); //PUT COROUTINES HERE!
    }
    public void Activate(Vector2 spawnWhere, Vector2 shootWhere) {
        GameObject projectile = Instantiate(rhythmFeather, spawnWhere, Quaternion.identity);
        projectile.GetComponent<Feather>().endPosition = shootWhere;
        projectile.GetComponent<Rigidbody2D>().velocity = new Vector2(shootWhere.x - transform.position.x, shootWhere.y - transform.position.y).normalized * 2;
    }
    private IEnumerator Demo1() {
        Activate(new Vector2(-10, 0), new Vector2(1, 0));
        Activate(new Vector2(10, 0), new Vector2(-1, 0));
        yield return new WaitForSeconds(1f);
    }
}
