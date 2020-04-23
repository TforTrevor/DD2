using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RoboRyanTron.SearchableEnum;

namespace DD2
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Stats")]
    public class Stats : ScriptableObject
    {
        [SerializeField] float maxHealth;
        [SerializeField] float attackRange;
        [SerializeField] float attackAngle;
        [SerializeField] float searchRange;
        [SerializeField] float searchAngle;
        [SerializeField] float presence;
        [SearchableEnum] [SerializeField] ElementType elementType;
        [SerializeField] float speed;
        [SerializeField] float attackRate;
        [SerializeField] float ragdollTime;
        [SerializeField] float radius;

        public float GetMaxHealth()
        {
            return maxHealth;
        }

        public float GetPresence()
        {
            return presence;
        }

        public float GetAttackRange()
        {
            return attackRange;
        }

        public float GetAttackAngle()
        {
            return attackAngle;
        }

        public float GetSearchRange()
        {
            return searchRange;
        }

        public float GetSearchAngle()
        {
            return searchAngle;
        }

        public float GetAttackRate()
        {
            return attackRate;
        }

        public ElementType GetElementType()
        {
            return elementType;
        }

        public float GetRagdollTime()
        {
            return ragdollTime;
        }

        public float GetRadius()
        {
            return radius;
        }

        public float GetSpeed()
        {
            return speed;
        }
    }
}
