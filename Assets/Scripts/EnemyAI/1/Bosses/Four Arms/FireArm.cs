using UnityEngine;

public class FireArm : Arm {
    [SerializeField] private float minX, minY, maxX, maxY;
    [SerializeField] private GameObject fireCircle;
    public override void SpecialAttack() {
        for (int i = 0; i < 5; i++) {
            Instantiate(fireCircle, new Vector2(Random.Range(minX, maxX), Random.Range(minY,maxY)), Quaternion.identity);
        }
    }
}
