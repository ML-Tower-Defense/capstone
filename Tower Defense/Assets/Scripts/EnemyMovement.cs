using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public GameObject[] waypointsArray;    // Array of waypoints along path
    int nextWaypoint;                      // Index of next waypoint

    public float movementSpeed = 3f;       // Enemy's movement speed
    public bool move = true;

    // Update is called once per frame
    void Update()
    {
        if (move == true){
            // Current location of enemy
            Vector2 currentLocation = transform.position;

            // Location of next waypoint
            Vector2 nextWPLocation = waypointsArray[nextWaypoint].transform.position;

            // If the enemy is very close to the next waypoint, then update the next waypoint to the one after
            if (Vector2.Distance(currentLocation, nextWPLocation) < 0.1f)
                nextWaypoint++;

            // If enemy reaches the last waypoint, destroy the enemy
            // This code snippet is temporary and only here so that the enemy doesn't stay on the screen
            if (nextWaypoint == waypointsArray.Length)
                move = false;
                transform.position = nextWPLocation;

            // Move enemy towards the next waypoint
            transform.position = Vector2.MoveTowards(currentLocation, nextWPLocation, movementSpeed * Time.deltaTime);
        }
        else {
        }
    }
}
