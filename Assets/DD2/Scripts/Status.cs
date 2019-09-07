﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using DD2.Abilities;

namespace DD2
{
    public class Status : MonoBehaviour
    {
        [Expandable] public Stats stats;
        [SerializeField] float currentHealth;

        [SerializeField] [ReorderableList] Ability[] abilities;

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

        public Ability GetAbility(int index)
        {
            return abilities[index];
        }

        public Ability[] GetAllAbilities()
        {
            return abilities;
        }

        public float GetAbilityCount()
        {
            return abilities.Length;
        }

        public virtual Vector3 GetPosition()
        {
            return transform.position;
        }

        public virtual Vector3 GetForward()
        {
            return transform.forward;
        }
    }
}
