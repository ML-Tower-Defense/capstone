using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 3;             //Movement Speed
    private Transform waypointTarget;   //Next waypoint target for enemy to move to
    private int wavepointIndex = 0;     //Which waypoint we last visited
    private int health = 100;           //health of enemy
    private int damage = 10;            //Damage dealt by this enemy
    private int rateFire = 1;           //How fast this enemy attacks
    public float range = 3f;            //Range enemy can attack
    public Transform target;            //Target tower to attack (looks for closest one within range)
    public string towerTag = "Tower";   //tag to add to towers


    // Start is called before the first frame update
    void Start()
    {
        waypointTarget = Waypoints.points[0];       //Sets initial wavepoint to the very first one to begin enemy movement
        InvokeRepeating("UpdateTarget", 0f, 0.5f);  //Repeatedly checking for towers within range every .5 seconds
    }

    // Update is called once per frame
    void Update()
    {
        //Movement
        Vector2 dir = waypointTarget.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

        if(Vector2.Distance(transform.position, waypointTarget.position) <= 0.1f){
            GetNextWaypoint();
        }

        //Attacking
        if (target == null)
            return;
       
    }

    //Finds all the towers with the tower tag and finds the closest one to the enemy
    //within the range
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

    //Finds next waypoint to move to
    //Or, enemy has reached end and is destroyed
    void GetNextWaypoint()
    {
        if (wavepointIndex >= Waypoints.points.Length - 1){
            Destroy(gameObject);
            return;
        }
        wavepointIndex++;
        waypointTarget = Waypoints.points[wavepointIndex];
    }

    //Draws range -> not visible when playing game 
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
