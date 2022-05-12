using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyMovement))]
public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 100;                     // Max health of enemy
    public int currentHealth;                       // Current health of enemy
    public int reward = 25;                         // Amount of gold earned on death

    private EnemyMovement enemyMovement;
    private MoneyManager moneyManager;
    AudioManager audioManager;
    private HealthBar healthBar;
    public GameObject healthPrefab;


    // Start is called before the first frame update
    void Start()
    {
        enemyMovement = GetComponent<EnemyMovement>();
        moneyManager = GameObject.Find("Gold_Container").GetComponent<MoneyManager>();
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        healthPrefab = Instantiate(healthPrefab);
        healthPrefab.transform.SetParent(gameObject.transform);
        healthPrefab.transform.localPosition = new Vector3(0, .5f, 0);
        healthBar = this.GetComponentInChildren(typeof(HealthBar)) as HealthBar;
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

        // Return false if health goes below 0 and enemy dies
        if (currentHealth <= 0)
        {
            audioManager.Play("EnemyDeath");
            DeactivateEnemy();
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
        gameObject.SetActive(false);
        currentHealth = maxHealth;
        enemyMovement.nextWaypoint = 0;
        enemyMovement.isFacingRight = true;
    }
}
