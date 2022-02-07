using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 3;             // Movement Speed
    private Transform waypointTarget;   // Next waypoint target for enemy to move to
    private int waypointIndex = 0;     // Which waypoint we last visited
    private int health = 100;           // health of enemy
    private int damage = 10;            // Damage dealt by this enemy
    public float rateFire = 1f;           // How fast this enemy attacks
    private float fireCountdown = 0f;
    public float range = 3f;            // Range enemy can attack
    public Transform target;            // Target tower to attack (looks for closest one within range)
    public string towerTag = "Tower";   // tag to add to towers
    public GameObject bulletPrefab;
    public Transform firePoint;         //where bullet spawns in


    // Start is called before the first frame update
    void Start()
    {
        waypointTarget = Waypoints.points[0];       // Sets initial waypoint to the very first one to begin enemy movement
        InvokeRepeating("UpdateTarget", 0f, 0.5f);  // Repeatedly checking for towers within range every .5 seconds
    }

    // Update is called once per frame
    void Update()
    {
        // Movement
        Vector2 dir = waypointTarget.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

        if(Vector2.Distance(transform.position, waypointTarget.position) <= 0.1f){
            GetNextWaypoint();
        }

        if (target == null)
            return;
        if (fireCountdown <= 0f)
        {
            Attack();
            fireCountdown = 1f / rateFire;
        }
        fireCountdown -= Time.deltaTime;
    }

    // TODO: Modify Attack so that the enemy will attack the gate instead of firing bullets at towers
    void Attack()
    {
        /*
        // This is where the attacking occurs
        // Bullets won't even show up
        Debug.Log("We are attacking");
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        GameObject bulletGo = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bullet = bulletGo.GetComponent<Bullet>();
        if (bullet != null)
            bullet.Seek(target);
        */
    }

    // Finds all the towers with the tower tag and finds the closest one to the enemy
    // within the range
    void UpdateTarget()
    {
        GameObject[] towers = GameObject.FindGameObjectsWithTag(towerTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestTower = null;
        foreach(GameObject tower in towers) {
            {
                float distanceToTower = Vector2.Distance(transform.position, tower.transform.position);
                if (distanceToTower < shortestDistance)
                {
                    shortestDistance = distanceToTower;
                    nearestTower = tower;
                }
            }
        }
        if (nearestTower != null && shortestDistance <= range)
        {
            target = nearestTower.transform;
        }
        else
            target = null;
    }

    // Finds next waypoint to move to
    // Or, enemy has reached end and is destroyed
    void GetNextWaypoint()
    {
        if (waypointIndex >= Waypoints.points.Length - 1){
            DeactivateEnemy();
            return;
        }
        waypointIndex++;
        waypointTarget = Waypoints.points[waypointIndex];
    }

    // Draws range -> not visible when playing game
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }

    // Returns the enemy to the object pool and resets the enemy's properties so that it can be
    // reused later
    // Use this function instead of Destroy() to deactivate an enemy object in the scene
    void DeactivateEnemy()
    {
        gameObject.SetActive(false);
        health = 100;
        waypointIndex = 0;
        waypointTarget = Waypoints.points[0];
    }
}
