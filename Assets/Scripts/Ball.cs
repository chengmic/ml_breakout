using UnityEngine;

public class Ball : MonoBehaviour
{
    Rigidbody2D rb;
    public GameManager game_manager;
    public MovePaddleAgent paddle_agent;
    [SerializeField] private Transform paddle;
    public float ball_speed = 6.7f;
    public bool ball_in_play = false;
    private float ball_movement_reward_delay = 0.8f;
    private float ball_movement_reward_timer;
    private float horizontal_location_check_timer;
    float horizontal_location_check_timer_delay = 1f;
    float check_horizontal_location; 

    // Start is called before the first frame update
    void Start()
    {
        ball_movement_reward_timer = ball_movement_reward_delay;
        horizontal_location_check_timer = horizontal_location_check_timer_delay;
        check_horizontal_location = transform.localPosition.x;
        // start ball on paddle
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.zero;
        transform.localPosition = paddle.localPosition + new Vector3(0, 0.3f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        ball_movement_reward_timer -= Time.deltaTime;
        horizontal_location_check_timer -= Time.deltaTime;

        if (!ball_in_play)
        {
            // If ball is not in play, reset its position to ontop of the paddle
            transform.localPosition = paddle.localPosition + new Vector3(0, 0.3f, 0);

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
            if (ball_vertical_velocity != 0f && ball_movement_reward_timer<=0)
            {
                paddle_agent.BallStillMoving();
                ball_movement_reward_timer = ball_movement_reward_delay;
            }
        }

        //Horizontal location

        if (horizontal_location_check_timer <= 0){
            float current_check_horizontal_location = transform.localPosition.x;

            if (Mathf.Abs(current_check_horizontal_location - check_horizontal_location) <0.01f){
                paddle_agent.NoHorizontalMovementPunishment();
            }

            horizontal_location_check_timer = horizontal_location_check_timer_delay;
            check_horizontal_location = current_check_horizontal_location;

        }

    }

    public void Launch()
    {
        rb.velocity = Vector2.up * ball_speed;
        ball_in_play = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // when ball collides with lower bound, lose life and respawn ball at starting position
        if (collision.gameObject.CompareTag("Lower Bound"))
        {
            game_manager.ChangeLives(-1);
            paddle_agent.BallBelowPaddle();

            if (game_manager.lives_val <= 0)
            {
                rb.velocity = Vector2.zero;
                return;
            }
            // Sets in play to false with resets the ball on top of the paddle
            ball_in_play = false;
        }
    }
}

