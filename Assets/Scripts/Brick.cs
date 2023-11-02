using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Brick : MonoBehaviour
{
    public GameManager game_manager;
    public MovePaddleAgent paddleAgent;

    // Start is called before the first frame update
    void Start()
    {
        if (game_manager == null)
        {
            game_manager = FindObjectOfType<GameManager>();
        }

        if (paddleAgent == null)
        {
            paddleAgent = FindObjectOfType<MovePaddleAgent>();
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // destory brick
        if (collision.gameObject.CompareTag("Ball"))
        {
            Destroy(this.gameObject);
            game_manager.ChangeScore(100);
            game_manager.ReduceBrickCount();


            paddleAgent.ballHitsBrick();
            if (game_manager.bricks_remaining==0){
                paddleAgent.gameWon();
            }

        }
    }
}