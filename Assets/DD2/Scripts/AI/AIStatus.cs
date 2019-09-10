using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace DD2.AI
{
    public class AIStatus : Status
    {
        public Transform firePosition;
        public Transform target;
        [HideInInspector] public NavMeshAgent navMeshAgent;

        private void Awake()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
        }

        public Vector3 GetFirePosition()
        {
            return firePosition.position;
        }
    }
}