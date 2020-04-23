using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RoboRyanTron.SearchableEnum;

namespace DD2.Actions
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Actions/Damage")]
    public class Damage : Action
    {
        [SerializeField] [SearchableEnum] ElementType elementType;
        [SerializeField] [SearchableEnum] DamageType damageType;
        [SerializeField] float damage;

        public override void DoAction(Entity target, Entity caller, object payload)
        {
            switch (damageType)
            {
                case DamageType.PercentMaxHealth:
                    PercentMaxHealth(target, caller);
                    break;
                case DamageType.PercentCurrentHealth:
                    PercentCurrentHealth(target, caller);
                    break;
                default:
                    Flat(target, caller);
                    break;
            }
        }

        void Flat(Entity target, Entity caller)
        {
            target.Damage(caller, damage);
        }

        void PercentMaxHealth(Entity target, Entity caller)
        {
            float damage = target.GetStats().GetMaxHealth() * this.damage;
            target.Damage(caller, damage);
        }

        void PercentCurrentHealth(Entity target, Entity caller)
        {
            float damage = target.GetCurrentHealth() * this.damage;
            target.Damage(caller, damage);
        }

        enum DamageType
        {
            Flat,
            PercentMaxHealth,
            PercentCurrentHealth
        }
    }
}
