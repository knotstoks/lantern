using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GabrielFinalRoom : MonoBehaviour {
    [SerializeField] private GameObject orbOneObject, orbTwoObject, orbThreeObject, orbFourObject;
    [SerializeField] private Dialogue[] outroDialogues;
    [SerializeField] private GameObject sunShard;
    [SerializeField] private GameObject featherDestroyer;
    [SerializeField] private AudioClip[] audioClips; //0 for normal, 1 for done
    [SerializeField] private FadeToWhite fadeToWhite;
    private AudioSource audioSource;
    private LightOrbs orbOne, orbTwo, orbThree, orbFour;
    private GameObject playerObject;
    private Player player;
    private GabrielFinal gabrielFinal;
    private DialogueManager dialogueManager;
    private int line;
    private bool inOutro;
    private bool imprisoned;
    private void Start() {
        featherDestroyer.SetActive(false);
        imprisoned = false;
        inOutro = false;
        line = 0;
        playerObject = GameObject.FindGameObjectWithTag("Player");
        player = playerObject.GetComponent<Player>();
        dialogueManager = GameObject.FindGameObjectWithTag("DialogueManager").GetComponent<DialogueManager>();
        gabrielFinal = GameObject.FindGameObjectWithTag("Gabriel").GetComponent<GabrielFinal>();
        orbOne = orbOneObject.GetComponent<LightOrbs>();
        orbTwo = orbTwoObject.GetComponent<LightOrbs>();
        orbThree = orbThreeObject.GetComponent<LightOrbs>();
        orbFour = orbFourObject.GetComponent<LightOrbs>();
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = audioClips[0];
        audioSource.loop = true;
        audioSource.Play();
    }
    private void Update() {
        //Imprisons Gabriel once the Orbs are charged
        if (orbOne.charge == 5 && orbTwo.charge == 5 && orbThree.charge == 5 && orbFour.charge == 5 && !imprisoned) {
            imprisoned = true;
            StartCoroutine(ImprisonGabriel());
        }

        if (inOutro) {
            if (Input.GetKeyDown(KeyCode.E)) {
                if (line == outroDialogues[(int) DataStorage.saveValues["finalBossBeatenCount"]].sentences.Length - 1) {
                    line = 0;
                    inOutro = false;
                    Destroy(gabrielFinal.gameObject);
                    SpawnSunShard();
                    dialogueManager.DisplayNextSentence();
                    DataStorage.saveValues["finalBossBeatenCount"] = (int) DataStorage.saveValues["finalBossBeatenCount"] + 1;
                } else {
                    line++;
                    dialogueManager.DisplayNextSentence();
                }
            }
        }
    }
    private IEnumerator ImprisonGabriel() {
        //Imprison Gabriel
        gabrielFinal.Imprison();
        yield return new WaitForSeconds(15);
        //Reset the orbs
        orbOne.ResetOrb();
        orbTwo.ResetOrb();
        orbThree.ResetOrb();
        orbFour.ResetOrb();
        imprisoned = false;
        //Resets Gabriel
        gabrielFinal.Return();
    }
    public void FinishFight() {
        if (!PlayerPrefs.HasKey("gabrielSlain")) {
            PlayerPrefs.SetInt("gabrielSlain", 1);
            GameObject.FindGameObjectWithTag("AchievementManager").GetComponent<AchievementManager>().NewAchievement(2);
        }

        if (GameObject.FindGameObjectWithTag("Upgrades").GetComponent<Upgrades>().upgrade == 0) { 
            if (!PlayerPrefs.HasKey("noUpgradeRun")) {
                PlayerPrefs.SetInt("noUpgradeRun", 1);
                GameObject.FindGameObjectWithTag("AchievementManager").GetComponent<AchievementManager>().NewAchievement(7);
            }
        }

        if ((int) DataStorage.saveValues["maxHealth"] == 6) { 
            if (!PlayerPrefs.HasKey("threeHeartsRun")) {
                PlayerPrefs.SetInt("threeHeartsRun", 1);
                GameObject.FindGameObjectWithTag("AchievementManager").GetComponent<AchievementManager>().NewAchievement(8);
            }
        }

        if ((int) DataStorage.saveValues["healAfterBosses"] == 0) { 
            if (!PlayerPrefs.HasKey("noHeal")) {
                PlayerPrefs.SetInt("noHeal", 1);
                GameObject.FindGameObjectWithTag("AchievementManager").GetComponent<AchievementManager>().NewAchievement(9);
            }
        }

        if (GameObject.FindGameObjectWithTag("Upgrades").GetComponent<Upgrades>().upgrade == 0 && (int) DataStorage.saveValues["maxHealth"] == 6
            && (int) DataStorage.saveValues["healAfterBosses"] == 0) {
            if (!PlayerPrefs.HasKey("hardMode")) {
                PlayerPrefs.SetInt("hardMode", 1);
                GameObject.FindGameObjectWithTag("AchievementManager").GetComponent<AchievementManager>().NewAchievement(10);
            }  
        }

        player.Heal((int) DataStorage.saveValues["healAfterBosses"]);
        audioSource.clip = audioClips[1];
        audioSource.loop = false;
        audioSource.Play();
        featherDestroyer.SetActive(true);
        //Start the Outro Dialogue
        dialogueManager.StartDialogue(outroDialogues[(int) DataStorage.saveValues["finalBossBeatenCount"]]);
        inOutro = true;
        //Teleport Player to specific position
        gabrielFinal.gameObject.transform.position = new Vector2(0.57f, 9.08f);
        playerObject.transform.position = new Vector2(0, 0);
        //Remove the Orbs' Colliders
        orbOne.ResetOrb();
        orbTwo.ResetOrb();
        orbThree.ResetOrb();
        orbFour.ResetOrb();
        Destroy(orbOneObject.GetComponent<Collider2D>());
        Destroy(orbTwoObject.GetComponent<Collider2D>());
        Destroy(orbThreeObject.GetComponent<Collider2D>());
        Destroy(orbFourObject.GetComponent<Collider2D>());
        //Stop Player from attacking
        player.allowCombat = false;
    }
    //Test to see if the sun shard can spawn, if not just leave
    public void SpawnSunShard() {
        bool spawn = false;
        if ((int) DataStorage.saveValues["introToEnd"] == 0) { //First time
            if (!PlayerPrefs.HasKey("endGame")) {
                PlayerPrefs.SetInt("endGame", 1);
                GameObject.FindGameObjectWithTag("AchievementManager").GetComponent<AchievementManager>().NewAchievement(6);
            }
            DataStorage.saveValues["sunShardsCollected"] = (int) DataStorage.saveValues["sunShardsCollected"] + 1;
            spawn = true;
        }

        if ((int) DataStorage.saveValues["completedReversedControls"] == 0 && (int) DataStorage.saveValues["reversedControls"] == 1) {
            if (!PlayerPrefs.HasKey("reverseControlsTrialed")) {
                PlayerPrefs.SetInt("reverseControlsTrialed", 1);
                GameObject.FindGameObjectWithTag("AchievementManager").GetComponent<AchievementManager>().NewAchievement(3);
            }
            DataStorage.saveValues["completedReversedControls"] = 1;
            DataStorage.saveValues["sunShardsCollected"] = (int) DataStorage.saveValues["sunShardsCollected"] + 1;
            spawn = true;
        }
        
        if ((int) DataStorage.saveValues["completedBlackOut"] == 0 && (int) DataStorage.saveValues["blackOut"] == 1) {
            if (!PlayerPrefs.HasKey("blackOutTrialed")) {
                PlayerPrefs.SetInt("blackOutTrialed", 1);
                GameObject.FindGameObjectWithTag("AchievementManager").GetComponent<AchievementManager>().NewAchievement(4);
            }
            DataStorage.saveValues["completedBlackOut"] = 1;
            DataStorage.saveValues["sunShardsCollected"] = (int) DataStorage.saveValues["sunShardsCollected"] + 1;
            spawn = true;
        }

        if ((int) DataStorage.saveValues["completedTimeTrial"] == 0 && (int) DataStorage.saveValues["timeTrial"] == 1) {
            if (!PlayerPrefs.HasKey("timeTrialTrialed")) {
                PlayerPrefs.SetInt("timeTrialTrialed", 1);
                GameObject.FindGameObjectWithTag("AchievementManager").GetComponent<AchievementManager>().NewAchievement(5);
            }
            DataStorage.saveValues["completedTimeTrial"] = 1;
            DataStorage.saveValues["sunShardsCollected"] = (int) DataStorage.saveValues["sunShardsCollected"] + 1;
            spawn = true;
        }

        DataStorage.saveValues["savedWaxGolem"] = 0;
        DataStorage.saveValues["savedFourArms"] = 0;

        if (spawn) {
            Instantiate(sunShard, new Vector2(0, 5), Quaternion.identity);
        } else {
            StartCoroutine(FadeOut());
        }
    }
    public IEnumerator FadeOut() {
        if ((int) DataStorage.saveValues["introToEnd"] == 0) {
            DataStorage.saveValues["introToEnd"] = 1;
        }
        DataStorage.saveValues["position"] = new Vector2(-9.8f, 2.2f);
        DataStorage.saveValues["currScene"] = "PriestOffice";
        DataStorage.saveValues["facingDirection"] = 3f;
        DataStorage.saveValues["savedWaxGolem"] = 0;
        DataStorage.saveValues["savedFourArms"] = 0;
        DataStorage.saveValues["reversedControls"] = 0;
        DataStorage.saveValues["blackOut"] = 0;
        DataStorage.saveValues["timeTrial"] = 0;
        if ((int) DataStorage.saveValues["finishGame"] == 0 && (int) DataStorage.saveValues["sunShardsCollected"] == 4) {
            DataStorage.saveValues["finishGame"] = 1;
        }
        player.SaveGame(-9.8f, 2.2f, 3, "PriestOffice");

        //Fade to White
        StartCoroutine(fadeToWhite.FadeNow());
        yield return new WaitForSeconds(1.6f);

        SceneManager.LoadScene("LoadingScreen");
        
    }
}
