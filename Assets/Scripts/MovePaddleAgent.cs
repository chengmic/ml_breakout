using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using Vector3 = UnityEngine.Vector3;
using Vector2 = UnityEngine.Vector2;
using System;
using static UnityEngine.Rendering.VirtualTexturing.Debugging;

public class MovePaddleAgent : Agent
{
    public GameObject ball;
    public float left_x_bound = -3.6f;
    public float right_x_bound = 3.6f;
    public float ball_speed = 6.7f;

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation((Vector2)transform.position);
        sensor.AddObservation((Vector2)ball.transform.position);
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        // paddle movement
        float move_x = actions.ContinuousActions[0];
        // Debug.Log("move_x: " + move_x);
        float speed = 10f;

        transform.position += new Vector3(move_x, 0f) * Time.deltaTime * speed;

        // handles paddle boundaries without needing a rigidbody, can probably be cleaned up to be less lines
        Vector3 new_position = transform.position;
        new_position.x = Mathf.Clamp(new_position.x, left_x_bound, right_x_bound);
        transform.position = new_position;


        // ball release
        int release_ball = actions.DiscreteActions[0];
        Debug.Log("release_ball: " + release_ball);
        bool ball_in_play = ball.GetComponent<Ball>().ball_in_play;
        Debug.Log("ball_in_play: " + ball_in_play);

        if (ball_in_play == false)
        {
            if (release_ball % 2 == 1)
            {
                Debug.Log("made it here");
                Rigidbody2D rb = ball.GetComponent<Rigidbody2D>();
                rb.velocity = Vector2.up * ball_speed;
                ball.GetComponent<Ball>().ball_in_play = true;
                Debug.Log("launched the ball");
            }
        }
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
        // small reward for when ball is moving
        // WRITE HERE

        // large reward for breaking brick
        if (collision.gameObject.CompareTag("Brick"))
        {
            AddReward(20f);
            Debug.Log("Brick destroyed +20");
        }

        // small reward when paddle hits ball
        if (collision.gameObject.CompareTag("Ball"))
        {
            AddReward(10f);
            Debug.Log("Paddle and ball collision +10");
        }

        // large penalty for losing ball
        if (collision.gameObject.CompareTag("LowerBound"))
        {
            AddReward(-20f);
            Debug.Log("Lower bound hit -10");
        }
    }
}
