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

        bool ShowDamage { get => statusEffect == StatusEffect.Burn; }

        public override void DoAction(Entity target, Entity caller, object payload)
        {
            if (!target.StatusEffects.HasFlag(statusEffect))
            {
                switch (statusEffect)
                {
                    case StatusEffect.Stun:
                        Timing.RunCoroutine(GenericRoutine(target, caller, payload));
                        break;
                    case StatusEffect.Freeze:
                        Timing.RunCoroutine(GenericRoutine(target, caller, payload));
                        break;
                    case StatusEffect.Burn:
                        Timing.RunCoroutine(BurnRoutine(target, caller, payload));
                        break;
                    case StatusEffect.Slow:
                        Timing.RunCoroutine(SlowRoutine(target, caller, payload));
                        break;
                    case StatusEffect.Speed:
                        Timing.RunCoroutine(SpeedRoutine(target, caller, payload));
                        break;
                    case StatusEffect.Root:
                        Timing.RunCoroutine(RootRoutine(target, caller, payload));
                        break;
                    case StatusEffect.Fear:
                        Timing.RunCoroutine(GenericRoutine(target, caller, payload));
                        break;
                    case StatusEffect.Confuse:
                        Timing.RunCoroutine(GenericRoutine(target, caller, payload));
                        break;
                    default:
                        break;
                }
            }
        }

        IEnumerator<float> GenericRoutine(Entity target, Entity caller, object payload)
        {
            target.AddStatus(statusEffect);

            yield return Timing.WaitForSeconds(duration);

            target.RemoveStatus(statusEffect);
        }

        IEnumerator<float> BurnRoutine(Entity target, Entity caller, object payload)
        {
            target.AddStatus(statusEffect);
            float currentDuration = 0;
            while (currentDuration < duration)
            {
                yield return Timing.WaitForSeconds(0.5f);
                damage.DoAction(target, caller, payload);
                currentDuration += 0.5f;                
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

        IEnumerator<float> SpeedRoutine(Entity target, Entity caller, object payload)
        {
            target.AddStatus(statusEffect);
            float value = Mathf.Max(100, strength) / 100;
            target.SetMoveSpeed(target.Stats.MoveSpeed * value);

            yield return Timing.WaitForSeconds(duration);

            target.SetMoveSpeed(target.Stats.MoveSpeed / value);
            target.RemoveStatus(statusEffect);
        }

        IEnumerator<float> RootRoutine(Entity target, Entity caller, object payload)
        {
            target.AddStatus(statusEffect);

            float previousSpeed = target.Stats.MoveSpeed;
            target.SetMoveSpeed(0);

            yield return Timing.WaitForSeconds(duration);

            target.SetMoveSpeed(previousSpeed);
            target.RemoveStatus(statusEffect);
        }
    }
}