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
        if (charge == 10) {
            orbLight.intensity = 100;
            chargedIcon.SetActive(true);
        } else {
            orbLight.intensity = 0f;
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