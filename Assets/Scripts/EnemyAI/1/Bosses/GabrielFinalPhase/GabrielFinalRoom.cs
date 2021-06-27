using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GabrielFinalRoom : MonoBehaviour {
    [SerializeField] private GameObject orbOneObject, orbTwoObject, orbThreeObject, orbFourObject;
    [SerializeField] private Dialogue[] outroDialogues;
    [SerializeField] private GameObject sunShard;
    private LightOrbs orbOne, orbTwo, orbThree, orbFour;
    private GameObject playerObject;
    private Player player;
    private GabrielFinal gabrielFinal;
    private DialogueManager dialogueManager;
    private int line;
    private bool inOutro;
    private bool imprisoned;
    private void Start() {
        imprisoned = false;
        inOutro = false;
        line = 0;
        playerObject = GameObject.FindGameObjectWithTag("Player");
        player = playerObject.GetComponent<Player>();
        dialogueManager = GameObject.FindGameObjectWithTag("DialogueManager").GetComponent<DialogueManager>();
        gabrielFinal = GameObject.FindGameObjectWithTag("Boss").GetComponent<GabrielFinal>();
        orbOne = orbOneObject.GetComponent<LightOrbs>();
        orbTwo = orbTwoObject.GetComponent<LightOrbs>();
        orbThree = orbThreeObject.GetComponent<LightOrbs>();
        orbFour = orbFourObject.GetComponent<LightOrbs>();
    }
    private void Update() {
        //Imprisons Gabriel once the Orbs are charged
        if (orbOne.charge == 10 && orbTwo.charge == 10 && orbThree.charge == 10 && orbFour.charge == 10 && !imprisoned) {
            imprisoned = true;
            StartCoroutine(ImprisonGabriel());
        }

        if (inOutro && line == outroDialogues[(int) DataStorage.saveValues["finalBossBeatenCount"]].names.Length - 1 && Input.GetKeyDown(KeyCode.E)) {
            line = 0;
            inOutro = false;
            Destroy(gabrielFinal.gameObject);
            SpawnSunShard();
            dialogueManager.DisplayNextSentence();
        } else {
            line++;
            dialogueManager.DisplayNextSentence();
        }
    }
    private IEnumerator ImprisonGabriel() {
        //Imprison Gabriel
        gabrielFinal.Imprison();
        yield return new WaitForSeconds(4f);
        //Resets Gabriel
        gabrielFinal.Return();
        //Reset the orbs
        imprisoned = false;
        orbOne.ResetOrb();
        orbTwo.ResetOrb();
        orbThree.ResetOrb();
        orbFour.ResetOrb();
    }
    public void FinishFight() {
        //Start the Outro Dialogue
        dialogueManager.StartDialogue(outroDialogues[(int) DataStorage.saveValues["finalBossBeatenCount"]]);
        DataStorage.saveValues["finalBossBeatenCount"] = (int) DataStorage.saveValues["finalBossBeatenCount"] + 1;
        inOutro = true;
        //Teleport Player to specific position
        gabrielFinal.gameObject.transform.position = new Vector2(0.57f, 9.08f);
        playerObject.transform.position = new Vector2(); //EDIT!!!!!!!!!
        //Remove the Orbs' Colliders
        Destroy(orbOneObject.GetComponent<Collider2D>());
        Destroy(orbTwoObject.GetComponent<Collider2D>());
        Destroy(orbThreeObject.GetComponent<Collider2D>());
        Destroy(orbFourObject.GetComponent<Collider2D>());
        //Stop Player from attacking
        player.allowCombat = false;
    }
    public void SpawnSunShard() {
        Instantiate(sunShard, new Vector2(), Quaternion.identity); //EDIT!!!!!!!!!!!
    }
}
