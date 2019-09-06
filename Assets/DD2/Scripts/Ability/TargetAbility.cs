using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MEC;

namespace DD2.Abilities
{
    public class TargetAbility : Ability
    {
        public override void UseAbility(Transform target)
        {
            if (onCooldown)
            {
                return;
            }

            for (int i = 0; i < abilityEffects.Length; i++)
            {
                abilityEffects[i].ApplyEffect(this, target);
            }

            cooldownRoutine = Timing.RunCoroutine(CooldownRoutine());
        }
    }
}