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
        [SerializeField] [MinValue(0)] [BoxGroup("Cooldown")]
        protected float cooldown;
        [SerializeField] [BoxGroup("Cooldown")]
        protected bool beforeEnd;
        [ReadOnly] [SerializeField] [BoxGroup("Cooldown")]
        protected bool onCooldown;
        [ReadOnly] [SerializeField] [BoxGroup("Cooldown")]
        protected bool isUsing;
        [SerializeField] [BoxGroup("Toggle")]
        protected bool isToggle;
        [SerializeField] [BoxGroup("Toggle")]
        protected float tickRate;
        [SerializeField] protected LayerMask layerMask;
        [SerializeField] protected bool multiTarget;

        [SerializeField] [ReorderableList] protected Hitbox[] hitboxes;

        public virtual void UseAbility(Vector3 position)
        {
            if (onCooldown)
            {
                return;
            }
            //Toggle ability
            if (isToggle)
            {
                isUsing = !isUsing;
                if (isUsing)
                {
                    Timing.RunCoroutine(ToggleRoutine(position));
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
                StartAbility(position);
            }
        }

        protected virtual void StartAbility(Vector3 position) { if (beforeEnd) { Timing.RunCoroutine(CooldownRoutine()); } }
        protected virtual void EndAbility(Vector3 position) { isUsing = false; if (!beforeEnd) { Timing.RunCoroutine(CooldownRoutine()); } }
        protected virtual void Tick(Vector3 position) { }
        protected virtual void StartCooldown() { }
        protected virtual void EndCooldown() { }
        protected virtual void StartEffects() { }
        protected virtual void EndEffects() { }

        protected IEnumerator<float> ToggleRoutine(Vector3 position)
        {
            while (isUsing)
            {
                Tick(position);

                yield return Timing.WaitForSeconds(tickRate);
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
            return status.GetFirePosition();
        }

        public void SetStatus(Status status)
        {
            this.status = status;
        }
    }
}
