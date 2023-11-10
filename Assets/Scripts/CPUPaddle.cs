using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using Vector3 = UnityEngine.Vector3;
using Vector2 = UnityEngine.Vector2;
using Unity.Barracuda;
using System;
using Grpc.Core;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine.UIElements;

public class CPUPaddle : Agent
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
        worker = WorkerFactory.CreateWorker(WorkerFactory.Type.CSharpBurst, runtime_model);
    }

    void Update()
    {
        var inputs = new Dictionary<string, Tensor>();
        inputs["obs_0"] = new Tensor(1, 1, 1, 10);
        inputs["action_masks"] = new Tensor(1, 1, 1, 2);
        worker.Execute(inputs);

        Tensor discrete_actions = worker.PeekOutput("discrete_actions");
        Tensor continuous_actions = worker.PeekOutput("continuous_actions");

        // use this to track the decision values the model is making
        // Debug.Log(discrete_actions[0]);
        // Debug.Log(continuous_actions[0]);

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

        Vector3 new_position = transform.position + new Vector3(move_x, 0f) * Time.deltaTime * speed;
        new_position.x = Mathf.Clamp(new_position.x, left_x_bound, right_x_bound);
        transform.position = new_position;

        // use this to track the paddle position
        // Debug.Log($"new_position.x: {new_position.x}");
    }
}
