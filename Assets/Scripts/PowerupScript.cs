using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupScript : MonoBehaviour
{
    public float powerup_dropping_speed;

    public PlayerGameManager playerGameManager;
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

        //if (powerupManager == null)
            //{
            //powerupManager = FindObjectOfType<PowerupManager>();
        //}
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0, -powerup_dropping_speed * Time.deltaTime, 0);

        
    }

    private void OnTriggerEnter2D(Collider2D collider){

        if(collider.gameObject.CompareTag("Paddle")){

            Debug.Log("Powerup HIT");

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
