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
        Debug.Log(actions.ContinuousActions[0]);
    }
}
