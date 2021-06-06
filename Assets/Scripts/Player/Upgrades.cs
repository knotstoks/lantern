using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class Upgrades : MonoBehaviour { //0 for No Upgrade, 1 for Vampric Embrace, 2 for Fleet Foot, 3 for Nova Impact
    [SerializeField] private GameObject superNova;
    [SerializeField] private Slider slider;
    [SerializeField] private Image sliderImage;
    [SerializeField] private Sprite[] sprites; //1 - 2 for VE, 3 - 4 for FF, 5 - 6 for NI
    public int upgrade;
    private int[] progressNeeded = {
        0, 50, 20, 30
    };
    private Player player;
    private IEnumerator Start() {
        yield return new WaitForSeconds(0.02f);
        player = GetComponentInParent<Player>();
        slider.GetComponent<Slider>().gameObject.SetActive(false);
        upgrade = (int) DataStorage.saveValues["upgrade"];

        if ((int) DataStorage.saveValues["blacksmith"] == 3) {
            if (upgrade != 0) {
                slider.GetComponent<Slider>().gameObject.SetActive(true);
            }
            slider.value = (int) DataStorage.saveValues["upgradeBar"];
            slider.maxValue = progressNeeded[upgrade];
        }
    }
    private void Update() {
        if ((int) DataStorage.saveValues["upgradeBar"] == progressNeeded[upgrade] && Input.GetKeyDown(KeyCode.Space) && upgrade != 0) {
            DoUpgrade();
            slider.value = 0;
            DataStorage.saveValues["upgradeBar"] = 0;
        }

        if ((int) DataStorage.saveValues["upgradeBar"] == progressNeeded[upgrade] && upgrade != 0) {
            sliderImage.sprite = sprites[upgrade * 2];
        } else if (upgrade != 0 && (int) DataStorage.saveValues["upgradeBar"] < progressNeeded[upgrade]) {
            sliderImage.sprite = sprites[upgrade * 2 - 1];
        }

        slider.value = (int) DataStorage.saveValues["upgradeBar"];
    }
    public void ChangeUpgrade(int n) {
        if (n != 0) {
            if (upgrade == 0) {
                slider.GetComponent<Slider>().gameObject.SetActive(true);
            }
            slider.maxValue = progressNeeded[n];
            DataStorage.saveValues["upgradeBar"] = 0;
            upgrade = n;
            DataStorage.saveValues["upgrade"] = n;
        } else {
            upgrade = n;
            DataStorage.saveValues["upgrade"] = n;
            slider.GetComponent<Slider>().gameObject.SetActive(false);
        }
    }
    public void ChargeUpgradeBar() {
        if (upgrade != 0) {
            if ((int) DataStorage.saveValues["upgradeBar"] < progressNeeded[upgrade]) {
                DataStorage.saveValues["upgradeBar"] = (int) DataStorage.saveValues["upgradeBar"] + 1;
                slider.value = (int) DataStorage.saveValues["upgradeBar"];
            }
        }
    }
    public void DoUpgrade() {
        DataStorage.saveValues["upgradeBar"] = 0;

        if (upgrade == 1) {
            StartCoroutine(VampricEmbrace());
        } else if (upgrade == 2) {
            StartCoroutine(FleetFoot());
        } else if (upgrade == 3) {
            NovaImpact();
        }
    }
    public void LoseProgress() {
        DataStorage.saveValues["upgradeBar"] = (int) DataStorage.saveValues["upgradeBar"] - 10;
        if ((int) DataStorage.saveValues["upgradeBar"] < 0) {
            DataStorage.saveValues["upgradeBar"] = 0;
        }
        slider.value = (int) DataStorage.saveValues["upgradeBar"];
    }
    private IEnumerator VampricEmbrace() {
        player.Heal(2);
        player.spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.5f);
        player.spriteRenderer.color = Color.white;
    }
    private IEnumerator FleetFoot() {
        player.isSpeeding = true;
        yield return new WaitForSeconds(5f);
        player.isSpeeding = false;
    }
    private void NovaImpact() {
        Instantiate(superNova, transform.position, Quaternion.identity);
    }
}
