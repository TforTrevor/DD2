using DD2.UI;
using MEC;
using System;
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
        [SerializeField] bool ignoreStats;
        [SerializeField] bool swapTargetAndCaller;
        [SerializeField] float damage;
        [SerializeField] float hitlagMultiplier;

        public override void DoAction(Entity target, Entity caller, object payload)
        {
            Entity tempTarget = swapTargetAndCaller ? caller : target;
            Entity tempCaller = swapTargetAndCaller ? target : caller;
            float damage = this.damage;

            switch (damageType)
            {
                case DamageType.Flat:
                    float multiplier = tempCaller != null ? 1 + tempCaller.Stats.AttackDamage / 50 : 1;
                    damage = ignoreStats ? this.damage : this.damage * multiplier * GetDamageMultiplier(tempTarget);
                    break;
                case DamageType.PercentMaxHealth:
                    damage = tempTarget.Stats.MaxHealth * this.damage / 100 * GetDamageMultiplier(tempTarget);
                    break;
                case DamageType.PercentCurrentHealth:
                    damage = tempTarget.CurrentHealth * this.damage / 100 * GetDamageMultiplier(tempTarget);
                    break;
            }

            tempTarget.Damage(tempCaller, damage);
            if (tempCaller != null) tempCaller.Hitlag(HitlagFormula(damage));
            DamageUICanvas.Instance.ShowDamage(tempTarget, damage);
        }

        float GetDamageMultiplier(Entity entity)
        {
            if (entity.Stats.ResistedElements.HasFlag(elementType) && entity.Stats.ResistedElements != ElementType.None)
            {
                return 0;
            }
            else if (entity.StatusEffects.HasFlag(StatusEffect.Freeze))
            {
                return 2;
            }
            else
            {
                return 1;
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

        float HitlagFormula(float damage)
        {
            return hitlagMultiplier * damage / 1000;
        }

        enum DamageType
        {
            Flat,
            PercentMaxHealth,
            PercentCurrentHealth
        }
    }
}
