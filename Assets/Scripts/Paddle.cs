using System.Collections;
using System.Collections.Generic;
using Unity.MLAgents.Integrations.Match3;
using UnityEngine;

public class Paddle : MonoBehaviour
{

    public float speed = 10;
    float horizontal_input;
    public float x_boundry = 8.15f;
    float x_velocity;
    Vector3 direction;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void FixedUpdate()          // FixedUpdate() used to make paddle movement more constant
    {
        // move paddle
        horizontal_input = Input.GetAxis("Horizontal");
        if (horizontal_input < 0)
        {
            direction = Vector3.left;
        }
        if (horizontal_input > 0)
        {
            direction = Vector3.right;
        }

        x_velocity = Mathf.Abs(horizontal_input) * speed;
        Vector3 new_position = transform.position + direction * x_velocity * Time.deltaTime;
        new_position.x = Mathf.Clamp(new_position.x, -x_boundry, x_boundry);        // handle paddle boundries
        transform.position = new_position;

    }
}
