using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityAtoms.BaseAtoms;
using System;
using UnityEngine.InputSystem;

namespace DD2
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] Stats stats;
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
        [SerializeField] Animator animator;

        Rigidbody rb;
        int currentJumps;
        int maximumJumps = 1;
        Vector2 lookInput;

        public bool IsMoving { get; private set; }
        public bool IsGrounded { get; private set; }
        public Vector2 MoveInput { get; private set; }
        public Vector3 Velocity { get; private set; }
        public float VerticalVelocity { get; private set; }

        void Awake()
        {
            rb = GetComponent<Rigidbody>();
        }

        void OnEnable()
        {
            InputManager.Instance.Actions.Player.Move.performed += OnMove;
            InputManager.Instance.Actions.Player.Look.performed += OnLook;
            InputManager.Instance.Actions.Player.Jump.performed += Jump;
        }

        void OnDisable()
        {
            InputManager.Instance.Actions.Player.Move.performed -= OnMove;
            InputManager.Instance.Actions.Player.Look.performed -= OnLook;
            InputManager.Instance.Actions.Player.Jump.performed -= Jump;
        }

        void OnMove(InputAction.CallbackContext context)
        {
            MoveInput = context.ReadValue<Vector2>();
        }

        void OnLook(InputAction.CallbackContext context)
        {
            lookInput = context.ReadValue<Vector2>();
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
            if (MoveInput.magnitude > 0)
            {
                float tempSpeed = stats.MoveSpeed;
                float tempAcceleration = acceleration;
                if (!IsGrounded)
                {
                    tempAcceleration = airAcceleration;
                    tempSpeed = Mathf.Max(Velocity.magnitude, tempSpeed);
                }

                Vector3 inputVector = new Vector3(MoveInput.x, 0, MoveInput.y).normalized;

                Vector3 targetVelocity = transform.TransformDirection(inputVector) * tempSpeed;
                Vector3 force = (targetVelocity - Velocity).normalized * tempAcceleration;
                Velocity += force * Time.deltaTime;

                if (Velocity.magnitude > 0)
                {
                    IsMoving = true;
                }
            }
            else
            {
                if (IsGrounded)
                {
                    if (Velocity.magnitude > 0.25f)
                    {
                        Velocity -= Velocity.normalized * brakeSpeed * Time.deltaTime;
                    }
                    else
                    {
                        Velocity = Vector3.zero;
                        IsMoving = false;
                    }
                }
            }
        }

        void Rotate()
        {
            transform.Rotate(Vector3.up, lookInput.x * sensitivity * 0.01f * Time.timeScale);
        }

        public void Jump(InputAction.CallbackContext context)
        {
            //Only call when jump is pressed down
            bool value = context.ReadValueAsButton();
            if (IsGrounded && value && currentJumps < maximumJumps)
            {
                VerticalVelocity += jumpForce;
                currentJumps++;
                animator.SetTrigger("Jump");
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