using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Wave
{
    // TODO: Add a data structure so that a wave can include different types of enemies
    // Maybe a Dictionary with tag and count as key and value
    public string tag;
    public int count;
    public float spawnRate;
}

public class WaveSpawner : MonoBehaviour
{
    public Wave[] waves;
    public Transform[] spawnPoints;
    public int enemiesRemaining = 0;
    public float timeBetweenWaves = 5.0f;
    public float countdown = 2.0f;
    private int waveIndex = 0;

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

    IEnumerator SpawnWave()
    {
        Wave wave = waves[waveIndex];
        enemiesRemaining = wave.count;

        for (int i = 0; i < wave.count; i++)
        {
            for (int j = 0; j < spawnPoints.Length; j++)
            {
                SpawnEnemy(wave.tag, spawnPoints[j]);
            }

            yield return new WaitForSeconds(wave.spawnRate);
        }

        waveIndex++;
    }

    void SpawnEnemy(string tag, Transform spawnPoint)
    {
        GameObject enemy = ObjectPooler.SharedInstance.GetPooledObject(tag);

        if (enemy != null)
        {
            enemy.transform.position = spawnPoint.position;
            enemy.transform.rotation = spawnPoint.rotation;
            enemy.SetActive(true);
        }
    }
}
