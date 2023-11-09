using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPUGameArea : MonoBehaviour
{
    public CPUBall ball;
    public CPUPaddle cpu_paddle;

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
}
