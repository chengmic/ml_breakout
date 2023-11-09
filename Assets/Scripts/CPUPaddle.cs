using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using Vector3 = UnityEngine.Vector3;
using Vector2 = UnityEngine.Vector2;
using Unity.Barracuda;

public class CPUPaddle : Agent
{
    public CPUBall ball;
    public CPUGameManager gm;
    public float left_x_bound = -3.6f;
    public float right_x_bound = 3.6f;
    public NNModel modelAsset;
    private Model m_RuntimeModel;

    void Start()
    {
        m_RuntimeModel = ModelLoader.Load(modelAsset);
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        // paddle movement
        float move_x = actions.ContinuousActions[0];
        int launch = actions.DiscreteActions[0];
        float speed = 20f;

        transform.localPosition += new Vector3(move_x, 0f) * Time.deltaTime * speed;

        // handles paddle boundaries without needing a rigidbody, can probably be cleaned up to be less lines
        Vector3 new_position = transform.localPosition;
        new_position.x = Mathf.Clamp(new_position.x, left_x_bound, right_x_bound);
        transform.localPosition = new_position;

        // ball release
        if (launch == 1 && !ball.ball_in_play)
        {
            ball.Launch();
        }
    }
}
