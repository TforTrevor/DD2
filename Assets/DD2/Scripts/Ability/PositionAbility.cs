using System.Collections.Generic;
using UnityEngine;
using MEC;
using DD2.Util;
using UnityEngine.AI;

namespace DD2.Abilities
{
    public class PositionAbility : HitboxAbility
    {
        [SerializeField] float radius;
        [SerializeField] Projectile projectile;
        Collider[] collisions;
        int maxCollisions = 0;

        protected override void Awake()
        {
            base.Awake();
            foreach(Hitbox hitbox in hitboxes)
            {
                if (hitbox.MaxCollisions > maxCollisions)
                {
                    maxCollisions = hitbox.MaxCollisions;
                }
            }
            collisions = new Collider[maxCollisions];
        }

        protected override void StartAbility(Entity target, object payload)
        {
            base.StartAbility(target, payload);
            Timing.RunCoroutine(AbilityRoutine(target, payload));
        }

        IEnumerator<float> AbilityRoutine(Entity target, object payload)
        {
            List<Collider> hitColliders = new List<Collider>();
            Vector3 position = transform.position;
            foreach (Hitbox hitbox in hitboxes)
            {
                //hitbox.HitboxObject.transform.position = position;
                //hitbox.hitboxObject.SetActive(true);

                yield return Timing.WaitForSeconds(hitbox.Delay);
                CoroutineHandle hitboxHandle = Timing.RunCoroutine(HitboxRoutine(hitbox, hitColliders, position));
                yield return Timing.WaitForSeconds(hitbox.Duration);
                Timing.KillCoroutines(hitboxHandle);

                //hitbox.hitboxObject.SetActive(false);
            }
            if (!isToggle)
            {
                EndAbility(target, payload);
            }
        }

        IEnumerator<float> HitboxRoutine(Hitbox hitbox, List<Collider> hitColliders, Vector3 position)
        {
            while(true)
            {
                hitColliders.Clear();
                int tempCount = hitbox.GetCollisionNonAlloc(position, LayerMask, collisions);
                for (int i = 0; i < tempCount; i++)
                {
                    hitColliders.Add(collisions[i]);
                }
                
                if (multiTarget)
                {
                    foreach (Collider hitCollider in hitColliders)
                    {
                        Entity hitEntity = hitCollider.GetComponent<Entity>();
                        CreateProjectile(hitEntity);
                        ApplyEffects(hitEntity, position);
                    }
                }
                else
                {
                    Collider collider = Util.Utilities.GetClosestToPoint(hitColliders, position);
                    if (collider != null)
                    {
                        Entity hitEntity = collider.GetComponent<Entity>();
                        CreateProjectile(hitEntity);
                        ApplyEffects(hitEntity, position);
                    }
                }
                yield return Timing.WaitForSeconds(hitboxTickRate);
            }
        }

        protected override void Tick(Entity target, object payload)
        {
            Timing.RunCoroutine(AbilityRoutine(target, payload));
        }

        void CreateProjectile(Entity target)
        {
            Projectile projectile = ProjectilePool.Instance.GetObject(this.projectile.PoolKey);
            if (projectile != null)
            {
                projectile.transform.position = entity.FireTransform.position;
                projectile.Initialize(target.transform, null);
            }
        }
    }
}