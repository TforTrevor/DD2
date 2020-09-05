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
        [SerializeField] float brakeSpeed;
        [SerializeField] float jumpForce;
        [SerializeField] Vector2 direction;
        [SerializeField] LayerMask groundedMask;
        [SerializeField] float groundedOffset;
        [SerializeField] float groundedLength;
        [SerializeField] float groundedRadius;

        Rigidbody rb;
        int currentJumps;
        int maximumJumps = 1;

        public bool IsMoving { get; private set; }
        public bool IsGrounded { get; private set; }
        public Vector3 Velocity { get; private set; }

        void Awake()
        {
            rb = GetComponent<Rigidbody>();
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
            if (moveInput.Value.magnitude > 0)
            {
                IsMoving = true;

                float tempAcceleration = IsGrounded ? acceleration : airAcceleration;
                Velocity += transform.TransformVector(new Vector3(moveInput.Value.x * tempAcceleration * Time.deltaTime, 0, moveInput.Value.y * tempAcceleration * Time.deltaTime));
                Velocity = Vector3.ClampMagnitude(Velocity, stats.MoveSpeed);
            }
            else
            {
                IsMoving = false;

                if (IsGrounded && Velocity.magnitude > 0)
                {
                    Velocity -= Velocity.normalized * brakeSpeed * Time.deltaTime;
                }
            }

            rb.MovePosition(rb.position + Velocity * Time.deltaTime);
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

        public void AddForce(Vector3 force, ForceMode forceMode)
        {
            rb.AddForce(force, forceMode);
        }
    }
}