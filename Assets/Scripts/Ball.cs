using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Ball : MonoBehaviour
{
    public float min_y_boundary =-5.0f;
    Rigidbody2D rb;
    public int lives = 3;


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
        if (lives <= 0)
        {
            // HANDLE GAME OVER
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // when ball collides with lower bound, lose life and respawn ball at starting position
        if (collision.gameObject.CompareTag("Lower Bound"))
        {
            transform.position = Vector3.zero;
            rb.velocity = Vector2.down * 10.0f;
            lives -= 1;
        }
    }
}
