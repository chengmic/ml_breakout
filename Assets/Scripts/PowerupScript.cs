using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupScript : MonoBehaviour
{
    public float powerup_dropping_speed;

    public PlayerGameManager player_game_manager;

     public GameObject lower_bound;

    public  SpriteRenderer lower_bound_sprite_renderer;
    public PlayerBall player_ball;

    public int below_paddle_safe;

    void Start()
    {
        GameObject playerBallObject = GameObject.FindGameObjectWithTag("PlayerBall"); 
        if (playerBallObject != null){
            player_ball = playerBallObject.GetComponent<PlayerBall>();
            }

        if (player_game_manager == null)
            {
            player_game_manager = FindObjectOfType<PlayerGameManager>();
        }

        if (lower_bound == null)
        {
            GameObject lower_bound_holder = GameObject.FindGameObjectWithTag("Lower Bound");
            lower_bound = lower_bound_holder;
            if (lower_bound != null)
            {
                lower_bound_sprite_renderer = lower_bound.GetComponent<SpriteRenderer>();
            }
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        // Have the powerups fall downards at a specific speed after spawned in
        transform.Translate(0, -powerup_dropping_speed * Time.deltaTime, 0);

        if (player_ball.belowPaddleSafe>=1){
            lower_bound_sprite_renderer.enabled = true;
        }
        else{
            lower_bound_sprite_renderer.enabled = false;
        }

        
    }

    private void OnTriggerEnter2D(Collider2D collider){

        if(collider.gameObject.CompareTag("Paddle")){

            Debug.Log("Powerup HIT");

            // Specific checks for each type of powerup

            if (gameObject.tag == "BigBallPowerup"){
                player_ball.BigBallPowerup();
            }
            if (gameObject.tag == "Powerup"){
                player_game_manager.ChangeLives(1);
            }
            if (gameObject.tag == "BelowPaddleSavePowerup"){
                below_paddle_safe +=1;
                player_ball.belowPaddleSafe+=1;
                Debug.Log(below_paddle_safe);
            }
            
            Destroy(gameObject);
        }

        if (collider.gameObject.CompareTag("Lower Bound")){
            Destroy(gameObject);
            

            
        }

    }
}
