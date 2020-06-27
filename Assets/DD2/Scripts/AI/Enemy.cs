using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MEC;
using UnityEngine.AI;
using DD2.Abilities;

namespace DD2.AI
{
    public class Enemy : EntityAI
    {
        public Transform target;
        [SerializeField] float distance;
        [SerializeField] Vector3 offset;
        [SerializeField] LayerMask groundedMask;
        [SerializeField] float ragdollTime;
        [SerializeField] bool isRagdolled;

        NavMeshAgent navMeshAgent;
        NavMeshObstacle navMeshObstacle;
        CoroutineHandle moveRoutine;
        Vector3 previousPosition;
        CoroutineHandle ragdollHandle;

        public bool IsRagdolled { get => isRagdolled; protected set => isRagdolled = value; }

        protected override void Awake()
        {
            base.Awake();
            navMeshAgent = GetComponent<NavMeshAgent>();
            navMeshObstacle = GetComponent<NavMeshObstacle>();
            RandomizeStats();
        }

        protected override void Start()
        {
            base.Start();
            CurrentMana = Stats.MaxMana;
            navMeshAgent.speed = Stats.MoveSpeed;
            IsGrounded = true;
        }

        void RandomizeStats()
        {
            if (Stats != null)
            {
                Stats.RandomizeHealth(Stats.MaxHealth * 0.25f);
                Stats.RandomizeResistances();
            }            
        }

        public virtual void Ragdoll()
        {
            if (IsRagdolled)
            {
                Timing.KillCoroutines(ragdollHandle);
            }
            else
            {
                navMeshAgent.ResetPath();
                Vector3 velocity = navMeshAgent.velocity;
                SetAgentActive(false);
                //rb.velocity = velocity;
                IsRagdolled = true;
            }            
        }

        public override void AddForce(Vector3 force, ForceMode forceMode)
        {
            if (IsAlive)
            {
                Ragdoll();
                base.AddForce(force, forceMode);
            }            
        }

        protected override void OnGrounded()
        {
            base.OnGrounded();
            if (IsRagdolled)
            {
                ragdollHandle = Timing.CallDelayed(ragdollTime, () =>
                {
                    if (IsGrounded)
                    {
                        navMeshAgent.nextPosition = transform.position;
                        SetAgentActive(true);
                        IsRagdolled = false;
                        if (!navMeshAgent.isOnNavMesh)
                        {
                            Debug.Log("Not on mesh");
                        }
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

        public void MoveToPosition(Vector3 position)
        {
            //previousPosition = position;
            //navMeshObstacle.enabled = false;
            //navMeshAgent.enabled = true;
            //NavMeshPath path = new NavMeshPath();
            //if (navMeshAgent.isOnNavMesh)
            //{
            //    navMeshAgent.CalculatePath(position, path);
            //}
            //Vector3[] corners = path.corners;
            //navMeshAgent.enabled = false;
            //navMeshObstacle.enabled = true;
            //Timing.KillCoroutines(moveRoutine);
            //moveRoutine = Timing.RunCoroutine(MoveRoutine(corners));
            navMeshAgent.SetDestination(position);
        }

        IEnumerator<float> MoveRoutine(Vector3[] corners)
        {
            for (int i = 0; i < corners.Length; i++)
            {
                Vector3 corner = corners[i];
                corner.y += navMeshAgent.height / 2;
                transform.LookAt(corner);
                while (Vector3.Distance(transform.position, corner) > 0.1f)
                {
                    transform.position += transform.forward * Stats.MoveSpeed * Time.deltaTime;
                    yield return Timing.WaitForOneFrame;
                }
            }
        }

        void FixedUpdate()
        {
            if (IsRagdolled)
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
        }

        //void Update()
        //{
        //    if (animator != null)
        //    {
        //        animator.SetFloat("Speed", navMeshAgent.velocity.magnitude / navMeshAgent.speed);
        //    }            
        //}
    }
}