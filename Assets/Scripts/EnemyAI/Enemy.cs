using UnityEngine;
using System.Collections;

//Parent class of Enemies which handles knockback and damage. 
//Movement and special abilities are settled by the different scripts.
public class Enemy : MonoBehaviour { 
    [SerializeField] protected int health;
    [SerializeField] protected float speed;
    [SerializeField] protected int damage;
    [SerializeField] protected float thrust;
    [SerializeField] protected float knockTime;
    [SerializeField] protected bool canPush;

    public void Damage(int damage) {
        health -= damage;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Bullet") {
            Rigidbody2D bullet = other.GetComponent<Rigidbody2D>();
            StartCoroutine(KnockCoroutine(bullet));
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "Player") {
            other.gameObject.GetComponent<Player>().Damage(damage);
        }

        if (other.gameObject.tag == "Enemy") {
            if (canPush) {
                StartCoroutine(PushAway(other.gameObject.GetComponent<Rigidbody2D>()));
            }
        }
    }

    private IEnumerator KnockCoroutine(Rigidbody2D bullet) {
        Rigidbody2D enemy = this.GetComponent<Rigidbody2D>();
        Vector2 forceDirection = transform.position - bullet.transform.position;
        Vector2 force = forceDirection.normalized * thrust;
        enemy.velocity = force;
        yield return new WaitForSeconds(knockTime);
        enemy.velocity = new Vector2();
    }

    private IEnumerator PushAway(Rigidbody2D other) {
        Rigidbody2D rb = this.GetComponent<Rigidbody2D>();
        Vector2 push = (gameObject.transform.position - other.transform.position).normalized;
        rb.velocity = push;
        yield return new WaitForSeconds(0.2f);
        rb.velocity = new Vector2();
    }
}
