using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyMovement))]
public class EnemyHealth : MonoBehaviour
{
    public int maxHealth;                     // Max health of enemy
    public int currentHealth;                       // Current health of enemy
    public int reward;                         // Amount of gold earned on death

    private AudioManager audioManager;
    private MoneyManager moneyManager;

    private EnemyMovement enemyMovement;
    private HealthBar healthBar;
    public GameObject healthPrefab;

    private WaveSpawner waveSpawner;
    private int currentWave;


    // Start is called before the first frame update
    void Start()
    {
        enemyMovement = GetComponent<EnemyMovement>();
        moneyManager = GameObject.Find("Gold_Container").GetComponent<MoneyManager>();
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        waveSpawner = GameObject.Find("GameManager").GetComponent<WaveSpawner>();
        healthPrefab = Instantiate(healthPrefab);
        healthPrefab.transform.SetParent(gameObject.transform);
        healthPrefab.transform.localPosition = new Vector3(0, .5f, 0);
        healthBar = GetComponentInChildren(typeof(HealthBar)) as HealthBar;
        currentWave = waveSpawner.getWave();
        maxHealth = (int) (maxHealth * (1.0f + ((currentWave-1)*.2f)));
        currentHealth = maxHealth;
        healthBar.setMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public bool TakeDamage(int dmgDealt)
    {
        currentHealth -= dmgDealt;
        healthBar.setHealth(currentHealth);
        if (currentHealth <= 0 && gameObject.activeInHierarchy)
        {
            audioManager.Play("EnemyDeath");
            Destroy(gameObject);
            moneyManager.AddGold(reward);
            WaveSpawner.enemiesRemaining--;
            return false;
        }

        // Return true if enemy is still alive
        return true;
    }

    // Returns the enemy to the object pool and resets the enemy's properties so that it can be
    // reused later
    // Use this function instead of Destroy() to deactivate an enemy object in the scene
    void DeactivateEnemy()
    {
        /*
        gameObject.SetActive(false);
        currentHealth = maxHealth;
        healthBar.setHealth(currentHealth);
        enemyMovement.nextWaypoint = 0;
        enemyMovement.isFacingRight = true;
        */
    }
}
