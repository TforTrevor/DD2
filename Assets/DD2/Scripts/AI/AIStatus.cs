using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using MEC;

namespace DD2.AI
{
    public class AIStatus : Status
    {
        public Transform target;
        [HideInInspector] public NavMeshAgent navMeshAgent;
        [SerializeField] float distance;
        [SerializeField] LayerMask groundedMask;

        protected override void Awake()
        {
            base.Awake();
            navMeshAgent = GetComponent<NavMeshAgent>();
        }

        public override void Ragdoll()
        {
            base.Ragdoll();
            navMeshAgent.ResetPath();
            Vector3 velocity = navMeshAgent.velocity;
            SetAgentActive(false);
            rb.velocity = velocity;
            ragdolled = true;
        }

        public override void AddForce(Vector3 force, ForceMode forceMode)
        {
            Ragdoll();
            base.AddForce(force, forceMode);
        }

        protected override void OnGrounded()
        {
            base.OnGrounded();
            if (ragdolled)
            {
                Timing.CallDelayed(stats.GetRagdollTime(), () =>
                {
                    if (grounded)
                    {
                        navMeshAgent.nextPosition = transform.position;
                        SetAgentActive(true);
                        ragdolled = false;
                    }
                });
            }
        }

        void SetAgentActive(bool value)
        {
            navMeshAgent.updatePosition = value;
            navMeshAgent.updateRotation = value;
            navMeshAgent.updateUpAxis = value;
            rb.isKinematic = value;
        }

        void FixedUpdate()
        {
            RaycastHit hit;
            Ray ray = new Ray(transform.position, Vector3.down);
            if (Physics.Raycast(ray, out hit, distance, groundedMask)) {
                SetGrounded(true);
            }
            else
            {
                SetGrounded(false);
            }
        }
    }
}