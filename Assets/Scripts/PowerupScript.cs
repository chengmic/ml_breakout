using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupScript : MonoBehaviour
{
    public float powerup_dropping_speed;

    public PlayerGameManager playerGameManager;

     public GameObject lower_bound;

    public  SpriteRenderer lower_bound_sprite_renderer;
    public PlayerBall playerBall;

    public int belowPaddleSafe;

    //public PowerupManager powerupManager;
    // Start is called before the first frame update

    void Start()
    {
        GameObject playerBallObject = GameObject.FindGameObjectWithTag("PlayerBall"); 
        if (playerBallObject != null){
            playerBall = playerBallObject.GetComponent<PlayerBall>();
            }

        if (playerGameManager == null)
            {
            playerGameManager = FindObjectOfType<PlayerGameManager>();
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

        //if (powerupManager == null)
            //{
            //powerupManager = FindObjectOfType<PowerupManager>();
        //}
        
    }

    // Update is called once per frame
    void Update()
    {
        // Have the powerups fall downards at a specific speed after spawned in
        transform.Translate(0, -powerup_dropping_speed * Time.deltaTime, 0);

        if (playerBall.belowPaddleSafe>=1){
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
                playerBall.BigBallPowerup();
            }
            if (gameObject.tag == "Powerup"){
                playerGameManager.ChangeLives(1);
            }
            if (gameObject.tag == "BelowPaddleSavePowerup"){
                belowPaddleSafe +=1;
                playerBall.belowPaddleSafe+=1;
                Debug.Log(belowPaddleSafe);
            }
            
            //powerupManager.Instance.powerupDeleted(this.gameObject);
            Destroy(gameObject);
        }

        if (collider.gameObject.CompareTag("Lower Bound")){
            Destroy(gameObject);
            

            
        }

    }
}
