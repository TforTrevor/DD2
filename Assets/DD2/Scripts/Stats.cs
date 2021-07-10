using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace DD2
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Stats")]
    public class Stats : ScriptableObject
    {
        [SerializeField] float maxHealth;
        [SerializeField] int maxMana;
        [SerializeField] float attackDamage;
        [SerializeField] float attackSpeed;
        [SerializeField] float moveSpeed;
        [SerializeField] float presence;

        [SerializeField] ElementType resistedElements;
        [SerializeField] float physicalResist;
        [SerializeField] float fireResist;
        [SerializeField] float lightningResist;
        [SerializeField] float energyResist;
        [SerializeField] float waterResist;

        [SerializeField] float attackRange;
        [SerializeField] float attackAngle;
        [SerializeField] float searchRange;
        [SerializeField] float searchAngle;

        public float MaxHealth { get => maxHealth; private set => maxHealth = value; }
        public int MaxMana { get => maxMana; private set => maxMana = value; }
        public float AttackDamage { get => attackDamage; private set => attackDamage = value; }
        public float AttackSpeed { get => attackSpeed; private set => attackSpeed = value; }
        public float MoveSpeed { get => moveSpeed; private set => moveSpeed = value; }
        public float Presence { get => presence; private set => presence = value; }
        public ElementType ResistedElements { get => resistedElements; private set => resistedElements = value; }
        public float PhysicalResist { get => physicalResist; private set => physicalResist = value; }
        public float FireResist { get => fireResist; private set => fireResist = value; }
        public float LightningResist { get => lightningResist; private set => lightningResist = value; }
        public float EnergyResist { get => energyResist; private set => energyResist = value; }
        public float WaterResist { get => waterResist; private set => waterResist = value; }
        public float AttackRange { get => attackRange; private set => attackRange = value; }
        public float AttackAngle { get => attackAngle; private set => attackAngle = value; }
        public float SearchRange { get => searchRange; private set => searchRange = value; }
        public float SearchAngle { get => searchAngle; private set => searchAngle = value; }
    }
}
