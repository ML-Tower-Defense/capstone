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
    }

    private void AttackTower()
    {
        // Return and do nothing if attack is still on cooldown
        if (!isAttackReady)
        {
            return;
        }

        // Shoot a projectile in the forward direction
        // If projectile hits the most valuable tower, add a high positive reward
        // Else if projectile hits any other tower, add a low positive reward
        // Else (projectile misses), add a negative reward

        // Set attack to not ready and reset the cooldown
        isAttackReady = false;
        attackCooldown = 10;
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        // Agent takes in positions, kill counts, and projectile counts of towers
        GameObject[] towers = GameObject.FindGameObjectsWithTag(towerTag);
        foreach (GameObject tower in towers)
        {
            Tower towerScript = tower.GetComponent<Tower>();
            sensor.AddObservation(tower.transform.position);
            sensor.AddObservation(towerScript.killCount);
            sensor.AddObservation(towerScript.projectileCount);
        }
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        /*
        int dec = actions.DiscreteActions[0];

        //if 0, move
        if (dec == 0) {
            if (this.gameObject.GetComponent<EnemyMovement>().enabled) {

            }
            else {
                this.gameObject.GetComponent<EnemyMovement>().enabled = true;
            }
        }
        //if 1, stop
        else {
            if (this.gameObject.GetComponent<EnemyMovement>().enabled) {
                this.gameObject.GetComponent<EnemyMovement>().enabled = false;
                transform.position = Vector2.MoveTowards(transform.position, transform.position, 1 * Time.deltaTime);
            }
            else {

            }
        }
        */

        float readyToShoot = actions.ContinuousActions[0]; 
        float rotationDegree = actions.ContinuousActions[1];

        if (readyToShoot >= 1)
        {
            AttackTower();
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
