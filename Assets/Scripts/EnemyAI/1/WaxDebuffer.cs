using System.Collections;
using UnityEngine;

public class WaxDebuffer : MonoBehaviour {
    [SerializeField] private GameObject tear;
    [SerializeField] private float resetShootTime;
    [SerializeField] private float tearSpeed;
    [SerializeField] private Animator animator;
    private float shootTime;
    private void Start() {
        animator.SetBool("Crying", false);
        shootTime = resetShootTime;
    }
    private void FixedUpdate() {
        if (shootTime > 0) {
            shootTime -= Time.deltaTime;
        } else {
            StartCoroutine(Cry());
            shootTime = resetShootTime;
        }
    }
    private IEnumerator Cry() {
        animator.SetBool("Crying", true);
        yield return new WaitForSeconds(0.9f);
        GameObject projectile = Instantiate(tear, transform.position, Quaternion.identity) as GameObject;
        projectile.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -1) * tearSpeed;
        animator.SetBool("Crying", false);
    }
    public IEnumerator Death() {
        shootTime = 1000f;
        animator.SetTrigger("Death");
        yield return new WaitForSeconds(1.65f);
        Destroy(gameObject);
    }
}