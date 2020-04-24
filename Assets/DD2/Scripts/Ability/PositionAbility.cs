using System.Collections.Generic;
using UnityEngine;
using MEC;
using DD2.Util;
using UnityEngine.AI;

namespace DD2.Abilities
{
    public class PositionAbility : Ability
    {
        [SerializeField] float radius;
        [SerializeField] string objectKey;
        Collider[] collisions;
        int maxCollisions = 0;

        protected override void Awake()
        {
            base.Awake();
            foreach(Hitbox hitbox in hitboxes)
            {
                if (hitbox.GetMaxCollisions() > maxCollisions)
                {
                    maxCollisions = hitbox.GetMaxCollisions();
                }
            }
            collisions = new Collider[maxCollisions];
        }

        protected override void StartAbility(Transform transform)
        {
            base.StartAbility(transform);
            Timing.RunCoroutine(AbilityRoutine(transform));
        }

        IEnumerator<float> AbilityRoutine(Transform transform)
        {
            List<Collider> hitColliders = new List<Collider>();
            Vector3 position = transform.position;
            foreach (Hitbox hitbox in hitboxes)
            {
                hitbox.hitboxObject.transform.position = position;
                hitbox.hitboxObject.SetActive(true);

                yield return Timing.WaitForSeconds(hitbox.GetDelay());
                CoroutineHandle hitboxHandle = Timing.RunCoroutine(HitboxRoutine(hitbox, hitColliders, position));
                yield return Timing.WaitForSeconds(hitbox.GetDuration());
                Timing.KillCoroutines(hitboxHandle);

                hitbox.hitboxObject.SetActive(false);
            }
            if (!isToggle)
            {
                EndAbility(transform);
            }
        }

        IEnumerator<float> HitboxRoutine(Hitbox hitbox, List<Collider> hitColliders, Vector3 position)
        {
            while(true)
            {
                hitColliders.Clear();
                int tempCount = hitbox.GetCollisionNonAlloc(position, layerMask, collisions);
                for (int i = 0; i < tempCount; i++)
                {
                    hitColliders.Add(collisions[i]);
                }
                
                if (multiTarget)
                {
                    foreach (Collider hitCollider in hitColliders)
                    {
                        Entity hitEntity = hitCollider.GetComponent<Entity>();
                        CreateProjectile(hitCollider.transform);
                        ApplyEffects(hitEntity, position);
                    }
                }
                else
                {
                    Collider collider = Util.Utilities.GetClosestToPoint(hitColliders, position);
                    if (collider)
                    {
                        Entity hitEntity = collider.GetComponent<Entity>();
                        CreateProjectile(collider.transform);
                        ApplyEffects(hitEntity, position);
                    }
                }
                yield return Timing.WaitForSeconds(hitboxTickRate);
            }
        }

        protected override void Tick(Transform transform)
        {
            Timing.RunCoroutine(AbilityRoutine(transform));
        }

        void CreateProjectile(Transform target)
        {
            Projectile projectile = ProjectilePool.Instance.GetObject(objectKey);
            if (projectile != null)
            {
                projectile.transform.position = GetFirePosition();
                projectile.target = target;
                projectile.gameObject.SetActive(true);
            }
        }
    }
}