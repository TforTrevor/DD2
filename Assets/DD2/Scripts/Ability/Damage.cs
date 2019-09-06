using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RoboRyanTron.SearchableEnum;

namespace DD2.Abilities.Effects
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Abilities/Effects/Damage")]
    public class Damage : Effect
    {
        [SerializeField] [SearchableEnum] ElementType elementType;
        [SerializeField] [SearchableEnum] DamageType damageType;
        [SerializeField] float damage;

        public override void ApplyEffect(Ability controller, Transform hit)
        {
            Status status = hit.GetComponent<Status>();
            switch (damageType)
            {
                case DamageType.PercentMaxHealth:
                    PercentMaxHealth(status);
                    break;
                case DamageType.PercentCurrentHealth:
                    PercentCurrentHealth(status);
                    break;
                default:
                    Flat(status);
                    break;
            }
        }

        void Flat(Status hitStatus)
        {
            hitStatus.Damage(damage);
        }

        void PercentMaxHealth(Status hitStatus)
        {
            float damage = hitStatus.stats.GetMaxHealth() * this.damage;
            hitStatus.Damage(damage);
        }

        void PercentCurrentHealth(Status hitStatus)
        {
            float damage = hitStatus.GetCurrentHealth() * this.damage;
            hitStatus.Damage(damage);
        }

        enum DamageType
        {
            Flat,
            PercentMaxHealth,
            PercentCurrentHealth
        }
    }
}
