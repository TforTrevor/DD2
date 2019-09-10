using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MEC;
using RoboRyanTron.SearchableEnum;
using DD2.Util;

namespace DD2.Abilities
{
    public class PositionAbility : Ability
    {
        [SerializeField] float radius;
        [SerializeField] [SearchableEnum] LayerMask layerMask;
        [SerializeField] bool multiTarget;

        public override void UseAbility(Vector3 position)
        {
            if (onCooldown)
            {
                return;
            }

            Collider[] hitColliders = Physics.OverlapSphere(position, radius, layerMask);

            if (multiTarget)
            {
                foreach (Collider hitCollider in hitColliders)
                {
                    for (int i = 0; i < abilityEffects.Length; i++)
                    {
                        abilityEffects[i].ApplyEffect(this, hitCollider.transform);
                    }
                }
            }
            else
            {
                Collider collider = Utilities.GetClosestToPoint(hitColliders, position);
                for (int i = 0; i < abilityEffects.Length; i++)
                {
                    abilityEffects[i].ApplyEffect(this, collider.transform);
                }
            }

            cooldownRoutine = Timing.RunCoroutine(CooldownRoutine());
        }
    }
}