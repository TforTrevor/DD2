using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DD2.Actions
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Actions/Damage")]
    public class Damage : Action
    {
        [SerializeField] ElementType elementType;
        [SerializeField] DamageType damageType;
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
            float multiplier = 1;
            if (caller != null)
            {
                multiplier = 1 + caller.Stats.AttackDamage / 50;
            }
            float damage = this.damage * multiplier * GetDamageMultiplier(target.Stats);
            target.Damage(caller, damage);
        }

        void PercentMaxHealth(Entity target, Entity caller)
        {
            float damage = target.Stats.MaxHealth * this.damage / 100 * GetDamageMultiplier(target.Stats);
            target.Damage(caller, damage);
        }

        void PercentCurrentHealth(Entity target, Entity caller)
        {
            float damage = target.CurrentHealth * this.damage / 100 * GetDamageMultiplier(target.Stats);
            target.Damage(caller, damage);
        }

        float GetDamageMultiplier(Stats targetStats)
        {
            switch (elementType)
            {
                case ElementType.Fire:
                    return ResistanceFormula(targetStats.FireResist);
                case ElementType.Lightning:
                    return ResistanceFormula(targetStats.LightningResist);
                case ElementType.Energy:
                    return ResistanceFormula(targetStats.EnergyResist);
                case ElementType.Water:
                    return ResistanceFormula(targetStats.WaterResist);
                default:
                    return ResistanceFormula(targetStats.PhysicalResist);
            }
        }

        float ResistanceFormula(float input)
        {
            if (input >= 0)
            {
                return 100 / (100 + input);
            }
            return 2 - (100 / (100 - input));
        }

        enum DamageType
        {
            Flat,
            PercentMaxHealth,
            PercentCurrentHealth
        }
    }
}
