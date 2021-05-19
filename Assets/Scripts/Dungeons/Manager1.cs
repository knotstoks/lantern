using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[System.Serializable]

//Shows how many different types of enemies to spawn in every wave
public class Wave {
    public GameObject[] smallEnemies; 
    public GameObject[] mediumEnemies;
    public GameObject[] bigEnemies; 
    public int smalls;
    public int mediums; 
    public int bigs;
}
public class Manager1 : MonoBehaviour {
    public int currWave;
    private bool complete;
    public int numOfEnemies;
    [SerializeField] private Wave[] waves;
    //Range of positions to spawn enemies
    public float minX;
    public float minY;
    public float maxX;
    public float maxY;

    private IEnumerator Start() {
        currWave = -1;
        complete = false;
        numOfEnemies = 0;
        yield return new WaitForSeconds(0.2f);
        GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().allowCombat = true;
        StartCoroutine(StartRoom());
    }
    private void Update() {
        //Finish the whole room
        if (complete) {
            complete = false;
            //Animation shaking of room + door opening
        }
    }

    private void CheckSpawn() {
        if (currWave <= waves.Length) {
            currWave += 1;

            if (currWave < waves.Length) {
                numOfEnemies = waves[currWave].smalls + waves[currWave].mediums + waves[currWave].bigs;
                NextWave();
            } else if (currWave == waves.Length) {
                complete = true;
                numOfEnemies = -1;
            }
        }
    }

    private void NextWave() {
        //Spawn the enemies
        GameObject[] smallies = waves[currWave].smallEnemies;
        GameObject[] mediumies = waves[currWave].mediumEnemies;
        GameObject[] bigies = waves[currWave].bigEnemies;
        for (int i = 0; i < waves[currWave].smalls; i++) {
            Instantiate(smallies[Random.Range(0, smallies.Length)], 
                new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY)), 
                Quaternion.identity);
        }

        for (int i = 0; i < waves[currWave].mediums; i++) {
            Instantiate(mediumies[Random.Range(0, mediumies.Length)], 
                new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY)), 
                Quaternion.identity);
        }

        for (int i = 0; i < waves[currWave].bigs; i++) {
            Instantiate(bigies[Random.Range(0, bigies.Length)], 
                new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY)), 
                Quaternion.identity);
        }
    }

    //Waits a while before starting the wave system
    private IEnumerator StartRoom() {
        yield return new WaitForSeconds(2);
        CheckSpawn();
    }

    public void EnemiesNow(int n) {
        numOfEnemies += n;
        if (numOfEnemies == 0) {
            CheckSpawn();
        }
    }
}