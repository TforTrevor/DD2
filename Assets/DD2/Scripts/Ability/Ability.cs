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
        Status status;
        [SerializeField] [ReorderableList] [Expandable]
        protected Effect[] abilityEffects;
        [SerializeField] [MinValue(0)]
        protected float cooldown;
        [SerializeField] [BoxGroup("Continuous")]
        protected bool isContinuous;
        [SerializeField] [ShowIf("isContinuous")] [BoxGroup("Continuous")]
        protected float tickRate;
        [SerializeField] [ShowIf("isContinuous")] [BoxGroup("Continuous")]
        protected float duration;
        [SerializeField] protected LayerMask layerMask;
        [SerializeField] protected bool multiTarget;
        [SerializeField] [BoxGroup("Input Buffering")]
        bool enableInputBuffer;
        [SerializeField] [ShowIf("enableInputBuffer")] [MinValue(0)] [BoxGroup("Input Buffering")]
        float bufferTime;
        [BoxGroup("Input Buffering")]
        bool buffer;

        protected bool onCooldown;
        protected CoroutineHandle cooldownRoutine;
        protected CoroutineHandle continuousRoutine;
        protected CoroutineHandle bufferRoutine;
        protected CoroutineHandle bufferTimeRoutine;

        public virtual void UseAbility(Vector3 position)
        {
            if (onCooldown)
            {
                if (enableInputBuffer)
                {
                    Timing.KillCoroutines(bufferTimeRoutine);
                    Timing.KillCoroutines(bufferRoutine);
                    bufferTimeRoutine = Timing.RunCoroutine(BufferTimerRoutine());
                    bufferRoutine = Timing.RunCoroutine(BufferRoutine(position));
                }
                
                return;
            }
            if (isContinuous)
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

        protected IEnumerator<float> BufferRoutine(Vector3 position)
        {
            while (buffer)
            {
                yield return Timing.WaitForOneFrame;
                UseAbility(position);
            }
        }

        protected IEnumerator<float> BufferTimerRoutine()
        {
            buffer = true;
            yield return Timing.WaitForSeconds(bufferTime);
            buffer = false;
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
            return status.GetFirePosition();
        }

        public void SetStatus(Status status)
        {
            this.status = status;
        }
    }
}
