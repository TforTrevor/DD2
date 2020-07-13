using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DD2.Abilities
{
    public class ProjectileAbility : Ability
    {
        [SerializeField] Projectile projectile;
        [SerializeField] Ability onHit;
        [SerializeField] FireType fireType;

        protected override void Tick(Entity entity, object payload)
        {
            Projectile projectile = ProjectilePool.Instance.GetObject(this.projectile.PoolKey);
            if (projectile != null)
            {
                Vector3 direction;
                if (fireType == FireType.freeFire)
                {
                    direction = this.entity.FireTransform.forward;
                }
                else
                {
                    direction = Util.Utilities.Direction(this.entity.FireTransform.position, entity.EyePosition);
                }
                projectile.transform.position = this.entity.FireTransform.position;
                projectile.Initialize(direction, (Entity hit) =>
                {
                    if (hit != null)
                    {
                        onHit?.UseAbility(hit, payload);
                    }
                    else
                    {
                        onHit?.UseAbility(projectile.Entity, payload);
                    }
                });
                ApplyEffects(entity, payload);
            }
        }

        enum FireType { freeFire, target }
    }
}