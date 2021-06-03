using UnityEngine;
using System.Collections;

//Parent class of Enemies which handles knockback and damage. 
//Movement and special abilities are settled by the different scripts.
public class Enemy : MonoBehaviour { 
    [SerializeField] protected int health;
    [SerializeField] protected float speed;
    [SerializeField] protected int damage;
    [SerializeField] protected AudioSource hitAudioSource;
    protected SpriteRenderer spriteRenderer;
    protected Upgrades upgradeManager;
    protected bool died;
    protected void Initialize() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        upgradeManager = GameObject.FindGameObjectWithTag("Upgrades").GetComponent<Upgrades>();
    }
    public void Damage(int damage) {
        if (!died) {
            upgradeManager.ChargeUpgradeBar();
            health -= damage;
            hitAudioSource.Play();
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Bullet") {
            Rigidbody2D bullet = other.GetComponent<Rigidbody2D>();
            StartCoroutine(FlashRed());
        }
    }
    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "Player") {
            other.gameObject.GetComponent<Player>().Damage(damage);
        }
    }
    private void OnCollisionStay2D(Collision2D other) {
        if (other.gameObject.tag == "Player") {
            other.gameObject.GetComponent<Player>().Damage(damage);
        }
    }
    private IEnumerator FlashRed() {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.color = Color.white;
    }
}
