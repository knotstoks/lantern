using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour {
    public int health;
    public float speed;
    public int damage;
    public Slider slider;
    public void Damage(int damage) {
        health -= damage;
        slider.value = health;
    }
}
