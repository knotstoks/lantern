using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour {
    public int health;
    public float speed;
    public int damage;
    public Slider slider;
    public AudioSource hitAudioSource;
    protected SpriteRenderer spriteRenderer;
    protected void GetSprite() {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "Player") {
            other.gameObject.GetComponent<Player>().Damage(damage);
        }
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Bullet") {
            Rigidbody2D bullet = other.GetComponent<Rigidbody2D>();
            StartCoroutine(FlashRed());
        }
    }
    public void Damage(int damage) {
        health -= damage;
        slider.value = health;
        hitAudioSource.Play();
    }
    private IEnumerator FlashRed() {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.color = Color.white;
    }
}