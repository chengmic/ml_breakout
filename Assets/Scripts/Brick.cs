using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Brick : MonoBehaviour
{
    public GameManager gamemgr;

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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // destory brick
        if (collision.gameObject.CompareTag("Ball"))
        {
            Destroy(this.gameObject);
            gamemgr.changeScore(100);
        }
    }
}
