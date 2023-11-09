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

public class CPUPaddle : Agent
{
    public CPUBall ball;
    public CPUGameManager gm;
    public NNModel model_asset;
    private Model runtime_model;
    private IWorker worker;
    private string output_layer_name;

    void Start()
    {
        runtime_model = ModelLoader.Load(model_asset);
        worker = WorkerFactory.CreateWorker(WorkerFactory.Type.CSharpBurst, runtime_model);
        output_layer_name = runtime_model.outputs[runtime_model.outputs.Count - 1];
    }

    void Update()
    {
        var inputs = new Dictionary<string, Tensor>();
        inputs["obs_0"] = new Tensor(1, 1, 1, 10);
        inputs["action_masks"] = new Tensor(1, 1, 1, 2);

        worker.Execute(inputs);

        Tensor O = worker.PeekOutput(output_layer_name);
        Debug.Log(O);

        inputs["obs_0"].Dispose();
        inputs["action_masks"].Dispose();
    }
}
