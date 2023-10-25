using System.Collections;
using System.Collections.Generic;
using Unity.MLAgents.Integrations.Match3;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    public float speed = 10;
    float horizontal_input;
    public float x_boundary = 3.6f;
    float x_velocity;
    Vector3 direction;
    public GameManager game_manager;

    // Update is called once per frame
    void Update() 
    {
        if (game_manager.lives_val <= 0 || game_manager.bricks_remaining == 0) {
            transform.position = new Vector3(0, transform.position.y, transform.position.z);
            return;
        }
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
        new_position.x = Mathf.Clamp(new_position.x, -x_boundary, x_boundary);        // handle paddle boundaries
        transform.position = new_position;
    }
}
