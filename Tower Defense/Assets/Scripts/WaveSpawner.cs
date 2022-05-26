using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Wave
{
    public WaveGroup[] waveGroups;              // Contains all of the consecutive enemy groups
    public float spawnRate;                     // Sets the wave's spawn rate in seconds
}

[System.Serializable]
public class WaveGroup
{
    public string enemyTag;                     // Sets the type of enemy for a group
    public int count;                           // Sets the amount of enemies in a group
}

public class WaveSpawner : MonoBehaviour
{
    public Wave[] waves;                        // Contains all of the waves
    public Transform[] spawnPoints;             // Contains all of the spawn point positions
    public GameManager gameManager;
    public float timeBetweenWaves = 5.0f;       // Determines how much time is in between waves
    public float countdown = 2.0f;              // Determines how much time until a wave starts
    public static int enemiesRemaining;         // Tracks how many enemies are still alive

    private int unspawnedEnemies;               // Tracks how many enemies are left in the wave
    private int waveIndex;                      // Tracks the current wave that the player is on

    public GameObject zombie;
    public GameObject goblin;
    public GameObject knight;
    public GameObject golem;

    // Start is called before the first frame update
    void Start()
    {
        // Reset enemy counts when a player reloads a map
        enemiesRemaining = 0;
        unspawnedEnemies = 0;

        gameManager = gameObject.GetComponent("GameManager") as GameManager;
    }

    // Update is called once per frame
    void Update()
    {
        //print("Wave: " + waveIndex + "/" + waves.Length + "    Unspawned: " + unspawnedEnemies + "    Remaining: " + enemiesRemaining);

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
        else if (countdown <= 0.0f && waveIndex != waves.Length)
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

        int totalEnemyCount = 0;
        for (int i = 0; i < currentWave.waveGroups.Length; i++)
        {
            totalEnemyCount += currentWave.waveGroups[i].count;
        }

        if (unspawnedEnemies == 0) {
            unspawnedEnemies = totalEnemyCount;
            enemiesRemaining += totalEnemyCount;
        }

        for (int i = 0; i < currentWave.waveGroups.Length; i++)
        {
            for (int j = 0; j < currentWave.waveGroups[i].count; j++)
            {
                for (int k = 0; k < spawnPoints.Length; k++)
                {
                    if (GateManager.gateCurrentHP > 0)
                    {
                        SpawnEnemy(currentWave.waveGroups[i].enemyTag, spawnPoints[k]);

                        if (unspawnedEnemies > 0)
                            unspawnedEnemies--;
                    }
                }

                yield return new WaitForSeconds(currentWave.spawnRate);
            }
        }

        waveIndex++;
    }

    // Retrieves an enemy object from the corresponding object pool, positions it at the spawn
    // points, and sets it as active in the scene hierarchy
    void SpawnEnemy(string enemyTag, Transform spawnPoint)
    {
        if (enemyTag == "zombieEnemy") {
            GameObject enemy = Instantiate(zombie);
            enemy.transform.position = spawnPoint.position;
            enemy.transform.rotation = spawnPoint.rotation;
            enemy.SetActive(true);
        }
        else if (enemyTag == "goblinEnemy") {
            GameObject enemy = Instantiate(goblin);
            enemy.transform.position = spawnPoint.position;
            enemy.transform.rotation = spawnPoint.rotation;
            enemy.SetActive(true);
        }
        else if (enemyTag == "knightEnemy") {
            GameObject enemy = Instantiate(knight);
            enemy.transform.position = spawnPoint.position;
            enemy.transform.rotation = spawnPoint.rotation;
            enemy.SetActive(true);
        }
        else if (enemyTag == "golemEnemy")
        {
            GameObject enemy = Instantiate(golem);
            enemy.transform.position = spawnPoint.position;
            enemy.transform.rotation = spawnPoint.rotation;
            enemy.SetActive(true);
        }

        /*
        GameObject enemy = ObjectPooler.SharedInstance.GetPooledObject(enemyTag);

        if (enemy != null)
        {
            enemy.transform.position = spawnPoint.position;
            enemy.transform.rotation = spawnPoint.rotation;
            //enemy.SetActive(true);
        }
        */
    }

    public int getWave () {
        return waveIndex + 1;
    }

    public int getUnspawned() {
        return unspawnedEnemies;
    }
}
