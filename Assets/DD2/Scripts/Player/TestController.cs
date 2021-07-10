using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DD2.SOArchitecture;

public class TestController : MonoBehaviour
{
    [SerializeField] Vector3Variable inputVector;
    [SerializeField] Vector3Variable mouseInputVector;
    [SerializeField] float sensitivity = 1;
    [SerializeField] float maxSpeed = 8;
    [SerializeField] float acceleration = 5;
    [SerializeField] float groundedLength = 0.1f;
    [SerializeField] Vector3 groundedPosition;
    [SerializeField] LayerMask groundedMask;

    [SerializeField] float jumpVelocity = 10;
     [SerializeField] Vector3 gravity = Vector3.zero;

    bool grounded = false;

    void Start()
    {

    }

    void Update()
    {
        Move();
        Rotate();
        Gravity();        
    }

    void Move()
    {
        Vector3 moveVector = transform.TransformDirection(new Vector3(inputVector.Value.x, 0, inputVector.Value.y));
        transform.position += moveVector * maxSpeed * Time.deltaTime;
    }

    void Rotate()
    {
        transform.Rotate(transform.up, mouseInputVector.Value.x * sensitivity * Time.deltaTime);
    }

    public void Jump()
    {
        if (isGrounded())
        {
            gravity.y += jumpVelocity;
            grounded = false;
        }        
    }

    void Gravity()
    {
        grounded = isGrounded();
        if (!grounded)
        {
            gravity += Physics.gravity * Time.deltaTime;

            RaycastHit hit;
            if (Physics.Raycast(transform.position + groundedPosition, Vector3.down, out hit, gravity.magnitude * Time.deltaTime, groundedMask))
            {
                transform.position += Vector3.down * hit.distance;
                gravity = Vector3.zero;
            }
            else
            {
                transform.position += gravity * Time.deltaTime;
            }
        } 
        else
        {
            gravity = Vector3.zero;
        }
    }

    bool isGrounded()
    {
        return Physics.Raycast(transform.position + groundedPosition, -transform.up, groundedLength, groundedMask);
    }
}
