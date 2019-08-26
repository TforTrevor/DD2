using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SmartData.SmartVector3;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] Vector3Reader moveInput;
    [SerializeField] Vector3Reader lookInput;
    [SerializeField] float sensitivity;
    [SerializeField] float maxSpeed;
    [SerializeField] float acceleration;
    [SerializeField] Vector2 direction;

    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Rotate();
        Move();
        rb.AddForce(Vector3.down * 9.81f);
    }

    void Move()
    {
        if (Mathf.Abs(moveInput.value.x) > 0)
        {
            direction.x += moveInput.value.x * (1 / acceleration) * Time.deltaTime;
            direction.x = Mathf.Clamp(direction.x, -1, 1);
        }
        else
        {
            if (Mathf.Abs(direction.x) < 0.05f)
            {
                direction.x = 0;
            }
            else
            {
                direction.x -= Mathf.Sign(direction.x) * (1 / acceleration) * Time.deltaTime;
            }
        }
        if (Mathf.Abs(moveInput.value.y) > 0)
        {
            direction.y += moveInput.value.y * (1 / acceleration) * Time.deltaTime;
            direction.y = Mathf.Clamp(direction.y, -1, 1);
        }
        else
        {
            if (Mathf.Abs(direction.y) < 0.05f)
            {
                direction.y = 0;
            }
            else
            {
                direction.y -= Mathf.Sign(direction.y) * (1 / acceleration) * Time.deltaTime;
            }
        }
        Vector2 temp = Vector2.ClampMagnitude(direction, 1);
        Vector3 speed = new Vector3(temp.x * maxSpeed, rb.velocity.y, temp.y * maxSpeed);
        rb.velocity = transform.TransformVector(speed);
    }

    void Rotate()
    {
        transform.Rotate(Vector3.up, lookInput.value.x * sensitivity * Time.deltaTime);
    }
}
