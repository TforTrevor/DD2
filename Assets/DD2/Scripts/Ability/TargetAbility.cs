using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MEC;
using DD2.AI;

namespace DD2.Abilities
{
    public class TargetAbility : Ability
    {
        [SerializeField] string objectKey;
        ComponentPool<Projectile> objectPool;

        void Awake()
        {
            objectPool = GetComponent<ComponentPool<Projectile>>();
        }

        public override void UseAbility(AIStatus status)
        {
            if (onCooldown)
            {
                return;
            }

            if (objectPool != null)
            {
                Projectile projectile = objectPool.GetObject(objectKey);
                if (projectile != null)
                {
                    projectile.transform.position = status.GetFirePosition();
                    projectile.target = status.target;
                    projectile.objectPool = objectPool;
                    projectile.gameObject.SetActive(true);
                }
            }
            
            for (int i = 0; i < abilityEffects.Length; i++)
            {
                abilityEffects[i].ApplyEffect(this, status.target);
            }

            cooldownRoutine = Timing.RunCoroutine(CooldownRoutine());
        }
    }
}