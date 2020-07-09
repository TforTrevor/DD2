using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MEC;
using DD2.AI;

namespace DD2.Abilities
{
    public class TargetAbility : Ability
    {
        protected override void Tick(Entity target, object payload)
        {
            base.Tick(target, payload);
            ApplyEffects(target, payload);
        }
    }
}