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

        Rigidbody rb;
        int currentJumps;
        int maximumJumps = 1;

        public bool IsMoving { get; private set; }
        public bool IsGrounded { get; private set; }
        public Vector3 Velocity { get; private set; }
        public float VerticalVelocity { get; private set; }

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

            if (!IsGrounded)
            {
                VerticalVelocity += Physics.gravity.y * Time.deltaTime;
            }

            RaycastHit hit;
            bool hitGround = Physics.Raycast(transform.position + new Vector3(0, groundedOffset, 0), Vector3.down, out hit, groundedLength, groundedMask);
            if (!hitGround)
            {
                IsGrounded = false;
            }

            Vector3 projectedVelocity = Vector3.ProjectOnPlane(Velocity, hit.normal);
            rb.velocity = new Vector3(projectedVelocity.x, projectedVelocity.y + VerticalVelocity, projectedVelocity.z);
        }

        void Move()
        {
            if (moveInput.Value.magnitude > 0)
            {
                IsMoving = true;

                float tempSpeed = stats.MoveSpeed;
                float tempAcceleration = acceleration;
                if (!IsGrounded)
                {
                    tempAcceleration = airAcceleration;
                    tempSpeed = Mathf.Max(Velocity.magnitude, tempSpeed);
                }

                Vector3 inputVector = new Vector3(moveInput.Value.x, 0, moveInput.Value.y).normalized;

                Vector3 targetVelocity = transform.TransformDirection(inputVector) * tempSpeed;
                Vector3 force = (targetVelocity - Velocity).normalized * tempAcceleration;
                Velocity += force * Time.deltaTime;
            }
            else
            {
                IsMoving = false;

                if (IsGrounded)
                {
                    if (Velocity.magnitude > 0.25f)
                    {
                        Velocity -= Velocity.normalized * brakeSpeed * Time.deltaTime;
                    }
                    else
                    {
                        Velocity = Vector3.zero;
                    }
                }
            }
        }

        void Rotate()
        {
            transform.Rotate(Vector3.up, lookInput.Value.x * sensitivity * 0.01f * Time.timeScale);
        }

        public void Jump(bool value)
        {
            //Only call when jump is pressed down
            if (IsGrounded && value && currentJumps < maximumJumps)
            {
                VerticalVelocity += jumpForce;
                currentJumps++;
            }
        }

        public void AddForce(Vector3 force, ForceMode forceMode)
        {
            rb.AddForce(force, forceMode);
        }

        void OnCollisionEnter(Collision collision)
        {
            if (Util.Utilities.IsInLayer(collision.collider.gameObject, groundedMask))
            {
                RaycastHit hit;
                if (Physics.Raycast(transform.position + new Vector3(0, groundedOffset, 0), Vector3.down, out hit, groundedLength, groundedMask))
                {
                    IsGrounded = true;
                    currentJumps = 0;
                    VerticalVelocity = 0;
                }
            }
        }

        void OnCollisionExit(Collision collision)
        {
            RaycastHit hit;
            bool hitGround = Physics.Raycast(transform.position + new Vector3(0, 0, 0), Vector3.down, out hit, 0.1f, groundedMask);
            if (Util.Utilities.IsInLayer(collision.collider.gameObject, groundedMask) && !hitGround)
            {
                IsGrounded = false;
            }
        }
    }
}