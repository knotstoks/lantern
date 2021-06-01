using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Upgrades : MonoBehaviour { //0 for No Upgrade, 1 for Vampric Embrace, 2 for Fleet Foot, 3 for Nova Impact
    [SerializeField] private GameObject superNova;
    [SerializeField] private Slider slider;
    public int upgrade;
    private int upgradeBar;
    private int[] progressNeeded = {
        50, 20, 30
    };
    private Player player;
    private void Start() {
        player = GetComponentInParent<Player>();
        slider.GetComponent<Slider>().gameObject.SetActive(false);
        upgrade = (int) DataStorage.saveValues["upgrade"];
        upgradeBar = (int) DataStorage.saveValues["upgradeBar"];

        if ((int) DataStorage.saveValues["blacksmith"] == 3) {
            if (upgrade != 0) {
                slider.GetComponent<Slider>().gameObject.SetActive(true);
            }
            slider.value = upgradeBar;
        }
    }
    private void Update() {
        if (upgradeBar == progressNeeded[upgrade - 1] && Input.GetKeyDown(KeyCode.Space) && upgrade != 0) {
            DoUpgrade();
        }
    }
    public void ChangeUpgrade(int n) {
        if (n != 0) {
            if (upgrade == 0) {
                slider.GetComponent<Slider>().gameObject.SetActive(true);
            }
            upgradeBar = 0;
            DataStorage.saveValues["upgradeBar"] = 0;
            upgrade = n;
            DataStorage.saveValues["upgrade"] = n;
            slider.maxValue = progressNeeded[n];
        } else {
            upgrade = n;
            DataStorage.saveValues["upgrade"] = n;
            slider.GetComponent<Slider>().gameObject.SetActive(false);
        }
    }
    public void ChargeUpgradeBar() {
        if (upgrade != 0) {
            if (upgradeBar < progressNeeded[upgrade]) {
                upgradeBar++;
                DataStorage.saveValues["upgradeBar"] = upgradeBar;
            }
        }
    }
    public void DoUpgrade() {
        upgradeBar = 0;
        DataStorage.saveValues["upgradeBar"] = upgradeBar;

        if (upgrade == 1) {
            StartCoroutine(VampricEmbrace());
        } else if (upgrade == 2) {
            StartCoroutine(FleetFoot());
        } else if (upgrade == 3) {
            NovaImpact();
        }
    }
    public void LoseProgress() {
        upgradeBar -= 10;
        DataStorage.saveValues["upgradeBar"] = upgradeBar;
    }
    private IEnumerator VampricEmbrace() {
        player.Heal(2);
        player.spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.5f);
        player.spriteRenderer.color = Color.white;
    }
    private IEnumerator FleetFoot() {
        player.speed = 5;
        yield return new WaitForSeconds(5f);
        player.speed = 3;
    }
    private void NovaImpact() {
        Instantiate(superNova, transform.position, Quaternion.identity);
    }
}
