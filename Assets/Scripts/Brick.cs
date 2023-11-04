using UnityEngine;

public class Brick : MonoBehaviour
{
    public GameManager game_manager;
    public MovePaddleAgent paddle_agent;

    // Start is called before the first frame update
    void Start()
    {
        if (game_manager == null)
        {
            game_manager = FindObjectOfType<GameManager>();
        }

        if (paddle_agent == null)
        {
            paddle_agent = FindObjectOfType<MovePaddleAgent>();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // destory brick
        if (collision.gameObject.CompareTag("Ball"))
        {
            gameObject.SetActive(false);
            game_manager.ChangeScore(100);
            paddle_agent.BallHitsBrick();
            game_manager.ReduceBrickCount();

            if (game_manager.bricks_remaining == 0)
            {
                paddle_agent.GameWon();
            }
        }
    }
}