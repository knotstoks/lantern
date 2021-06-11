using UnityEngine;

public class WaterArm : Arm {
    [SerializeField] private GameObject puddle;
    public override void SpecialAttack() {
        StartCoroutine(puddle.GetComponent<PuddleAttackFourArms>().Attack());
    }
}
