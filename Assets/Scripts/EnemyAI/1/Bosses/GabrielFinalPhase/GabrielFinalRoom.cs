using System.Collections;
using UnityEngine;

public class GabrielFinalRoom : MonoBehaviour {
    [SerializeField] private GameObject orbOneObject, orbTwoObject, orbThreeObject, orbFourObject;
    [SerializeField] private Dialogue[] outroDialogues;
    [SerializeField] private GameObject sunShard;
    [SerializeField] private GameObject featherDestroyer;
    [SerializeField] private AudioClip[] audioClips; //0 for normal, 1 for done
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
        player.allowCombat = true;
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
        yield return new WaitForSeconds(4f);
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
    public void SpawnSunShard() {
        Instantiate(sunShard, new Vector2(0, 5), Quaternion.identity);
    }
}
