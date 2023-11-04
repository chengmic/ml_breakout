using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameArea : MonoBehaviour
{   
    public Ball ball;
    public MovePaddleAgent agent;

    public int TotalBricks()
    {
        Transform[] all_children = gameObject.GetComponentsInChildren<Transform>(true); 
        int total_bricks = 0;

        for (int i = 0; i < all_children.Length; i++)
        {
            if (all_children[i].CompareTag("Brick"))
            {
                total_bricks++;
            }
        }
        return total_bricks;
    }

    public void ResetArea()
    {
        agent.GameWon();
        ball.transform.localPosition = agent.transform.localPosition + new Vector3(0, 0.3f, 0);

        Transform[] all_children = gameObject.GetComponentsInChildren<Transform>(true); 
        for (int i = 0; i < all_children.Length; i++)
        {
            all_children[i].gameObject.SetActive(true);
        }
    }
}
