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
        //Effects
        [SerializeField] [ReorderableList] [Expandable]
        protected Effect[] abilityEffects;
        //Cooldown
        [SerializeField] [MinValue(0)] [BoxGroup("Cooldown")]
        protected float cooldown;
        [SerializeField] [BoxGroup("Cooldown")]
        protected bool beforeEnd;
        [ReadOnly] [SerializeField] [BoxGroup("Cooldown")]
        protected bool onCooldown;
        [ReadOnly] [SerializeField] [BoxGroup("Cooldown")]
        protected bool isUsing;
        //Toggle
        [SerializeField] [BoxGroup("Toggle")]
        protected bool isToggle;
        [SerializeField] [BoxGroup("Toggle")] [ShowIf("isToggle")]
        protected float tickRate;
        //Input buffering
        [SerializeField] [BoxGroup("Input Buffering")]
        protected bool bufferInput;
        [SerializeField] [BoxGroup("Input Buffering")] [ShowIf("bufferInput")]
        protected float bufferTime;
        [ReadOnly] [SerializeField] [BoxGroup("Input Buffering")] [ShowIf("bufferInput")]
        protected bool isBuffered;
        protected CoroutineHandle bufferHandle;
        //Targetting
        [SerializeField] [BoxGroup("Targetting")]
        protected LayerMask layerMask;
        [SerializeField] [BoxGroup("Targetting")]
        protected bool multiTarget;
        //Hitboxes
        [SerializeField] [ReorderableList]
        protected Hitbox[] hitboxes;
        
        Status status;

        public virtual void UseAbility(Transform transform)
        {
            if (onCooldown)
            {
                if (bufferInput)
                {
                    isBuffered = true;
                    Timing.KillCoroutines(bufferHandle);
                    bufferHandle = Timing.CallDelayed(bufferTime, () =>
                    {
                        isBuffered = false;
                    });
                }
                return;
            }
            //Toggle ability
            if (isToggle)
            {
                isUsing = !isUsing;
                if (isUsing)
                {
                    Timing.RunCoroutine(ToggleRoutine(transform));
                }
                return;
            }
            //Non-toggle ability
            if (isUsing)
            {
                return;
            }
            else
            {
                isUsing = true;
                StartAbility(transform);
            }
        }

        protected IEnumerator<float> ToggleRoutine(Transform transform)
        {
            while (isUsing)
            {
                Tick(transform);

                yield return Timing.WaitForSeconds(tickRate);
            }
            EndAbility(transform);
        }

        protected IEnumerator<float> CooldownRoutine(Transform transform)
        {
            onCooldown = true;
            StartCooldown();
            yield return Timing.WaitForSeconds(cooldown);
            onCooldown = false;
            EndCooldown(transform);
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

        //Events
        protected virtual void StartAbility(Transform transform) {
            if (beforeEnd)
            {
                Timing.RunCoroutine(CooldownRoutine(transform));
            }
        }
        protected virtual void EndAbility(Transform transform)
        {
            if (!isToggle)
            {
                isUsing = false;
            }
            if (!beforeEnd)
            {
                Timing.RunCoroutine(CooldownRoutine(transform));
            }
        }
        protected virtual void Tick(Transform transform) { }
        protected virtual void StartCooldown() { }
        protected virtual void EndCooldown(Transform transform)
        {
            if (isBuffered)
            {
                UseAbility(transform);
                isBuffered = false;
                Timing.KillCoroutines(bufferHandle);
            }
        }
        protected virtual void StartEffects() { }
        protected virtual void EndEffects() { }
    }
}
