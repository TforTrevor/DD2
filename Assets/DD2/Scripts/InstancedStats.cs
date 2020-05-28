using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DD2
{
    public class InstancedStats
    {
        float maxHealth;
        int maxMana;
        float attackDamage;
        float attackSpeed;
        float moveSpeed;
        float presence;

        float physicalResist;
        float fireResist;
        float lightningResist;
        float energyResist;
        float waterResist;

        float attackRange;
        float attackAngle;
        float searchRange;
        float searchAngle;

        public float MaxHealth { get => maxHealth; set => maxHealth = value; }
        public int MaxMana { get => maxMana; set => maxMana = value; }
        public float AttackDamage { get => attackDamage; set => attackDamage = value; }
        public float AttackSpeed { get => attackSpeed; set => attackSpeed = value; }
        public float MoveSpeed { get => moveSpeed; set => moveSpeed = value; }
        public float Presence { get => presence; set => presence = value; }
        public float PhysicalResist { get => physicalResist; set => physicalResist = value; }
        public float FireResist { get => fireResist; set => fireResist = value; }
        public float LightningResist { get => lightningResist; set => lightningResist = value; }
        public float EnergyResist { get => energyResist; set => energyResist = value; }
        public float WaterResist { get => waterResist; set => waterResist = value; }
        public float AttackRange { get => attackRange; set => attackRange = value; }
        public float AttackAngle { get => attackAngle; set => attackAngle = value; }
        public float SearchRange { get => searchRange; set => searchRange = value; }
        public float SearchAngle { get => searchAngle; set => searchAngle = value; }

        public InstancedStats(Stats stats)
        {
            MaxHealth = stats.MaxHealth;
            MaxMana = stats.MaxMana;
            AttackDamage = stats.AttackDamage;
            AttackSpeed = stats.AttackSpeed;
            MoveSpeed = stats.MoveSpeed;
            Presence = stats.Presence;
            PhysicalResist = stats.PhysicalResist;
            FireResist = stats.FireResist;
            LightningResist = stats.LightningResist;
            EnergyResist = stats.EnergyResist;
            WaterResist = stats.WaterResist;
            AttackRange = stats.AttackRange;
            AttackAngle = stats.AttackAngle;
            SearchRange = stats.SearchRange;
            SearchAngle = stats.SearchAngle;
        }
    }
}