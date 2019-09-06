using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using DD2.Abilities;

namespace DD2
{
    public class Status : MonoBehaviour
    {
        [Expandable] public Stats stats;
        [SerializeField] float currentHealth;
        public Transform target;
        public NavMeshAgent navMeshAgent;
        [SerializeField] Ability ability;

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

        public Ability GetAbility()
        {
            return ability;
        }
    }
}
