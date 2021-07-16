using System.Collections;
using UnityEngine;
using UnityEngine.UI;
/*
    int golemSlain; 0 #

    int fourArmsSlain; 1 #

    int gabrielSlain; 2 #

    int reverseControlsTrialed; 3 #

    int blackOutTrialed; 4 #

    int timeTrialTrialed; 5 #

    int endGame; 6 #

    int noUpgradeRun; 7 #

    int threeHeartsRun; 8 #

    int noHeal; 9 #

    int hardMode; 10 #
 
    int kill100; 11 #

    int kill500; 12 #

    int died5; 13 #

    int died10; 14 #
*/

public class AchievementManager : MonoBehaviour {
    [SerializeField] private Sprite[] achievementSprites; //refer to above
    [SerializeField] private GameObject imageObject;
    [SerializeField] private Image image;
    [SerializeField] private GameObject text;
    [SerializeField] private GameObject everything;
    private Animator animator;
    private void Start() {
        imageObject.SetActive(false);
        text.SetActive(false);
        animator = everything.GetComponent<Animator>();
    }
    public void NewAchievement(int n) {
        StartCoroutine(DisplayAchievement(n));
    }
    private IEnumerator DisplayAchievement(int n) {
        image.sprite = achievementSprites[n];
        imageObject.SetActive(true);
        text.SetActive(true);

        animator.SetTrigger("In");

        yield return new WaitForSeconds(3);

        animator.SetTrigger("Out");

        yield return new WaitForSeconds(1);

        animator.SetTrigger("Done");

        imageObject.SetActive(false);
        text.SetActive(false);
    }
    public void CheckKills() {
        if (!PlayerPrefs.HasKey("mobKills")) {
            PlayerPrefs.SetInt("mobKills", 0);
        }

        PlayerPrefs.SetInt("mobKills", PlayerPrefs.GetInt("mobKills") + 1);

        if (PlayerPrefs.GetInt("mobKills") > 100) {
            if (!PlayerPrefs.HasKey("kill100")) {
                PlayerPrefs.SetInt("kill100", 1);
                StartCoroutine(DisplayAchievement(11));
            }
        }

        if (PlayerPrefs.GetInt("mobKills") > 500) {
            if (!PlayerPrefs.HasKey("kill500")) {
                PlayerPrefs.SetInt("kill500", 1);
                StartCoroutine(DisplayAchievement(12));
            }
        }
    }
}
