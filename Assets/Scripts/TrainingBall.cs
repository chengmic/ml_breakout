using UnityEngine;

public class TrainingBall : MonoBehaviour
{
    Rigidbody2D rb;
    public TrainingManager training_manager;
    public TrainingPaddle paddle;

    // Ball movement variables
    public float ball_speed = 5f;
    public bool ball_in_play = false;
    private float ball_movement_reward_delay = 2f;
    private float ball_movement_reward_timer;

    // Ball x position variables
    private float x_position_check_timer;
    float x_position_check_timer_delay = 4f;
    float check_x_position; 

    // Stuck ball variables
    float ball_stuck_timer = 30f;
    float ball_stuck_check_timer;

    public Vector3 start_position;


    // Start is called before the first frame update
    void Start()
    {
        ball_movement_reward_timer = ball_movement_reward_delay;
        x_position_check_timer = x_position_check_timer_delay;
        ball_stuck_check_timer = ball_stuck_timer;
        check_x_position = transform.localPosition.x;

        // start ball on paddle
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.zero;
        
        transform.localPosition = paddle.transform.localPosition + new Vector3(0, 0.3f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        ball_movement_reward_timer -= Time.deltaTime;
        x_position_check_timer -= Time.deltaTime;
        ball_stuck_check_timer -= Time.deltaTime;

        if (!ball_in_play)
        {
            // If ball is not in play, reset its position to ontop of the paddle
            transform.localPosition = paddle.transform.localPosition + new Vector3(0, 0.3f, 0);

            if (Input.GetButtonDown("Jump"))
            {
                // When user presses spacebar, send ball upwards from paddle
                Launch();
            }
        }
        else
        {
            // If ball is in play, check vertical speed. If ball is in motion, provide small reward
            float ball_vertical_velocity = rb.velocity.y;
            if (ball_vertical_velocity != 0f && ball_movement_reward_timer <= 0)
            {
                paddle.BallStillMoving();
                ball_movement_reward_timer = ball_movement_reward_delay;
            }
            
            // Resets ball if it starts to slow down too much
            if (rb.velocity.magnitude <= 3.5f)
            {
                ball_in_play = false;
                Debug.Log("ball was moving too slowly");
            }
        }

        //Horizontal location
        if (x_position_check_timer <= 0)
        {
            float current_check_x_position = transform.localPosition.x;

            if (Mathf.Abs(current_check_x_position - check_x_position) < 0.01f)
            {
                paddle.NoHorizontalMovementPunishment();
            }

            x_position_check_timer = x_position_check_timer_delay;
            check_x_position = current_check_x_position;
        }

        // Check if ball is stuck going up and down in corner
        if (ball_stuck_check_timer <= 0)
        {
            float current_check_x_position = transform.localPosition.x;

            if (Mathf.Abs(current_check_x_position - check_x_position) < 0.01f)
            {
                ball_in_play = false;
                Debug.Log("ball was too close to wall and got stuck");
            }
            ball_stuck_check_timer = ball_stuck_timer;
        }
    }

    public void Launch()
    {
        // Select angle of launch vector
        Vector2 angle_of_launch = new Vector2(0.1f, 1.2f);

        // Check if ball is left or right of spawn location
        if (transform.localPosition.x > start_position.x)
        {
            // if to the right, launch to the left
            angle_of_launch.x = -angle_of_launch.x;
        }
        // Only keep direction of vector
        Vector2 launch_direction = angle_of_launch.normalized;

        rb.velocity = launch_direction * ball_speed;
        ball_in_play = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // when ball collides with lower bound, lose life and respawn ball at starting position
        if (collision.gameObject.CompareTag("Lower Bound"))
        {
            training_manager.ChangeLives(+1);
            paddle.BallBelowPaddle();

            // Sets in play to false with resets the ball on top of the paddle
            ball_in_play = false;
        }
    }
}

