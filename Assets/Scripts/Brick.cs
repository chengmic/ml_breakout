using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Brick : MonoBehaviour
{
    public GameManager gamemgr;

<<<<<<< Updated upstream
    // Start is called before the first frame update
    void Start()
    {
        if (gamemgr == null)
        {
            gamemgr = FindObjectOfType<GameManager>();
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

=======
>>>>>>> Stashed changes
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // destory brick
        if (collision.gameObject.CompareTag("Ball"))
        {

            Destroy(this.gameObject);
            gamemgr.changeScore(100);
            gamemgr.reduce_brick_count();
        }
    }
}