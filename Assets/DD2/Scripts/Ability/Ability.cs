using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MEC;
using NaughtyAttributes;
using RoboRyanTron.SearchableEnum;
using DD2.Actions;

namespace DD2.Abilities
{
    public class Ability : MonoBehaviour
    {
        //Effects
        [SerializeField] [ReorderableList] [Expandable]
        protected Action[] abilityEffects;
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
        protected float toggleTickRate;
        [ReadOnly] [SerializeField] [BoxGroup("Toggle")] [ShowIf("isToggle")]
        protected bool toggleState;
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
        [SerializeField] [BoxGroup("Hitboxes")]
        protected float hitboxTickRate;
        [SerializeField] [ReorderableList] [BoxGroup("Hitboxes")]
        protected Hitbox[] hitboxes;
        
        Entity entity;

        public virtual void UseAbility(Transform transform)
        {
            ////Input buffering
            //if (onCooldown || isUsing)
            //{
            //    if (bufferInput)
            //    {
            //        isBuffered = true;
            //        Timing.KillCoroutines(bufferHandle);
            //        bufferHandle = Timing.CallDelayed(bufferTime, () =>
            //        {
            //            isBuffered = false;
            //        });
            //    }
            //}

            if (!onCooldown)
            {
                //Toggle ability
                if (isToggle)
                {
                    if (isUsing)
                    {
                        toggleState = false;
                    }
                    else
                    {
                        toggleState = !toggleState;
                    }
                    if (toggleState)
                    {
                        Timing.RunCoroutine(ToggleRoutine(transform));
                    }
                }
                //Non-toggle ability
                else if (!isUsing)
                {
                    isUsing = true;
                    StartAbility(transform);
                }
            }            
        }

        protected IEnumerator<float> ToggleRoutine(Transform transform)
        {
            isUsing = true;
            while (toggleState)
            {
                Tick(transform);

                yield return Timing.WaitForSeconds(toggleTickRate);
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

        protected void ApplyEffects(Entity target, object payload)
        {
            StartEffects();
            for (int i = 0; i < abilityEffects.Length; i++)
            {
                abilityEffects[i].DoAction(target, entity, payload);
            }
            EndEffects();
        }

        protected Vector3 GetFirePosition()
        {
            return entity.GetFirePosition();
        }

        public void SetEntity(Entity entity)
        {
            this.entity = entity;
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
            isUsing = false;
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

        public bool GetToggleState()
        {
            return toggleState;
        }
    }
}
