using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waveCounter : MonoBehaviour
{
    public int maxWave;
    public int currentWave;
    string waveText;
    private int unspawnCount;
    public UnityEngine.UI.Text waveCount = null;
    public UnityEngine.UI.Text unspawned = null;
    public UnityEngine.UI.Text enemiesLeftString = null;
    private WaveSpawner waveSpawner;


    // Start is called before the first frame update
    void Start()
    {
        waveSpawner = GameObject.Find("GameManager").GetComponent<WaveSpawner>();
        currentWave = waveSpawner.getWave();
        maxWave = waveSpawner.waves.Length;
        waveText = string.Concat(currentWave.ToString()," / ",maxWave.ToString());
        waveCount.text = waveText;
        unspawnCount = waveSpawner.getUnspawned();
        unspawned.text = unspawnCount.ToString();
        enemiesLeftString.text = WaveSpawner.enemiesRemaining.ToString();     
    }

    // Update is called once per frame
    void Update()
    {
        currentWave = waveSpawner.getWave();
        waveText = string.Concat(currentWave.ToString()," / ",maxWave.ToString());
        waveCount.text = waveText;
        unspawnCount = waveSpawner.getUnspawned();
        unspawned.text = unspawnCount.ToString();
        enemiesLeftString.text = WaveSpawner.enemiesRemaining.ToString();  
    }
}
