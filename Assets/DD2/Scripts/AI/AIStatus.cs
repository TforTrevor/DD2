using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using MEC;
using DD2.AI.Context;
using Apex.AI.Components;
using Apex.AI;
using System;

namespace DD2.AI
{
    public class AIStatus : Status, IContextProvider
    {
        public Transform target;
        [HideInInspector] public NavMeshAgent navMeshAgent;
        [SerializeField] float distance;
        [SerializeField] Vector3 offset;
        [SerializeField] LayerMask groundedMask;
        public EnemyContext enemyContext;

        protected override void Awake()
        {
            base.Awake();
            navMeshAgent = GetComponent<NavMeshAgent>();
            enemyContext = new EnemyContext(this);
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
            Debug.DrawRay(transform.position + offset, Vector3.down * distance, Color.cyan, 2);
            if (Physics.Raycast(transform.position + offset, Vector3.down, out hit, distance, groundedMask))
            {
                SetGrounded(true);
            }
            else
            {
                SetGrounded(false);
            }
        }

        public IAIContext GetContext(Guid aiId)
        {
            return enemyContext;
        }
    }
}