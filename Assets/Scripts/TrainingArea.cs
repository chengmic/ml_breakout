using UnityEngine;

public class TrainingArea : MonoBehaviour
{   
    public TrainingBall ball;
    public TrainingPaddle agent;

    public int TotalBricks()
    {
        //counts all children under parent object, set to true to count inactivate objects
        Transform[] all_children = gameObject.GetComponentsInChildren<Transform>(true); 
        int total_bricks = 0;

        for (int i = 0; i < all_children.Length; i++)
        {
            // uses brick tag to only count bricks
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

        // get all children and reactivate the bricks
        Transform[] all_children = gameObject.GetComponentsInChildren<Transform>(true); 
        for (int i = 0; i < all_children.Length; i++)
        {
            all_children[i].gameObject.SetActive(true);
        }
    }
}
