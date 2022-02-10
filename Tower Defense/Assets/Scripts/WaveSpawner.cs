using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Wave
{
    // TODO: Add a data structure so that a wave can include different types of enemies
    // Maybe a Dictionary with enemyTag and count as key and value

    public string enemyTag;                     // Sets the type of enemy that will spawn
    public int count;                           // Sets the amount of enemies in a wave
    public float spawnRate;                     // Sets the wave's spawn rate
}

public class WaveSpawner : MonoBehaviour
{
    public Wave[] waves;                        // Contains all of the waves
    public Transform[] spawnPoints;             // Contains all of the spawn point positions
    public int enemiesRemaining = 0;            // Tracks how many enemies are left in the wave
    public float timeBetweenWaves = 5.0f;       // Determines how much time is in between waves
    public float countdown = 2.0f;              // Determines how much time until a wave starts
    private int waveIndex = 0;                  // Tracks the current wave that the player is on

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // TODO: Check if the waveIndex is the same as the length of the waves array
        // This indiciates that the player has completed all waves (the player has beat the map?)
        // Also need to connect the WaveSpawner to a higher level GameManager script

        if (enemiesRemaining > 0)
        {
            return;
        }
        else if (countdown <= 0.0f)
        {
            StartCoroutine(SpawnWave());
            countdown = timeBetweenWaves;
            return;
        }
        else
        {
            countdown -= Time.deltaTime;
            countdown = Mathf.Clamp(countdown, 0.0f, Mathf.Infinity);
        }
    }

    // Selects the current wave and spawns in the set amount of enemies at all of the spawn points
    // at a rate determined by the wave's spawn rate
    IEnumerator SpawnWave()
    {
        Wave wave = waves[waveIndex];
        enemiesRemaining = wave.count;

        for (int i = 0; i < wave.count; i++)
        {
            for (int j = 0; j < spawnPoints.Length; j++)
            {
                SpawnEnemy(wave.enemyTag, spawnPoints[j]);
            }

            yield return new WaitForSeconds(wave.spawnRate);
        }

        waveIndex++;
    }

    // Retrieves an enemy object from the corresponding object pool, positions it at the spawn
    // points, and sets it as active in the scene hierarchy
    void SpawnEnemy(string enemyTag, Transform spawnPoint)
    {
        GameObject enemy = ObjectPooler.SharedInstance.GetPooledObject(enemyTag);

        if (enemy != null)
        {
            enemy.transform.position = spawnPoint.position;
            enemy.transform.rotation = spawnPoint.rotation;
            enemy.SetActive(true);
        }
    }
}
