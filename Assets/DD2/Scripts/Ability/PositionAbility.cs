using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MEC;
using RoboRyanTron.SearchableEnum;
using DD2.Util;
using UnityEngine.AI;

namespace DD2.Abilities
{
    public class PositionAbility : Ability
    {
        [SerializeField] float radius;
        [SerializeField] string objectKey;

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
                hitColliders.AddRange(hitbox.GetCollision(position, layerMask));
                
                if (multiTarget)
                {
                    foreach (Collider hitCollider in hitColliders)
                    {
                        CreateProjectile(hitCollider.transform);
                        ApplyEffects(hitCollider.transform);
                    }
                }
                else
                {
                    Collider collider = Utilities.GetClosestToPoint(hitColliders, position);
                    if (collider)
                    {
                        CreateProjectile(collider.transform);
                        ApplyEffects(collider.transform);
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