using UnityEngine;
using System.Collections.Generic;

public class PlayerBrick : MonoBehaviour
{
    public PlayerGameManager player_game_manager;
    public List<GameObject> powerups;

    public int selected_powerup_index;

    public PowerupManager powerupManager;

    void Start()
    {
        if (player_game_manager == null)
        {
            player_game_manager = FindObjectOfType<PlayerGameManager>();
        }

        if (powerupManager == null)
            {
            powerupManager = FindObjectOfType<PowerupManager>();
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // destory brick
        if (collision.gameObject.CompareTag("PlayerBall"))
        {

          if (powerups.Count > 0 && Random.value <= 0.99f){
            selected_powerup_index = Random.Range(0, powerups.Count);
            GameObject newPowerup = Instantiate(powerups[selected_powerup_index],transform.position, transform.rotation);
            //powerupManager.newPowerupCreated(newPowerup);
            
            }
            gameObject.SetActive(false);
            player_game_manager.ChangeScore(100);
            player_game_manager.ReduceBrickCount();

  
        }
    }

    public void RandomPowerup(){

    }
}
