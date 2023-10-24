using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

public class MovePaddleAgent : Agent
{
    [SerializeField] private Transform ball;

    public override void OnEpisodeBegin()
    {
        transform.position = new Vector3(0, -3.7356f);
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation((Vector2)transform.position);
        sensor.AddObservation((Vector2)ball.position);
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        float move_x = actions.ContinuousActions[0];
        float speed = 10f;

        transform.position += new Vector3(move_x, 0f) * Time.deltaTime * speed;
    }

    // this is for when the user controls the paddle
    // in unity, click on paddle, look for behavior paarameters under inspector, change behavior type to heuristic to control paddle
    // can probably remove later, was added in when following tutorial
    public override void Heuristic(in ActionBuffers actions_out)
    {
        ActionSegment<float> continuous_actions = actions_out.ContinuousActions;
        continuous_actions[0] = Input.GetAxisRaw("Horizontal");
    }

    private void OnTriggerEnter2D(Collider2D collision) 
    {
        if (collision.name == "Ball")
        {
            AddReward(10f);
        }
    }
}
