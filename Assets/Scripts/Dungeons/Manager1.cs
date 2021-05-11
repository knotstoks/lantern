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
    private int currWave;
    private bool complete;
    public int numOfEnemies;
    [SerializeField] private Wave[] waves;
    //Range of positions to spawn enemies
    [SerializeField] private float minX;
    [SerializeField] private float minY;
    [SerializeField] private float maxX;
    [SerializeField] private float maxY;

    private IEnumerator Start() {
        yield return new WaitForSeconds(0.2f);
        GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().allowCombat = true;
        currWave = -1;
        complete = false;
        numOfEnemies = -1;
        StartCoroutine(StartRoom());
    }
    private void Update() {
        //Checks whether to spawn next wave
        if (numOfEnemies == 0) {
            if (currWave == waves.Length) {
                complete = true;
                Debug.Log("Room is complete!");
            } else {
                NextWave();
            }
        }

        //Finish the whole room
        if (complete) {
            complete = false;
            //Animation shaking of room + door opening
        }
    }

    private void NextWave() {
        currWave += 1;
        Debug.Log("Wave: " + currWave);
        numOfEnemies = waves[currWave].smalls + waves[currWave].mediums + waves[currWave].bigs;

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
        yield return 1000;
        NextWave();
    }

    public void EnemiesNow(int n) {
        numOfEnemies += n;
    }
}