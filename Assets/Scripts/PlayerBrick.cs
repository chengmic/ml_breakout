using UnityEngine;
using System.Collections.Generic;

public class PlayerBrick : MonoBehaviour
{
    public PlayerGameManager player_game_manager;
    public List<GameObject> powerups;

    public int selected_powerup_index;

    private float powerup_spawn_probability = 0.20f; // Percentage

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
            if (powerups.Count > 0 && Random.value <= powerup_spawn_probability)
            {
                selected_powerup_index = Random.Range(0, powerups.Count);
                _ = Instantiate(powerups[selected_powerup_index], transform.position, transform.rotation);
            }

            gameObject.SetActive(false);
            player_game_manager.ChangeScore(100);
            player_game_manager.ReduceBrickCount();
        }
    }
}
