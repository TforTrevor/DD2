using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DD2
{
    public class Status : MonoBehaviour
    {
        [Expandable] public Stats stats;
        [SerializeField] float currentHealth;
        public Transform target;

        void Start()
        {
            Initialize();
        }

        void Initialize()
        {
            currentHealth = stats.GetMaxHealth();
        }

        public Vector3 GetPosition()
        {
            return transform.position;
        }
    }
}
