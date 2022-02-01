using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public Transform enemy;
    public float timeBetweenWaves = 10f;
    private float countDown = 3f;

    

    // Update is called once per frame
    void Update()
    {
        if(countDown <= 0f){
            SpawnWave();
            countDown = timeBetweenWaves;
        }

        countDown -= Time.deltaTime;
    }

    void SpawnWave(){
        Debug.Log("Wave spawned");
    }
}
