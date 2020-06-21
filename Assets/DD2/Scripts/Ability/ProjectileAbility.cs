using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DD2.Abilities
{
    public class ProjectileAbility : Ability
    {
        [SerializeField] Projectile projectile;
        [SerializeField] HitboxAbility hitbox;

        protected override void Tick(Entity entity, object payload)
        {
            Projectile projectile = ProjectilePool.Instance.GetObject(this.projectile.PoolKey);
            if (projectile != null)
            {
                projectile.transform.position = entity.GetFirePosition();
                projectile.Initialize(entity.GetForward(), () =>
                {
                    hitbox.UseAbility(projectile.Entity, payload);
                });
                ApplyEffects(entity, payload);
            }
        }
    }
}