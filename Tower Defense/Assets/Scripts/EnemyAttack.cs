using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyAttack : EnemyMovement
{
    public GameObject gate;

    bool reachedGate = false;
    bool startedAttacking = false;

    // Update is called once per frame
    void Update()
    {
        Vector2 currentLocation = transform.position;
        Vector2 nextWPLocation;

        if (nextWaypoint != waypointsArray.Length)
        {
            nextWPLocation = waypointsArray[nextWaypoint].transform.position;

            if (Vector2.Distance(currentLocation, nextWPLocation) < 0.1f)
                nextWaypoint++;
        }

        // print("Waypoint: " + nextWaypoint + "/" + waypointsArray.Length);

        if (nextWaypoint == waypointsArray.Length)
            reachedGate = true;

        if (reachedGate && !startedAttacking)
        {
            StartCoroutine(damageGate(25));
            startedAttacking = true;
        }

        if (GateManager.gateCurrentHP <= 0)
        {
            Destroy(gate);
            animator.Play("battlecry");
        }
    }

    IEnumerator damageGate(int damage)
    {
        yield return new WaitForSecondsRealtime(0.5f);

        GateManager.damageGate(damage);

        yield return new WaitForSecondsRealtime(0.4f);

        startedAttacking = false;

        StopAllCoroutines();
    }
}
