using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class LightOrbs : MonoBehaviour {
    [SerializeField] private GameObject chargedIcon;
    [SerializeField] private AudioClip audioClip;
    private Upgrades upgradeManager;
    private AudioSource audioSource;
    public int charge;
    private Light2D orbLight;
    private void Start() {
        upgradeManager = GameObject.FindGameObjectWithTag("Upgrades").GetComponent<Upgrades>();
        charge = 0;
        orbLight = GetComponent<Light2D>();
        audioSource = GetComponent<AudioSource>();
    }
    private void Update() {
        if (charge == 5) {
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
            audioSource.PlayOneShot(audioClip);
        }
    }
    private void ChargeOrb() {
        if (charge < 5) {
            charge++;
            upgradeManager.ChargeUpgradeBar();
        }
    }
    public void ResetOrb() {
        charge = 0;
    }
}