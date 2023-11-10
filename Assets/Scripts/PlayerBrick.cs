using UnityEngine;

public class PlayerBrick : MonoBehaviour
{
    public PlayerGameManager player_game_manager;

    void Start()
    {
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // destory brick
        if (collision.gameObject.CompareTag("Ball"))
        {
            gameObject.SetActive(false);
            player_game_manager.ChangeScore(100);
            player_game_manager.ReduceBrickCount();
        }
    }
}
