using UnityEngine;
using Vector3 = UnityEngine.Vector3;
using Unity.Barracuda;
using System.Collections.Generic;

public class CPUPaddle : MonoBehaviour
{
    public CPUBall ball;
    public CPUGameManager gm;
    public float left_x_bound = -3.6f;
    public float right_x_bound = 3.6f;
    public NNModel model_asset;
    private Model runtime_model;
    private IWorker worker;

    void Start()
    {
        runtime_model = ModelLoader.Load(model_asset);
        worker = WorkerFactory.CreateWorker(WorkerFactory.Type.CSharp, runtime_model);
    }

    void Update()
    {
        // Current Ball and Paddle locations
        Vector2 current_paddle_location = transform.localPosition;
        Vector2 current_ball_Location = ball.transform.localPosition;

        //Distance from paddle to ball
        float distance_to_ball = Vector2.Distance(current_paddle_location, current_ball_Location);

        //Direction of ball from paddle
        Vector2 ball_direction_from_paddle_plus_distance = current_ball_Location - current_paddle_location;
        Vector2 ball_direction_from_paddle = ball_direction_from_paddle_plus_distance.normalized;

        //Distance From Wall to Paddle
        float paddle_distance_from_right_wall = current_paddle_location.x - right_x_bound;
        float paddle_distance_from_left_wall = left_x_bound - current_paddle_location.x;


        // inputs
        var inputs = new Dictionary<string, Tensor>();
        inputs["obs_0"] = new Tensor(1, 1, 1, 10);
        inputs["obs_0"][0] = current_paddle_location.x;
        inputs["obs_0"][1] = current_paddle_location.y;
        inputs["obs_0"][2] = current_ball_Location.x;
        inputs["obs_0"][3] = current_ball_Location.y;
        inputs["obs_0"][4] = ball.ball_in_play ? 1.0f : 0.0f;
        inputs["obs_0"][5] = distance_to_ball;
        inputs["obs_0"][6] = ball_direction_from_paddle.x;
        inputs["obs_0"][7] = ball_direction_from_paddle.y;
        inputs["obs_0"][8] = paddle_distance_from_right_wall;
        inputs["obs_0"][9] = paddle_distance_from_left_wall;
        inputs["action_masks"] = new Tensor(1, 1, 1, 2);
        worker.Execute(inputs);

        // outputs
        Tensor discrete_actions = worker.PeekOutput("discrete_actions");
        Tensor continuous_actions = worker.PeekOutput("continuous_actions");

        // use this to track the decision values the model is making
        // Debug.Log(discrete_actions[0]);
        // Debug.Log(continuous_actions[0]);

        // dispose
        inputs["obs_0"].Dispose();
        inputs["action_masks"].Dispose();

        // ball release
        if (discrete_actions[0] == 1 && !ball.ball_in_play)
        {
            ball.Launch();
        }

        // paddle movement
        float move_x = continuous_actions[0];
        float speed = 10f;

        Vector3 new_position = transform.localPosition + new Vector3(move_x, 0f) * Time.deltaTime * speed;
        new_position.x = Mathf.Clamp(new_position.x, left_x_bound, right_x_bound);
        transform.localPosition = new_position;

        // use this to track the paddle position
        // Debug.Log($"new_position.x: {new_position.x}");
        // Debug.Log($"new_position.y: {new_position.y}");
    }
}
