﻿using System.Collections;
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
        [SerializeField] float attackCone;
        [SerializeField] float searchRange;
        [SerializeField] float searchAngle;
        [SerializeField] float aggro;
        [SearchableEnum] [SerializeField] ElementType elementType;
        [SerializeField] float speed;
        [SerializeField] float attackRate;

        public float GetMaxHealth()
        {
            return maxHealth;
        }

        public float GetAggro()
        {
            return aggro;
        }

        public float GetAttackRange()
        {
            return attackRange;
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
    }
}
