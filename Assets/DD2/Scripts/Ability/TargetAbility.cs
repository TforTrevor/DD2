using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MEC;

namespace DD2.Abilities
{
    public class TargetAbility : Ability
    {
        [SerializeField] string objectKey;
        ObjectPool objectPool;

        void Awake()
        {
            objectPool = GetComponent<ObjectPool>();
        }

        public override void UseAbility(Transform target)
        {
            if (onCooldown)
            {
                return;
            }

            GameObject vfx = objectPool.GetObject(objectKey);
            if (vfx != null)
            {
                vfx.transform.position = transform.position;
                Projectile projectile = vfx.GetComponent<Projectile>();
                projectile.target = target;
                projectile.objectPool = objectPool;
                vfx.SetActive(true);
            }
            
            for (int i = 0; i < abilityEffects.Length; i++)
            {
                abilityEffects[i].ApplyEffect(this, target);
            }

            cooldownRoutine = Timing.RunCoroutine(CooldownRoutine());
        }
    }
}