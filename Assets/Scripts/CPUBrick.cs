using UnityEngine;

public class CPUBrick : MonoBehaviour
{
    public CPUGameManager game_manager;
    public CPUPaddle cpu_paddle;

    // Start is called before the first frame update
    void Start()
    {
        if (game_manager == null)
        {
            game_manager = FindObjectOfType<CPUGameManager>();
        }

        if (cpu_paddle == null)
        {
            cpu_paddle = FindObjectOfType<CPUPaddle>();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // destory brick
        if (collision.gameObject.CompareTag("Ball"))
        {
            gameObject.SetActive(false);
            game_manager.ChangeScore(100);
            game_manager.ReduceBrickCount();

            if (game_manager.bricks_remaining == 0)
            {
                game_manager.LoseGame();
            }
        }
    }
}