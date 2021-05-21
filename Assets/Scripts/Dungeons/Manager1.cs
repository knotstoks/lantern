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
    [SerializeField] private AudioSource musicAudioSource;
    [SerializeField] private AudioSource sfx;
    [SerializeField] private AudioClip[] sounds; //0 - main music, 1 - finished music, 2 - spawn sounds
    [SerializeField] private GameObject spawnCircle;
    //Range of positions to spawn enemies
    public float minX;
    public float minY;
    public float maxX;
    public float maxY;

    private IEnumerator Start() {
        //Delete after
        PlayerPrefs.SetFloat("volume", 1);

        musicAudioSource.volume = PlayerPrefs.GetFloat("volume");
        sfx.volume = PlayerPrefs.GetFloat("volume");
        musicAudioSource.loop = true;
        musicAudioSource.clip = sounds[0];
        musicAudioSource.Play();
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
            musicAudioSource.clip = sounds[1];
            musicAudioSource.loop = false;
            musicAudioSource.Play();
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
        sfx.Play();
        //Spawn the enemies
        GameObject[] smallies = waves[currWave].smallEnemies;
        GameObject[] mediumies = waves[currWave].mediumEnemies;
        GameObject[] bigies = waves[currWave].bigEnemies;
        for (int i = 0; i < waves[currWave].smalls; i++) {
            GameObject circle = Instantiate(spawnCircle, 
                new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY)), 
                Quaternion.identity);
            circle.GetComponent<SpawnCircle>().enemy = smallies[Random.Range(0, smallies.Length)];
        }

        for (int i = 0; i < waves[currWave].mediums; i++) {
            GameObject circle = Instantiate(spawnCircle, 
                new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY)), 
                Quaternion.identity);
            circle.GetComponent<SpawnCircle>().enemy = mediumies[Random.Range(0, mediumies.Length)];
        }

        for (int i = 0; i < waves[currWave].bigs; i++) {
            GameObject circle = Instantiate(spawnCircle, 
                new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY)), 
                Quaternion.identity);
            circle.GetComponent<SpawnCircle>().enemy = bigies[Random.Range(0, bigies.Length)];
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