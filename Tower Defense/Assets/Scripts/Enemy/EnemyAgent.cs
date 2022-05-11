using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class EnemyAgent : Agent
{
    // Reinforcement learning cycle
    // Observation -> Decision -> Action -> Reward
    public Transform attackPoint;
    public GameObject projectilePrefab;
    public LineRenderer laser;

    private float rotationSpeed = 3f;
    private bool isAttackReady = true;
    private int attackCooldown = 0;
    private string towerTag = "Tower";

    // Updates the attack cooldown to determine if attack is ready
    void Update()
    {
        if (!isAttackReady)
        {
            attackCooldown -= 1;

            if (attackCooldown <= 0)
            {
                isAttackReady = true;
            }
        }

        // Hit spacebar to test shooting
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(Shoot());
        }
    }

    IEnumerator Shoot()
    {
        // Return and do nothing if attack is still on cooldown
        if (!isAttackReady)
        {
            yield return null;
        }

        // Shoot a projectile in the forward direction
        RaycastHit2D hitInfo = Physics2D.Raycast(attackPoint.position, attackPoint.right);

        // If projectile hits any other tower, add a positive reward
        if (hitInfo)
        {
            Tower tower = hitInfo.transform.GetComponent<Tower>();

            if (tower)
            {
                // TODO: Add functionality for keeping track of health and taking damage to tower
                // tower.TakeDamage(damage);
                AddReward(2);
            }

            // TODO: Create an impact effect on hitting a tower

            laser.SetPosition(0, attackPoint.position);
            laser.SetPosition(1, hitInfo.point);
        }
        // Else (projectile misses), add a negative reward
        else
        {
            AddReward(-1);

            laser.SetPosition(0, attackPoint.position);
            laser.SetPosition(1, attackPoint.position + attackPoint.right * 100);
        }

        laser.enabled = true;
        yield return new WaitForSeconds(0.02f);
        laser.enabled = false;

        // Set attack to not ready and reset the cooldown
        isAttackReady = false;
        attackCooldown = 5;
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        // Agent takes in kill counts and projectile counts of towers
        GameObject[] towers = GameObject.FindGameObjectsWithTag(towerTag);
        foreach (GameObject tower in towers)
        {
            Tower towerScript = tower.GetComponent<Tower>();
            sensor.AddObservation(towerScript.killCount);
            sensor.AddObservation(towerScript.projectileCount);
        }
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        float readyToShoot = actions.ContinuousActions[0]; 
        float rotationDegree = actions.ContinuousActions[1];

        if (readyToShoot >= 1)
        {
            StartCoroutine(Shoot());
        }

        transform.Rotate(Vector3.forward, rotationDegree * rotationSpeed);
    }

    public override void Heuristic(in ActionBuffers actionsOut) {
        ActionSegment<int> discreteActions = actionsOut.DiscreteActions;
        discreteActions[0] = 0;
        if (Input.GetKey("right")) {
            discreteActions[0] = 1;
        }
    }
}
