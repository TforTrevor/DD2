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
        private float toggleTickRate;
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
        
        protected Entity entity;

        public float ToggleTickRate { get => toggleTickRate; set => toggleTickRate = value; }

        protected virtual void Awake()
        {

        }

        public virtual void UseAbility(Entity target, object payload)
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
                        Timing.RunCoroutine(ToggleRoutine(target, payload));
                    }
                }
                //Non-toggle ability
                else if (!isUsing)
                {
                    isUsing = true;
                    StartAbility(target, payload);
                }
            }            
        }

        protected IEnumerator<float> ToggleRoutine(Entity target, object payload)
        {
            isUsing = true;
            while (toggleState)
            {
                Tick(target, payload);

                yield return Timing.WaitForSeconds(ToggleTickRate);
            }
            EndAbility(target, payload);
        }

        protected IEnumerator<float> CooldownRoutine(Entity target, object payload)
        {
            onCooldown = true;
            StartCooldown();
            yield return Timing.WaitForSeconds(cooldown);
            onCooldown = false;
            EndCooldown(target, payload);
        }

        protected virtual void ApplyEffects(Entity target, object payload)
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
        protected virtual void StartAbility(Entity target, object payload) 
        {
            if (beforeEnd)
            {
                Timing.RunCoroutine(CooldownRoutine(target, payload));
            }
        }
        protected virtual void EndAbility(Entity target, object payload)
        {
            isUsing = false;
            if (!beforeEnd)
            {
                Timing.RunCoroutine(CooldownRoutine(target, payload));
            }
        }
        protected virtual void Tick(Entity target, object payload) { }
        protected virtual void StartCooldown() { }
        protected virtual void EndCooldown(Entity target, object payload)
        {
            if (isBuffered)
            {
                UseAbility(target, payload);
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
