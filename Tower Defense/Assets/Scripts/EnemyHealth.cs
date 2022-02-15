using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyMovement))]
public class EnemyHealth : MonoBehaviour
{
    public int health = 100;                   // health of enemy
    private EnemyMovement enemyMovement;

    // Start is called before the first frame update
    void Start()
    {
        enemyMovement = GetComponent<EnemyMovement>();
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
