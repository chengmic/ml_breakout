using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using Vector3 = UnityEngine.Vector3;
using Vector2 = UnityEngine.Vector2;

public class MovePaddleAgent : Agent
{
    //[SerializeField] private Transform ball;
    public Ball ball;
    public float left_x_bound = -3.6f;
    public float right_x_bound = 3.6f;

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation((Vector2)transform.position);
        sensor.AddObservation((Vector2)ball.transform.position);

        sensor.AddObservation(ball.ball_in_play);
    }


    public override void OnActionReceived(ActionBuffers actions)
    {
        float move_x = actions.ContinuousActions[0];
        int launch = actions.DiscreteActions[0];
        float speed = 10f;

        transform.position += new Vector3(move_x, 0f) * Time.deltaTime * speed;

        // handles paddle boundaries without needing a rigidbody, can probably be cleaned up to be less lines
        Vector3 new_position = transform.position;
        new_position.x = Mathf.Clamp(new_position.x, left_x_bound, right_x_bound);
        transform.position = new_position;

        if (launch == 1 && !ball.ball_in_play)
        {
            Debug.Log("launched ball");
            ball.ball_in_play = true;
            ball.Launch();
        }

        if (!ball.ball_in_play)
        {
            Debug.Log("held ball");
            AddReward(-2f);
        }
    }

    // this is for when the user controls the paddle
    // in unity, click on paddle, look for behavior parameters under inspector, change behavior type to heuristic to control paddle
    // can probably remove later, was added in when following tutorial
    public override void Heuristic(in ActionBuffers actions_out)
    {
        ActionSegment<float> continuous_actions = actions_out.ContinuousActions;
        continuous_actions[0] = Input.GetAxisRaw("Horizontal");
    }

    // change from trigger to collision
    private void OnCollisionEnter2D(Collision2D collision) 
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            AddReward(10f);
        }
    }
}
