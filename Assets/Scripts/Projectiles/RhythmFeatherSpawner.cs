using System.Collections;
using UnityEngine;

public class RhythmFeatherSpawner : MonoBehaviour {
    [SerializeField] private GameObject rhythmFeather;
    private float shootTime;
    private IEnumerator Start() {
        yield return new WaitForSeconds(1f);
        StartCoroutine(VShape()); //PUT COROUTINES HERE!
        yield return new WaitForSeconds(2f);
        StartCoroutine(OrbAttackLR());
        yield return new WaitForSeconds(2f);
        OrbAttackUD();
        yield return new WaitForSeconds(1f);
        StartCoroutine(DiagonalPathway1());
        yield return new WaitForSeconds(7f);
        RightToLeft3();
        LeftToRight3();
        yield return new WaitForSeconds(4f);
        CircularShotsDown();
        yield return new WaitForSeconds(4f);


    }
    public void Activate(Vector2 spawnWhere, Vector2 shootWhere) {
        GameObject projectile = Instantiate(rhythmFeather, spawnWhere, Quaternion.identity);
        projectile.GetComponent<Feather>().endPosition = shootWhere;
        projectile.GetComponent<Rigidbody2D>().velocity = new Vector2(shootWhere.x - spawnWhere.x, shootWhere.y - spawnWhere.y).normalized * 3;
    }
    private IEnumerator VShape() {
        // arrows downwards in a V shape
        Activate(new Vector2(0, 8), new Vector2(0, -1));
        yield return new WaitForSeconds(1f);
        Activate(new Vector2(-2, 8), new Vector2(-2, -1));
        Activate(new Vector2(2, 8), new Vector2(2, -1));
        yield return new WaitForSeconds(1f);
        Activate(new Vector2(-4, 8), new Vector2(-4, -1));
        Activate(new Vector2(4, 8), new Vector2(4, -1));
        Activate(new Vector2(-8, 0), new Vector2(1, 0));
        Activate(new Vector2(8, 0), new Vector2(-1, 0));
    }
    private IEnumerator OrbAttackLR() {
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
    }
    private void OrbAttackUD() {
        // attack at player standing at orb UD
        Activate(new Vector2(-6, 4), new Vector2(-6, -1));
        Activate(new Vector2(6, 4), new Vector2(6, -1));
        Activate(new Vector2(-6, 4), new Vector2(-6, 1));
        Activate(new Vector2(6, 4), new Vector2(6, 1));
        Activate(new Vector2(0, 8), new Vector2(0, -1));
    }
    private IEnumerator DiagonalPathway1() {
        // diagonal pathway v v v ^ ^ ^
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
    }
    private void RightToLeft3() {
        // spawn < < <
        Activate(new Vector2(10, 3), new Vector2(-1, 3));
        Activate(new Vector2(9, 2), new Vector2(-1, 2));
        Activate(new Vector2(8, 1), new Vector2(-1, 1));
        Activate(new Vector2(8, -1), new Vector2(-1, -1));
        Activate(new Vector2(9, -2), new Vector2(-1, -2));
        Activate(new Vector2(10, -3), new Vector2(-1, -3));
    }
    private void LeftToRight3() {
        // spawn > > >
        Activate(new Vector2(-10, 3), new Vector2(-1, 3));
        Activate(new Vector2(-9, 2), new Vector2(-1, 2));
        Activate(new Vector2(-8, 1), new Vector2(-1, 1));
        Activate(new Vector2(-8, -1), new Vector2(-1, -1));
        Activate(new Vector2(-9, -2), new Vector2(-1, -2));
        Activate(new Vector2(-10, -3), new Vector2(-1, -3));
    }
    private void CircularShotsDown() {
        // spawn circular shots downwards
        Activate(new Vector2(-8, 8), new Vector2(0, 0));
        Activate(new Vector2(-4, 10), new Vector2(0, 0));
        Activate(new Vector2(0, 12), new Vector2(0, 0));
        Activate(new Vector2(4, 10), new Vector2(0, 0));
        Activate(new Vector2(8, 8), new Vector2(0, 0));
    }
    private void CircularShotsUp() {
        //spawn circular shots upwards
        Activate(new Vector2(-8, -8), new Vector2(0, 0));
        Activate(new Vector2(-4, -10), new Vector2(0, 0));
        Activate(new Vector2(0, -12), new Vector2(0, 0));
        Activate(new Vector2(4, -10), new Vector2(0, 0));
        Activate(new Vector2(8, -8), new Vector2(0, 0));
    }

    private void BottomFromLR() {
        
    }
    
}
