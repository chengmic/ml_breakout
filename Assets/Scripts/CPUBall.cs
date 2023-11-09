using UnityEngine;

public class CPUBall : MonoBehaviour
{
    Rigidbody2D rb;
    public CPUGameManager game_manager;
    public CPUPaddle cpu_paddle;
    [SerializeField] private Transform paddle;
    public float ball_speed = 5.0f;
    public bool ball_in_play = false;

    void Start()
    {
        // start ball on paddle
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.zero;
        transform.localPosition = paddle.localPosition + new Vector3(0, 0.3f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (!ball_in_play)
        {
            // If ball is not in play, reset its position to ontop of the paddle
            transform.localPosition = paddle.localPosition + new Vector3(0, 0.3f, 0);
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
