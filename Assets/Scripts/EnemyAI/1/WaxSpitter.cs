using System.Collections;
using UnityEngine;

//Shoots a laser beam, mounted to a wall
public class WaxSpitter : MonoBehaviour {
    [SerializeField] private float resetSpitTime;
    [SerializeField] private Animator animator;
    [SerializeField] private int direction;
    [SerializeField] private float spitForTime;
    private float spitTime;


    void Start() {
        spitTime = resetSpitTime;
        animator.SetInteger("Direction", direction);
    }

    void Update() {
        if (spitTime > 0) {
            spitTime -= Time.deltaTime;
        } else {
            //Trigger the spit
            StartCoroutine(Spit());
            spitTime = resetSpitTime;
        }
    }
    private IEnumerator Spit() {
        animator.SetBool("Spit", true);
        yield return new WaitForSeconds(spitForTime);
        animator.SetBool("Spit", false);
    }
    public IEnumerator Death() {
        animator.SetTrigger("Death");
        yield return new WaitForSeconds(2.25f);
        Destroy(gameObject);
    }
}