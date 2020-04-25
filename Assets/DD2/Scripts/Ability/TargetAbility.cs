using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MEC;
using DD2.AI;

namespace DD2.Abilities
{
    public class TargetAbility : Ability
    {
        [SerializeField] string projectileKey;

        protected override void Tick(Entity target, object payload)
        {
            base.Tick(target, payload);
            Projectile projectile = ProjectilePool.Instance.GetObject(projectileKey);
            if (projectile != null)
            {
                projectile.transform.position = entity.GetFirePosition();
                projectile.Initialize(target, projectileKey);
            }            
            ApplyEffects(target, payload);
        }
    }
}