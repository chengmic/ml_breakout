using UnityEngine;

public class TrainingBall : MonoBehaviour
{
    Rigidbody2D rb;
    public TrainingManager training_manager;
    public TrainingPaddle paddle;

    // Ball movement variables
    public float ball_speed = 5f;
    public bool ball_in_play = false;
    private float ball_movement_reward_delay = 0.8f;
    private float ball_movement_reward_timer;

    // Ball x position variables
    private float x_position_check_timer;
    float x_position_check_timer_delay = 5f;
    float check_x_position; 

    // Stuck ball variables
    float ball_stuck_timer = 30f;
    float ball_stuck_check_timer;

    // Powerup variables
    public Vector3 original_ball_size;
    public float large_ball_powerup_timer;


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

        // Store base ball size
        original_ball_size = transform.localScale;
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

        // Powerup code
        if (large_ball_powerup_timer > 0)
        {
            large_ball_powerup_timer -= Time.deltaTime;

            if (large_ball_powerup_timer == 0)
            {
                transform.localScale = original_ball_size;
            }
        }
    }

    public void Launch()
    {
        rb.velocity = Vector2.up * ball_speed;
        ball_in_play = true;
    }

    // Powerup code
    public void BigBallPowerup(){
        transform.localScale = original_ball_size * 2;
        large_ball_powerup_timer = 4f;
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

