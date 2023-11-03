using UnityEngine;

public class Ball : MonoBehaviour
{
    Rigidbody2D rb;
    public GameManager game_manager;
    public MovePaddleAgent paddle_agent;
    [SerializeField] private Transform paddle;
    public float ball_speed = 6.7f;
    public bool ball_in_play = false;

    // Start is called before the first frame update
    void Start()
    {
        // start ball on paddle
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.zero;
        transform.position = paddle.position + new Vector3(0, 0.3f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (!ball_in_play)
        {
            // If ball is not in play, reset its position to ontop of the paddle
            transform.position = paddle.position + new Vector3(0, 0.3f, 0);

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
            if (ball_vertical_velocity != 0f)
            {
                paddle_agent.BallStillMoving();
            }
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