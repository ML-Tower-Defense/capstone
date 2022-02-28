using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyMovement))]
public class EnemyHealth : WaveSpawner
{
    public int health = 100;                    // Health of enemy
    public int reward = 25;                     // Amount of gold earned on death

    private EnemyMovement enemyMovement;
    private MoneyManager moneyManager;

    // Start is called before the first frame update
    void Start()
    {
        enemyMovement = GetComponent<EnemyMovement>();
        moneyManager = GameObject.Find("Gold_Container").GetComponent<MoneyManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TakeDamage(int dmgDealt)
    {
        health -= dmgDealt;
        if (health <= 0)
        {
            DeactivateEnemy();
            moneyManager.AddGold(reward);
            enemiesRemaining--;
        }
    }

    // Returns the enemy to the object pool and resets the enemy's properties so that it can be
    // reused later
    // Use this function instead of Destroy() to deactivate an enemy object in the scene
    void DeactivateEnemy()
    {
        gameObject.SetActive(false);
        health = 100;
        enemyMovement.nextWaypoint = 0;
    }
}
