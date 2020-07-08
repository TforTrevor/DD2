using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityAtoms.BaseAtoms;

namespace DD2
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] Stats stats;
        [SerializeField] Vector2Variable moveInput;
        [SerializeField] Vector2Variable lookInput;
        [SerializeField] BoolVariable enableMove;
        [SerializeField] BoolVariable enableLook;
        [SerializeField] float sensitivity;
        [SerializeField] float acceleration;
        [SerializeField] float airAcceleration;
        [SerializeField] float jumpForce;
        [SerializeField] Vector2 direction;
        [SerializeField] LayerMask groundedMask;
        [SerializeField] float groundedOffset;
        [SerializeField] float groundedLength;
        [SerializeField] float groundedRadius;

        Rigidbody rb;
        bool isMoving;
        bool isGrounded;
        int currentJumps;
        int maximumJumps = 1;

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
            if (enableLook.Value)
            {
                Rotate();
            }
            if (enableMove.Value)
            {
                Move();
            }
            rb.AddForce(Vector3.down * 9.81f);

            RaycastHit hit;
            if (Physics.SphereCast(transform.position + new Vector3(0, groundedOffset, 0), groundedRadius, Vector3.down, out hit, groundedLength, groundedMask)) 
            {
                IsGrounded = true;
                currentJumps = 0;
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

            float tempAcceleration = IsGrounded ? acceleration : airAcceleration;

            if (Mathf.Abs(moveInput.Value.x) > 0)
            {
                direction.x += moveInput.Value.x * (tempAcceleration) * Time.deltaTime;
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
                    direction.x -= Mathf.Sign(direction.x) * (tempAcceleration) * Time.deltaTime;
                }
            }
            if (Mathf.Abs(moveInput.Value.y) > 0)
            {
                direction.y += moveInput.Value.y * (tempAcceleration) * Time.deltaTime;
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
                    direction.y -= Mathf.Sign(direction.y) * (tempAcceleration) * Time.deltaTime;
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

        public void Jump(bool value)
        {
            //Only call when jump is pressed down
            if (IsGrounded && value && currentJumps < maximumJumps)
            {
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                currentJumps++;
            }
        }
    }
}