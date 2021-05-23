using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour {
    public int health;
    public float speed;
    public int damage;
    public void Damage(int damage) {
        health -= damage;
    }
}
