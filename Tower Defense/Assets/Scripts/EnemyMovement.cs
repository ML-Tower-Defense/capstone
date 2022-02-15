using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public GameObject[] waypointsArray;    // Array of waypoints along path
    public int nextWaypoint;               // Index of next waypoint

    public float movementSpeed = 5f;       // Enemy's movement speed

    public Animator animator;       // Enemy's animation controller
                                           // Knight animations:
                                           // "idle", "walk", "run", "battlecry", "jump",
                                           // "attack", "attack2", "attack3", "attack4", "shield",
                                           // "take_hit", "take_hit2", "dying"

    string[] attackAnims = { "attack", "attack2", "attack3", "attack4" };

    bool crying = false;    // ;(
    float cryTimer;         // Cry duration

    bool gateDestroyed = false;

    // Let enemy walk at first
    private void Start()
    {
        animator.Play("walk");
    }

    // Update is called once per frame
    void Update()
    {
        // Current location of enemy
        Vector2 currentLocation = transform.position;

        // Location of next waypoint
        Vector2 nextWPLocation;

        if (nextWaypoint != waypointsArray.Length)
        {
            nextWPLocation = waypointsArray[nextWaypoint].transform.position;

            // Let enemy face the appropriate x-direction at all times
            // Face right
            if (nextWPLocation.x > transform.position.x)
                transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);

            // Face left
            else if (nextWPLocation.x < transform.position.x)
                transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);

            // If the enemy is very close to the next waypoint, then update the next waypoint to the one after
            if (Vector2.Distance(currentLocation, nextWPLocation) < 0.1f)
                nextWaypoint++;

            // If gate is destroyed, enemy doesn't move and repeats battlecry
            if (GateManager.gateCurrentHP <= 0)
            {
                gateDestroyed = true;
            }

            // Once enemy reaches second waypoint, do a battlecry and start running
            if (nextWaypoint >= 2 && nextWaypoint != waypointsArray.Length && !gateDestroyed)
            {
                if (cryTimer < 1.5)
                {
                    cryTimer += Time.deltaTime;
                    StartCoroutine(battleCry());
                }

                else
                {
                    StopCoroutine(battleCry());
                    crying = false;
                    animator.Play("run");
                }

                // Look left after passing 3rd waypoint
                if (nextWaypoint == 3)
                    transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
            }

            // If enemy reaches the last waypoint (the gate), play attack animations
            else if (nextWaypoint == waypointsArray.Length)
                animator.Play(attackAnims[0]);

            // Move enemy towards the next waypoint
            if (!crying && !gateDestroyed && nextWaypoint != waypointsArray.Length)
                transform.position = Vector2.MoveTowards(currentLocation, nextWPLocation, movementSpeed * Time.deltaTime);
        }
    }

    // Do the roar
    IEnumerator battleCry()
    {
        crying = true;

        animator.Play("battlecry");

        yield return new WaitForSecondsRealtime(2);
    }
}
