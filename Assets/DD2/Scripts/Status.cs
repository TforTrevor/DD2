using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace DD2
{
    public class Status : MonoBehaviour
    {
        [Expandable] public Stats stats;
        [SerializeField] float currentHealth;
        public Transform target;
        public NavMeshAgent navMeshAgent;

        private void Awake()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
        }

        void Start()
        {
            Initialize();
        }

        void Initialize()
        {
            currentHealth = stats.GetMaxHealth();
        }

        public void Damage(float damage)
        {
            currentHealth -= damage;
        }

        public float GetCurrentHealth()
        {
            return currentHealth;
        }
    }
}
