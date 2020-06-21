using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MEC;
using DD2.AI;

namespace DD2.Abilities
{
    public class TargetAbility : Ability
    {
        [SerializeField] Projectile projectile;

        protected override void Tick(Entity target, object payload)
        {
            base.Tick(target, payload);
            Projectile projectile = ProjectilePool.Instance.GetObject(this.projectile.PoolKey);
            if (projectile != null)
            {
                projectile.transform.position = entity.GetFirePosition();
                projectile.Initialize(target.transform, null);
            }            
            ApplyEffects(target, payload);
        }
    }
}