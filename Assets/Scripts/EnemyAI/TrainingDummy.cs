using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainingDummy : Enemy {
    [SerializeField] private Sprite[] damaged;
    private void Start() {
        GetSprite();
    }
    private void Update() {
        if (health == 4) {
            gameObject.GetComponent<SpriteRenderer>().sprite = damaged[0];
        } else if (health == 2) {
            gameObject.GetComponent<SpriteRenderer>().sprite = damaged[1];
        }

        if (health <= 0) {
            GameObject.FindGameObjectWithTag("SceneManager").GetComponent<DojoTutorial>().numOfDummies -= 1;
            Destroy(gameObject);
        }
    }
}
