using UnityEngine;

public class TrainingBrick : MonoBehaviour
{
    public TrainingManager training_manager;
    public TrainingPaddle paddle;

    // Start is called before the first frame update
    void Start()
    {
        if (training_manager == null)
        {
            training_manager = FindObjectOfType<TrainingManager>();
        }

        if (paddle == null)
        {
            paddle = FindObjectOfType<TrainingPaddle>();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // destory brick
        if (collision.gameObject.CompareTag("Ball"))
        {
            gameObject.SetActive(false);
            paddle.BallHitsBrick();
            training_manager.ReduceBrickCount();
        }
    }
}