﻿using MEC;
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
            if (!target.Stats.ResistedElements.HasFlag(elementType) || target.Stats.ResistedElements == ElementType.None)
            {
                if (damageType == DamageType.Flat)
                {
                    if (swapTargetAndCaller)
                        Flat(caller, target);
                    else
                        Flat(target, caller);
                }
                else if (damageType == DamageType.PercentMaxHealth)
                {
                    if (swapTargetAndCaller)
                        PercentMaxHealth(caller, target);
                    else
                        PercentMaxHealth(target, caller);
                }
                else if (damageType == DamageType.PercentCurrentHealth)
                {
                    if (swapTargetAndCaller)
                        PercentCurrentHealth(caller, target);
                    else
                        PercentCurrentHealth(target, caller);
                }
            }            
        }

        void Flat(Entity target, Entity caller)
        {
            float multiplier = 1;
            if (caller != null)
            {
                multiplier = 1 + caller.Stats.AttackDamage / 50;
            }
            if (ignoreStats)
            {
                target.Damage(caller, damage);
                caller?.Hitlag(HitlagFormula(damage));
            }
            else
            {
                float damage = this.damage * multiplier * GetDamageMultiplier(target.Stats);
                target.Damage(caller, damage);
                caller?.Hitlag(HitlagFormula(damage));
            }
        }

        void PercentMaxHealth(Entity target, Entity caller)
        {
            if (ignoreStats)
            {
                target.Damage(caller, damage);
                caller?.Hitlag(HitlagFormula(damage));
            }
            else
            {
                float damage = target.Stats.MaxHealth * this.damage / 100 * GetDamageMultiplier(target.Stats);
                target.Damage(caller, damage);
                caller?.Hitlag(HitlagFormula(damage));
            }            
        }

        void PercentCurrentHealth(Entity target, Entity caller)
        {
            if (ignoreStats)
            {
                target.Damage(caller, damage);
                caller?.Hitlag(HitlagFormula(damage));
            }
            else
            {
                float damage = target.CurrentHealth * this.damage / 100 * GetDamageMultiplier(target.Stats);
                target.Damage(caller, damage);
                caller?.Hitlag(HitlagFormula(damage));
            }            
        }

        float GetDamageMultiplier(InstancedStats targetStats)
        {
            if (targetStats != null)
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
