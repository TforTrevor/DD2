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

        public float PhysicalResist;
        public float FireResist;
        public float LightningResist;
        public float EnergyResist;
        public float WaterResist;

        public float AttackRange;
        public float AttackAngle;
        public float SearchRange;
        public float SearchAngle;

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

        public void RandomizeHealth(float range)
        {
            MaxHealth += Random.Range(-range, range);
        }

        public void RandomizeResistances()
        {
            int resistance = Random.Range(0, 16);
            switch (resistance)
            {
                case 0:
                    FireResist = 10000;
                    break;
                case 1:
                    LightningResist = 10000;
                    Debug.Log("Lightning");
                    break;
                case 2:
                    EnergyResist = 10000;
                    break;
                case 3:
                    WaterResist = 10000;
                    break;
                case 4:
                    PhysicalResist = 10000;
                    break;
            }
        }
    }
}