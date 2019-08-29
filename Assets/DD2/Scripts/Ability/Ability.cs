using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MEC;
using NaughtyAttributes;

namespace DD2.Ability
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Abilities/Ability")]
    public class Ability : MonoBehaviour
    {
        [SerializeField] [ReorderableList] [Expandable] Effect[] abilityEffects;
        [SerializeField] float cooldown;
        bool onCooldown;
        CoroutineHandle cooldownRoutine;

        public void UseAbility()
        {
            if (onCooldown)
            {
                return;
            }

            cooldownRoutine = Timing.RunCoroutine(CooldownRoutine());
        }

        IEnumerator<float> CooldownRoutine()
        {
            yield return Timing.WaitForSeconds(cooldown);
            onCooldown = false;
        }
    }
}
