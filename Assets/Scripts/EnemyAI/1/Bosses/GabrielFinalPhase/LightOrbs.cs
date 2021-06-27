using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class LightOrbs : MonoBehaviour {
    [SerializeField] private GameObject chargedIcon;
    public int charge;
    private Light2D orbLight;
    private void Start() {
        charge = 0;
        orbLight = GetComponent<Light2D>();
    }
    private void Update() {
        if (charge == 0) {
            orbLight.intensity = 0;
        } else if (charge <= 5) {
            orbLight.intensity = 0.5f;
        } else {
            orbLight.intensity = 1;
        }

        if (charge == 10) {
            chargedIcon.SetActive(true);
        } else {
            chargedIcon.SetActive(false);
        }
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Bullet") {
            ChargeOrb();
        }
    }
    private void ChargeOrb() {
        if (charge < 10) {
            charge++;
        }
    }
    public void ResetOrb() {
        charge = 0;
    }
}