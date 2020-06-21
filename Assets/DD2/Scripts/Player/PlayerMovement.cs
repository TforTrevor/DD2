using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DD2.SOArchitecture;

namespace DD2
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] Stats stats;
        [SerializeField] Vector3Variable moveInput;
        [SerializeField] Vector3Variable lookInput;
        [SerializeField] float sensitivity;
        [SerializeField] float acceleration;
        [SerializeField] float jumpForce;
        [SerializeField] Vector2 direction;
        [SerializeField] LayerMask groundedMask;
        [SerializeField] float groundedOffset;
        [SerializeField] float groundedLength;

        Rigidbody rb;
        bool isMoving;
        bool isGrounded;

        bool enableMovement = true;
        bool enableLook = true;

        public bool IsMoving { get => isMoving; private set => isMoving = value; }
        public bool IsGrounded { get => isGrounded; private set => isGrounded = value; }

        void Awake()
        {
            rb = GetComponent<Rigidbody>();
        }

        void Update()
        {
            //if (enableLook)
            //    Rotate();
        }

        void FixedUpdate()
        {
            if (enableLook)
            {
                Rotate();
            }
            if (enableMovement)
            {
                Move();
            }
            rb.AddForce(Vector3.down * 9.81f);

            Debug.DrawRay(transform.position + Vector3.up * groundedOffset, Vector3.down * groundedLength, Color.red);
            RaycastHit hit;
            if (Physics.Raycast(transform.position + Vector3.up * groundedOffset, Vector3.down, out hit, groundedLength, groundedMask))
            {
                IsGrounded = true;
            }
            else
            {
                IsGrounded = false;
            }
        }

        void Move()
        {
            if (Mathf.Abs(moveInput.Value.x) > 0 || Mathf.Abs(moveInput.Value.y) > 0)
            {
                IsMoving = true;
            }
            else
            {
                IsMoving = false;
            }

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
            Vector3 speed = new Vector3(temp.x * stats.MoveSpeed, rb.velocity.y, temp.y * stats.MoveSpeed);
            rb.velocity = transform.TransformVector(speed);
        }

        void Rotate()
        {
            Quaternion deltaRotation = Quaternion.AngleAxis(lookInput.Value.x * sensitivity * Time.deltaTime, Vector3.up);
            rb.MoveRotation(rb.rotation * deltaRotation);

            //transform.Rotate(Vector3.up, lookInput.Value.x * sensitivity * Time.deltaTime);
        }

        public void Jump()
        {
            if (IsGrounded)
            {
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }
        }

        public void ToggleMovement(bool value)
        {
            enableMovement = value;
        }

        public void ToggleLook(bool value)
        {
            enableLook = value;
        }
    }
}