using UnityEngine;
using System.Collections.Generic;

public class VPlayerBrick : MonoBehaviour
{
    public PlayerGameManager player_game_manager;

    void Start()
    {
        if (player_game_manager == null)
        {
            player_game_manager = FindObjectOfType<PlayerGameManager>();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // destory brick
        if (collision.gameObject.CompareTag("PlayerBall"))
        {
            gameObject.SetActive(false);
            player_game_manager.ChangeScore(100);
            player_game_manager.ReduceBrickCount();
        }
    }
}
