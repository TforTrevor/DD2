using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DD2.SOArchitecture;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] Vector3Variable moveInput;
    [SerializeField] Vector3Variable lookInput;
    [SerializeField] float sensitivity;
    [SerializeField] float maxSpeed;
    [SerializeField] float acceleration;
    [SerializeField] Vector2 direction;
    [SerializeField] Transform cameraParent;

    Rigidbody rb;
    float xRot;

    public bool enableMovement = true;
    public bool enableLook = true;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        if (enableLook)
            Rotate();
        if (enableMovement)
            Move();
        rb.AddForce(Vector3.down * 9.81f);
    }

    void Move()
    {
        if (Mathf.Abs(moveInput.Value.x) > 0)
        {
            direction.x += moveInput.Value.x * (1 / acceleration) * Time.deltaTime;
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
        if (Mathf.Abs(moveInput.Value.y) > 0)
        {
            direction.y += moveInput.Value.y * (1 / acceleration) * Time.deltaTime;
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
        Quaternion deltaRotation = Quaternion.AngleAxis(lookInput.Value.x * sensitivity * Time.deltaTime, Vector3.up);
        rb.MoveRotation(rb.rotation * deltaRotation);

        Vector3 v = cameraParent.localEulerAngles;
        v.x += lookInput.Value.y * sensitivity * Time.deltaTime;
        cameraParent.localEulerAngles = v;
        //transform.Rotate(Vector3.up, lookInput.value.x * sensitivity * Time.deltaTime);
    }

    public void ToggleCursorLock()
    {
        if (Cursor.lockState == CursorLockMode.Locked)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }        
    }
}
