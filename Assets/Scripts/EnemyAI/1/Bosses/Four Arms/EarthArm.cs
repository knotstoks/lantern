using UnityEngine;

public class EarthArm : Arm {
    [SerializeField] private GameObject earthAreaObject;
    private EarthArea earthArea;
    private bool done = false;
    private void Start() {
        earthArea = earthAreaObject.GetComponent<EarthArea>();
    }
    public override void SpecialAttack() {
        if (!done) {
            if (!earthArea.playerIn) {
                player.transform.position = new Vector2(9.5f, -18f);
            }
            fourArms.blackHole.SetActive(true);
            done = true;
        }
    }
}
