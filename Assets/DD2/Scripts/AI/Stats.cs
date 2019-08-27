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
        [SerializeField] float range;
        [SerializeField] float cone;
        [SerializeField] float aggro;
        [SearchableEnum] [SerializeField] ElementType elementType;
        [SerializeField] float speed;

        public float GetMaxHealth()
        {
            return maxHealth;
        }

        public float GetAggro()
        {
            return aggro;
        }

        public float GetRange()
        {
            return range;
        }
    }
}
