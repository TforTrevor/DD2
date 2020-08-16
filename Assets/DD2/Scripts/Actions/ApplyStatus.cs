using MEC;
using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DD2.Actions
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Actions/Apply Status")]
    public class ApplyStatus : Action
    {
        [SerializeField] StatusEffect statusEffect;
        [SerializeField] float strength;
        [SerializeField] float duration;
        [ShowIf("ShowDamage")] [SerializeField] Damage damage;

        bool ShowDamage { get => statusEffect == StatusEffect.Burned; }

        public override void DoAction(Entity target, Entity caller, object payload)
        {
            if (!target.StatusEffects.HasFlag(statusEffect))
            {
                switch (statusEffect)
                {
                    case StatusEffect.Stunned:
                        Stunned(target, caller, payload);
                        break;
                    case StatusEffect.Frozen:
                        Frozen(target, caller, payload);
                        break;
                    case StatusEffect.Burned:
                        Timing.RunCoroutine(BurnRoutine(target, caller, payload));
                        break;
                    case StatusEffect.Slowed:
                        Timing.RunCoroutine(SlowRoutine(target, caller, payload));
                        break;
                    default:
                        break;
                }
            }
        }

        void Stunned(Entity target, Entity caller, object payload)
        {

        }

        void Frozen(Entity target, Entity caller, object payload)
        {

        }

        IEnumerator<float> BurnRoutine(Entity target, Entity caller, object payload)
        {
            target.AddStatus(statusEffect);
            float currentDuration = 0;
            while (currentDuration < duration)
            {
                damage.DoAction(target, caller, payload);
                currentDuration += 0.5f;
                yield return Timing.WaitForSeconds(0.5f);
            }
            target.RemoveStatus(statusEffect);
        }

        IEnumerator<float> SlowRoutine(Entity target, Entity caller, object payload)
        {
            target.AddStatus(statusEffect);
            float value = Mathf.Max(100, strength) / 100;
            target.SetMoveSpeed(target.Stats.MoveSpeed / value);

            yield return Timing.WaitForSeconds(duration);

            target.SetMoveSpeed(target.Stats.MoveSpeed * value);
            target.RemoveStatus(statusEffect);
        }
    }
}