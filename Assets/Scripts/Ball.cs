using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Ball : MonoBehaviour
{
    Rigidbody2D rb;
    public GameManager game_manager;
    [SerializeField] private Transform paddle;
    public float ball_speed = 6.7f;
    public bool ball_in_play = false;

    // Start is called before the first frame update
    void Start()
    {
        // start ball in downard direction
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.zero;
        transform.position = paddle.position + new Vector3(0, 0.5f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (game_manager.bricks_remaining == 0) {
            this.gameObject.SetActive(false);
        }
        if (ball_in_play ==  false){
            // If ball is not in play, reset its position to ontop of the paddle
            transform.position = paddle.position + new Vector3(0, 0.3f, 0);
            
            if (Input.GetButtonDown("Jump")){
                // When user presses spacebar, send ball upwards from paddle
                rb.velocity = Vector2.up * ball_speed;
                ball_in_play = true;
            }
        }
    }

    public void Launch()
    {
        rb.velocity = Vector2.up * ball_speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // when ball collides with lower bound, lose life and respawn ball at starting position
        if (collision.gameObject.CompareTag("Lower Bound"))
        {
            game_manager.ChangeLives(-1);

            if (game_manager.lives_val <= 0)
            {
                rb.velocity = Vector2.zero;
                this.gameObject.SetActive(false);
                
                return;
            }
            // Sets in play to false with resets the ball on top of the paddle
            ball_in_play = false;
        }
    }
}