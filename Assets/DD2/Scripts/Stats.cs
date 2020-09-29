using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

namespace DD2
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Stats")]
    public class Stats : ScriptableObject
    {
        [SerializeField] [BoxGroup("Character")] float maxHealth;
        [SerializeField] [BoxGroup("Character")] int maxMana;
        [SerializeField] [BoxGroup("Character")] float attackDamage;
        [SerializeField] [BoxGroup("Character")] float attackSpeed;
        [SerializeField] [BoxGroup("Character")] float moveSpeed;
        [SerializeField] [BoxGroup("Character")] float presence;
        [SerializeField] [BoxGroup("Character")] float castSpeed;

        [SerializeField] [EnumFlags] [BoxGroup("Resistances")] ElementType resistedElements;

        [SerializeField] [BoxGroup("Range")] float attackRange;
        [SerializeField] [BoxGroup("Range")] float attackAngle;

        public float MaxHealth { get => maxHealth; private set => maxHealth = value; }
        public int MaxMana { get => maxMana; private set => maxMana = value; }
        public float AttackDamage { get => attackDamage; private set => attackDamage = value; }
        public float AttackSpeed { get => attackSpeed; private set => attackSpeed = value; }
        public float MoveSpeed { get => moveSpeed; private set => moveSpeed = value; }
        public float Presence { get => presence; private set => presence = value; }
        public ElementType ResistedElements { get => resistedElements; private set => resistedElements = value; }
        public float AttackRange { get => attackRange; private set => attackRange = value; }
        public float AttackAngle { get => attackAngle; private set => attackAngle = value; }
    }
}
