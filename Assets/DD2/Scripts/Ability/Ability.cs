using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MEC;
using NaughtyAttributes;
using RoboRyanTron.SearchableEnum;

namespace DD2.Abilities
{
    public class Ability : MonoBehaviour
    {
        [SerializeField] [ReorderableList] [Expandable] protected Effect[] abilityEffects;
        [SerializeField] protected float cooldown;
        [SerializeField] protected bool continuous;
        [SerializeField] [ShowIf("continuous")] protected float tickRate;
        [SerializeField] [ShowIf("continuous")] protected float duration;
        [SerializeField] protected LayerMask layerMask;
        [SerializeField] protected bool multiTarget;
        [SerializeField] Transform fireTransform;

        protected bool onCooldown;
        protected CoroutineHandle cooldownRoutine;
        protected CoroutineHandle continuousRoutine;

        public virtual void UseAbility(Vector3 position)
        {
            if (onCooldown) { return; }
            if (continuous)
            {
                continuousRoutine = Timing.RunCoroutine(ContinuousRoutine(position));
            }
            else
            {
                StartAbility(position);
                EndAbility(position);
            }
            cooldownRoutine = Timing.RunCoroutine(CooldownRoutine());
        }

        protected virtual void StartAbility(Vector3 position) { }
        protected virtual void EndAbility(Vector3 position) { }
        protected virtual void ContinuousTick(Vector3 position) { }
        protected virtual void StartCooldown() { }
        protected virtual void EndCooldown() { }
        protected virtual void StartEffects() { }
        protected virtual void EndEffects() { }

        protected IEnumerator<float> ContinuousRoutine(Vector3 position)
        {
            StartAbility(position);
            float tempDuration = duration;
            while (tempDuration > 0)
            {
                ContinuousTick(position);
                yield return Timing.WaitForSeconds(tickRate);
                tempDuration -= tickRate;
            }
            EndAbility(position);
        }

        protected IEnumerator<float> CooldownRoutine()
        {
            onCooldown = true;
            StartCooldown();
            yield return Timing.WaitForSeconds(cooldown);
            onCooldown = false;
            EndCooldown();
        }

        protected void ApplyEffects(Transform target)
        {
            StartEffects();
            for (int i = 0; i < abilityEffects.Length; i++)
            {
                abilityEffects[i].ApplyEffect(target);
            }
            EndEffects();
        }

        protected Vector3 GetFirePosition()
        {
            return fireTransform.position;
        }
    }
}
