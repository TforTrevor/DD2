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

        public override void DoAction(Transform target, Entity status, Vector3 position)
        {
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

        void Flat(Entity hitStatus)
        {
            hitStatus.Damage(damage);
        }

        void PercentMaxHealth(Entity hitStatus)
        {
            float damage = hitStatus.stats.GetMaxHealth() * this.damage;
            hitStatus.Damage(damage);
        }

        void PercentCurrentHealth(Entity hitStatus)
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
