using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DD2
{
    public class InstancedStats
    {
        public float MaxHealth;
        public int MaxMana;
        public float AttackDamage;
        public float AttackSpeed;
        public float MoveSpeed;
        public float Presence;

        public ElementType ResistedElements;

        public float AttackRange;
        public float AttackAngle;

        public InstancedStats(Stats stats)
        {
            MaxHealth = stats.MaxHealth;
            MaxMana = stats.MaxMana;
            AttackDamage = stats.AttackDamage;
            AttackSpeed = stats.AttackSpeed;
            MoveSpeed = stats.MoveSpeed;
            Presence = stats.Presence;
            ResistedElements = stats.ResistedElements;
            AttackRange = stats.AttackRange;
            AttackAngle = stats.AttackAngle;
        }

        public void RandomizeHealth(float range)
        {
            MaxHealth += Random.Range(-range, range);
        }

        public void RandomizeResistances()
        {
            int resistance = Random.Range(0, 16);
        }

        public void TowerLevel()
        {
            float increase = 1.25f;
            MaxHealth *= increase;
            AttackDamage *= increase;
            AttackSpeed *= increase;
            Presence *= increase;
            AttackRange *= increase;
            AttackAngle *= increase;
        }
    }
}