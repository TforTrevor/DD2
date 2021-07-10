using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MEC;

using RoboRyanTron.SearchableEnum;
using DD2.Actions;

namespace DD2.Abilities
{
    public class Ability : MonoBehaviour
    {
        //Effects
        [SerializeField] [Expandable]
        protected Action[] tickActions;
        [SerializeField]  [Expandable]
        protected Action[] abilityEffects;
        //Cooldown
        [SerializeField]
        protected float cooldown;
        [SerializeField]
        protected bool beforeEnd;
        [SerializeField]
        protected bool onCooldown;
        [SerializeField]
        protected bool isUsing;
        //Toggle
        [SerializeField]
        protected bool isToggle;
        [SerializeField]
        private float toggleTickRate;
        [SerializeField]
        private bool toggleState;
        //Input buffering
        [SerializeField]
        protected bool bufferInput;
        [SerializeField]
        protected float bufferTime;
        [SerializeField]
        protected bool isBuffered;
        protected CoroutineHandle bufferHandle;
        //Targetting
        [SerializeField]
        private LayerMask layerMask;
        [SerializeField]
        private LayerMask confuseLayerMask;
        [SerializeField]
        protected bool multiTarget;
        [SerializeField]
        protected bool hitSelf;
        
        protected Entity entity;

        public float ToggleTickRate { get => toggleTickRate; private set => toggleTickRate = value; }
        public bool ToggleState { get => toggleState; private set => toggleState = value; }
        protected LayerMask LayerMask { get => entity != null && entity.StatusEffects.HasFlag(StatusEffect.Confuse) ? confuseLayerMask : layerMask; }

        protected virtual void Awake()
        {

        }

        public virtual void UseAbility(Entity target, object payload)
        {
            if (!onCooldown)
            {
                //Toggle ability
                if (isToggle)
                {
                    if (isUsing)
                    {
                        ToggleState = false;
                    }
                    else
                    {
                        ToggleState = true;
                    }
                    if (ToggleState)
                    {
                        Timing.RunCoroutine(ToggleRoutine(target, payload));
                    }
                }
                //Non-toggle ability
                else if (!isUsing)
                {
                    isUsing = true;
                    StartAbility(target, payload);
                    Tick(target, payload);
                    EndAbility(target, payload);
                }
            }
        }

        protected IEnumerator<float> ToggleRoutine(Entity target, object payload)
        {
            isUsing = true;
            while (ToggleState)
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
            if (target != entity)
            {
                StartEffects();
                for (int i = 0; i < abilityEffects.Length; i++)
                {
                    abilityEffects[i].DoAction(target, entity, payload);
                }
                EndEffects();
            }            
        }

        public void SetEntity(Entity entity)
        {
            this.entity = entity;
        }

        public virtual void CancelAbility()
        {
            if (ToggleState)
            {
                UseAbility(null, null);
            }
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
        protected virtual void Tick(Entity target, object payload) 
        {
            foreach (Action action in tickActions)
            {
                action.DoAction(target, entity);
            }
        }
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
    }
}
