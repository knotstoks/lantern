using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/*
Handles Health and Knockback of Player
*/
public class Health : MonoBehaviour {

    [SerializeField] private int health;
    [SerializeField] private int maxHealth;
    [SerializeField] private Image[] hearts;
    [SerializeField] private Sprite fullHeart;
    [SerializeField] private Sprite halfHeart;
    [SerializeField] private Sprite emptyHeart;
    [SerializeField] private float resetInvulTime;
    public float invulTime; //if its > 0, cannot be damaged. < 0 can be damaged.
    


    void Start() {
        updateHealth();
        invulTime = 0.5f;
    }

    void Update() {
        if (invulTime > 0) {
            invulTime -= Time.deltaTime;
        }
    }

    private void updateHealth() {
        if (health > maxHealth) {
            health = maxHealth;
        }

        //Sets current Health
        for (int i = 0; i < hearts.Length; i++) {
            if ((i + 1) * 2 <= health) {
                hearts[i].sprite = fullHeart;
            } else if (i * 2 < health && health % 2 == 1) {
                hearts[i].sprite = halfHeart;
            } else {
                hearts[i].sprite = emptyHeart;
            }


            //Sets max Health
            if (i < maxHealth / 2) {
                hearts[i].enabled = true;
            } else {
                hearts[i].enabled = false;
            }
        }
    }

    public void Heal(int amt) {
        if (health + amt > maxHealth) {
            health = maxHealth;
        } else {
            health += amt;
        }
        updateHealth();
    }

    public void Damage(int amt) {
        if (invulTime < 0) {
            invulTime = resetInvulTime;
            health -= amt;
            updateHealth();
        }
    }
}
