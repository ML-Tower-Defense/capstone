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

    public override void CollectObservations(VectorSensor sensor)
    {
        // Agent takes in its own position as observation data
        sensor.AddObservation(transform.position);

        // Agent takes in positions of existing towers on the map?
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
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
    }

    public override void Heuristic(in ActionBuffers actionsOut) {
        ActionSegment<int> discreteActions = actionsOut.DiscreteActions;
        discreteActions[0] = 0;
        if (Input.GetKey("right")) {
            discreteActions[0] = 1;
        }
    }
}
