using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthArm : Arm {
    private bool done = false;
    public override void SpecialAttack() {
        if (!done) {
            player.transform.position = new Vector2(9.5f, -18f);
            fourArms.blackHole.SetActive(true);
            done = true;
        }
    }
}
