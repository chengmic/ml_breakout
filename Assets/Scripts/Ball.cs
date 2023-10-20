using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Ball : MonoBehaviour
{
    Rigidbody2D rb;
    public GameManager gameManager;
    [SerializeField] private Transform paddle;

    // Start is called before the first frame update
    void Start()
    {
        // start ball in downard direction
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.down * 10.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.bricks_remaining == 0) {
            this.gameObject.SetActive(false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // when ball collides with lower bound, lose life and respawn ball at starting position
        if (collision.gameObject.CompareTag("Lower Bound"))
        {
            gameManager.changeLives(-1);

            if (gameManager.lives_val <= 0)
            {
                rb.velocity = Vector2.zero;
                this.gameObject.SetActive(false);
                
                return;
            }
            // moves ball position above paddle when a new ball spawns
            transform.position = paddle.position + new Vector3(0, 0.5f, 0);;
        }
    }
}