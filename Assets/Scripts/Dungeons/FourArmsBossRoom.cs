using System.Collections;
using UnityEngine;

public class FourArmsBossRoom : MonoBehaviour {
    [SerializeField] private GameObject doorOut;
    [SerializeField] private Dialogue[] introDialogues; //0 for havent seen, 1 for seen but lost, 2 for seen and beat before
    [SerializeField] private Dialogue outroDialogue;
    [SerializeField] private GameObject bossHPBar;
    [SerializeField] private AudioClip[] music; //0 for boss theme, 1 for end theme
    [SerializeField] private GameObject gate;
    private bool introDone;
    private bool fightCompleted;
    private FourArms fourArmsBoss;
    private Player player;
    private DialogueManager dialogueManager;
    private int line;
    private AudioSource audioSource;
    private IEnumerator Start() {
        //DELETE AFTER!!!!!!!!!!!!!!!!
        //DataStorage.saveValues["waxDungeonFourArms"] = 0;

        doorOut.SetActive(false);
        introDone = false;
        fightCompleted = false;
        line = 0;
        bossHPBar.SetActive(false);
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        fourArmsBoss = GameObject.FindGameObjectWithTag("FourArms").GetComponent<FourArms>();
        dialogueManager = GameObject.FindGameObjectWithTag("DialogueManager").GetComponent<DialogueManager>();
        audioSource = GetComponent<AudioSource>();
        audioSource.loop = true;
        audioSource.clip = music[0];
        audioSource.Play();
        yield return new WaitForSeconds(0.05f);
        StartFourArmsIntro();
    } 
    private void Update() {
        if (!introDone && Input.GetKeyDown(KeyCode.E)) { //Intro
            if (line == introDialogues[(int) DataStorage.saveValues["waxDungeonFourArms"]].names.Length - 1) {
                if ((int) DataStorage.saveValues["waxDungeonFourArms"] == 0) {
                    DataStorage.saveValues["waxDungeonFourArms"] = 1;
                }

                introDone = true;
                line = 0;
                dialogueManager.DisplayNextSentence();
                player.allowCombat = true;
                StartBossFight();
            } else {
                line++;
                dialogueManager.DisplayNextSentence();
            }
        }

        if (fightCompleted && Input.GetKeyDown(KeyCode.E)) { //Outro
            if (line == outroDialogue.names.Length - 1) {
                fightCompleted = false;
                dialogueManager.DisplayNextSentence();
                line = 0;
            } else {
                line++;
                dialogueManager.DisplayNextSentence();
            }
        }
    }
    private void StartFourArmsIntro() {
        dialogueManager.StartDialogue(introDialogues[(int) DataStorage.saveValues["waxDungeonFourArms"]]);
    }
    private void StartBossFight() {
        //Set Four Arms Mini Boss Active with HP Bar
        bossHPBar.SetActive(true);
        fourArmsBoss.StartBoss();
    }
    public void CompleteFight() {
        audioSource.clip = music[1];
        audioSource.loop = false;
        audioSource.Play();
        fightCompleted = true;
        bossHPBar.SetActive(false);
        dialogueManager.StartDialogue(outroDialogue);
        doorOut.SetActive(true);
        gate.GetComponent<Animator>().SetTrigger("Open");
    }
}
