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
            sensor.AddObservation((Vector2)transform.position);
            sensor.AddObservation((Vector2)ball.transform.position);
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
        float speed = 10f;

        transform.position += new Vector3(move_x, 0f) * Time.deltaTime * speed;

        // handles paddle boundaries without needing a rigidbody, can probably be cleaned up to be less lines
        Vector3 new_position = transform.position;
        new_position.x = Mathf.Clamp(new_position.x, left_x_bound, right_x_bound);
        transform.position = new_position;

        // ball release
        if (launch == 1 && !ball.ball_in_play)
        {
            ball.Launch();
            Debug.Log("launched ball");
            AddReward(2f);
        }

        if (!ball.ball_in_play)
        {
            Debug.Log("held ball, -2");
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // small reward when paddle hits ball
        if (collision.gameObject.CompareTag("Ball"))
        {
            AddReward(5f);
            Debug.Log("Paddle and ball collision +5");
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
    }

    public void BallStillMoving()
    {
        AddReward(0.2f);
        Debug.Log("Ball Moving, +1");
    }
}
