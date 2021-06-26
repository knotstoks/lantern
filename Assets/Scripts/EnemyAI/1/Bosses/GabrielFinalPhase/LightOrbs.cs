using UnityEngine;

public class LightOrbs : MonoBehaviour {
    [SerializeField] private GameObject chargedIcon;
    public int charge;
    public bool orbCharged;
    private Light orbLight;
    private void Start() {
        charge = 0;
        orbLight = GetComponent<Light>();
    }
    private void Update() {
        if (charge == 0) {
            orbLight.intensity = 0;
        } else if (charge <= 10) {
            orbLight.intensity = 0.5f;
        } else if (charge <= 19) {
            orbLight.intensity = 1;
        } else {
            orbLight.intensity = 2;
        }

        if (charge == 20) {
            orbCharged = true;
            chargedIcon.SetActive(true);
        } else {
            orbCharged = false;
            chargedIcon.SetActive(false);
        }
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Bullet") {
            ChargeOrb();
        }
    }
    private void ChargeOrb() {
        if (charge < 20) {
            charge++;
        }
    }
    public void ResetOrb() {
        charge = 0;
    }
}
