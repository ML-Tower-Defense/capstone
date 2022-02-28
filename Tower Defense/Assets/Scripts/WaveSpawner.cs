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
    public float spawnRate;                     // Sets the wave's spawn rate in seconds
}

public class WaveSpawner : MonoBehaviour
{
    public Wave[] waves;                        // Contains all of the waves
    public Transform[] spawnPoints;             // Contains all of the spawn point positions
    public GameManager gameManager;
    public float timeBetweenWaves = 5.0f;       // Determines how much time is in between waves
    public float countdown = 2.0f;              // Determines how much time until a wave starts
    public static int enemiesRemaining;         // Tracks how many enemies are still alive

    private int unspawnedEnemies;           // Tracks how many enemies are left in the wave
    private int waveIndex;                  // Tracks the current wave that the player is on

    // Start is called before the first frame update
    void Start()
    {
        gameManager = gameObject.GetComponent("GameManager") as GameManager;
    }

    // Update is called once per frame
    void Update()
    {
        print("Wave: " + waveIndex + "/" + waves.Length + "    Unspawned: " + unspawnedEnemies + "    Remaining: " + enemiesRemaining);

        // Ignore cases if there are still enemies to spawn
        if (unspawnedEnemies > 0)
        {
            return;
        }
        // Direct player to Victory if all waves are completed
        else if (waveIndex == waves.Length && enemiesRemaining == 0)
        {
            gameManager.Victory();
            this.enabled = false;
        }
        // Spawn waves if cooldown has hit 0 seconds
        else if (countdown <= 0.0f)
        {
            StartCoroutine(SpawnWave());
            countdown = timeBetweenWaves;

            return;
        }
        // Otherwise, decrement cooldown
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
        Wave currentWave = waves[waveIndex];

        if (unspawnedEnemies == 0) {
            unspawnedEnemies = currentWave.count;
            enemiesRemaining += currentWave.count;
        }

        for (int i = 0; i < currentWave.count; i++)
        {
            for (int j = 0; j < spawnPoints.Length; j++)
            {
                if (GateManager.gateCurrentHP > 0)
                {
                    SpawnEnemy(currentWave.enemyTag, spawnPoints[j]);

                    if (unspawnedEnemies > 0)
                        unspawnedEnemies--;
                }
            }

            yield return new WaitForSeconds(currentWave.spawnRate);
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
