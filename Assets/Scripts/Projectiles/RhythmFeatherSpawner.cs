using System.Collections;
using UnityEngine;

public class RhythmFeatherSpawner : MonoBehaviour {
    [SerializeField] private GameObject rhythmFeather;
    private float shootTime;
    private IEnumerator Start() {
        yield return new WaitForSeconds(1f);
        StartCoroutine(FirstWave()); //PUT COROUTINES HERE!
    }
    public void Activate(Vector2 spawnWhere, Vector2 shootWhere) {
        GameObject projectile = Instantiate(rhythmFeather, spawnWhere, Quaternion.identity);
        projectile.GetComponent<Feather>().endPosition = shootWhere;
        projectile.GetComponent<Rigidbody2D>().velocity = new Vector2(shootWhere.x - spawnWhere.x, shootWhere.y - spawnWhere.y).normalized * 3;
    }
    private IEnumerator FirstWave() {
        Activate(new Vector2(0, 8), new Vector2(0, -1));
        yield return new WaitForSeconds(1f);
        Activate(new Vector2(-2, 8), new Vector2(-2, -1));
        Activate(new Vector2(2, 8), new Vector2(2, -1));
        yield return new WaitForSeconds(1f);
        Activate(new Vector2(-4, 8), new Vector2(-4, -1));
        Activate(new Vector2(4, 8), new Vector2(4, -1));
        Activate(new Vector2(-8, 0), new Vector2(1, 0));
        Activate(new Vector2(8, 0), new Vector2(-1, 0));
        yield return new WaitForSeconds(1f);
        // top and bottom row arrows LR
        Activate(new Vector2(-8, 6), new Vector2(1, 6));
        Activate(new Vector2(8, 6), new Vector2(-1, 6));
        Activate(new Vector2(-8, -6), new Vector2(1, -6));
        Activate(new Vector2(8, -6), new Vector2(-1, -6));
        yield return new WaitForSeconds(1f);
        Activate(new Vector2(-8, 4), new Vector2(1, 4));
        Activate(new Vector2(8, 4), new Vector2(-1, 4));
        Activate(new Vector2(-8, -4), new Vector2(1, -4));
        Activate(new Vector2(8, -4), new Vector2(-1, -4));
        yield return new WaitForSeconds(1f);
        // attack at player standing at orb UD
        Activate(new Vector2(-6, 4), new Vector2(-6, -1));
        Activate(new Vector2(6, 4), new Vector2(6, -1));
        Activate(new Vector2(-6, 4), new Vector2(-6, 1));
        Activate(new Vector2(6, 4), new Vector2(6, 1));
        Activate(new Vector2(0, 8), new Vector2(0, -1));
        yield return new WaitForSeconds(1f);
        // diagonal pathway
        Activate(new Vector2(-7, 8), new Vector2(-7, -1));
        Activate(new Vector2(-6, 9), new Vector2(-6, -1));
        Activate(new Vector2(-5, 10), new Vector2(-5, -1));
        // Activate(new Vector2(-4, 11), new Vector2(-4, -1));
        Activate(new Vector2(-3, 12), new Vector2(-3, -1));
        Activate(new Vector2(-2, 13), new Vector2(-2, -1));
        Activate(new Vector2(-1, 14), new Vector2(-1, -1));
        Activate(new Vector2(0, 15), new Vector2(0, -1));
        Activate(new Vector2(1, 16), new Vector2(1, -1));
        Activate(new Vector2(2, 17), new Vector2(2, -1));
        Activate(new Vector2(3, 18), new Vector2(3, -1));
        Activate(new Vector2(4, 19), new Vector2(4, -1));
        Activate(new Vector2(5, 20), new Vector2(5, -1));
        Activate(new Vector2(6, 21), new Vector2(6, -1));
        Activate(new Vector2(7, 22), new Vector2(7, -1));
        yield return new WaitForSeconds(2f);
        Activate(new Vector2(-7, -22), new Vector2(-7, 1));
        Activate(new Vector2(-6, -21), new Vector2(-6, 1));
        Activate(new Vector2(-5, -20), new Vector2(-5, 1));
        Activate(new Vector2(-4, -19), new Vector2(-4, 1));
        Activate(new Vector2(-3, -18), new Vector2(-3, 1));
        Activate(new Vector2(-2, -17), new Vector2(-2, 1));
        Activate(new Vector2(-1, -16), new Vector2(-1, 1));
        Activate(new Vector2(0, -15), new Vector2(0, 1));
        Activate(new Vector2(1, -14), new Vector2(1, 1));
        Activate(new Vector2(2, -13), new Vector2(2, 1));
        Activate(new Vector2(3, -12), new Vector2(3, 1));
        // Activate(new Vector2(4, -11), new Vector2(4, 1));
        Activate(new Vector2(5, -10), new Vector2(5, 1));
        Activate(new Vector2(6, -9), new Vector2(6, 1));
        Activate(new Vector2(7, -8), new Vector2(7, 1));
        // spawn < < <
        yield return new WaitForSeconds(5f);
        Activate(new Vector2(10, 3), new Vector2(-1, 3));
        Activate(new Vector2(9, 2), new Vector2(-1, 2));
        Activate(new Vector2(8, 1), new Vector2(-1, 1));
        Activate(new Vector2(8, -1), new Vector2(-1, -1));
        Activate(new Vector2(9, -2), new Vector2(-1, -2));
        Activate(new Vector2(10, -3), new Vector2(-1, -3));
    }
}
