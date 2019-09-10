using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MEC;
using NaughtyAttributes;
using DD2.AI;

namespace DD2.Abilities
{
    public class Ability : MonoBehaviour
    {
        [SerializeField] [ReorderableList] [Expandable] protected Effect[] abilityEffects;
        [SerializeField] protected float cooldown;
        protected bool onCooldown;
        protected CoroutineHandle cooldownRoutine;

        public virtual void UseAbility() { }
        public virtual void UseAbility(Transform target) { }
        public virtual void UseAbility(AIStatus status) { }

        protected IEnumerator<float> CooldownRoutine()
        {
            yield return Timing.WaitForSeconds(cooldown);
            onCooldown = false;
        }
    }
}
