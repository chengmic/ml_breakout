using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using Vector3 = UnityEngine.Vector3;
using Vector2 = UnityEngine.Vector2;

public class MovePaddleAgent : Agent
{
    public Ball ball;
    public GameManager gm;
    public float left_x_bound = -3.6f;
    public float right_x_bound = 3.6f;

    public override void CollectObservations(VectorSensor sensor)
    {
        bool is_game_over = gm.lives_val <= 0 || gm.bricks_remaining == 0;

        // Add a boolean flag indicating whether the game is over
        sensor.AddObservation(is_game_over);
    
        if (!is_game_over)
        {
            // Only add these observations if the game is not over
            sensor.AddObservation((Vector2)transform.localPosition);
            sensor.AddObservation((Vector2)ball.transform.localPosition);
            sensor.AddObservation(ball.ball_in_play);
        }
        else
        {
            // Add placeholder values if the game is over
            sensor.AddObservation(Vector2.zero); // Placeholder for paddle position
            sensor.AddObservation(Vector2.zero); // Placeholder for ball position
            sensor.AddObservation(false);        // Placeholder for ball_in_play
        }
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        // paddle movement
        float move_x = actions.ContinuousActions[0];
        int launch = actions.DiscreteActions[0];
        float speed = 20f;

        transform.localPosition += new Vector3(move_x, 0f) * Time.deltaTime * speed;

        // handles paddle boundaries without needing a rigidbody, can probably be cleaned up to be less lines
        Vector3 new_position = transform.localPosition;
        new_position.x = Mathf.Clamp(new_position.x, left_x_bound, right_x_bound);
        transform.localPosition = new_position;

        // ball release
        if (launch == 1 && !ball.ball_in_play)
        {
            ball.Launch();
            AddReward(2f);
            Debug.Log("launched ball, +2");
        }

        if (!ball.ball_in_play)
        {
            AddReward(-0.5f);
            Debug.Log("held ball, -0.5");
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // small reward when paddle hits ball
        if (collision.gameObject.CompareTag("Ball"))
        {
            AddReward(0.01f);
            Debug.Log("Paddle and ball collision +0.01");
        }
    }

    public void BallBelowPaddle()
    {
        AddReward(-10f);
        Debug.Log("Lowerbound hit, -10");
    }

    public void BallHitsBrick()
    {
        AddReward(10f);
        Debug.Log("Brick Destroyed, +10");
    }

    public void GameWon()
    {
        AddReward(20f);
        Debug.Log("Game Won, +20");
        EndEpisode();
    }

    public void BallStillMoving()
    {
        AddReward(0.01f);
        Debug.Log("Ball Moving, +0.01");
    }
}
