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
            Timing.RunCoroutine(Test(transform));
        }

        IEnumerator<float> Test(Transform transform)
        {
            List<Collider> hitColliders = new List<Collider>();
            Vector3 position = transform.position;
            foreach (Hitbox hitbox in hitboxes)
            {
                yield return Timing.WaitForSeconds(hitbox.GetDelay());

                hitbox.hitboxObject.transform.position = position;
                hitbox.hitboxObject.SetActive(true);

                hitColliders.Clear();
                hitColliders.AddRange(hitbox.GetCollision(position, layerMask));
                if (multiTarget)
                {
                    foreach (Collider hitCollider in hitColliders)
                    {
                        CreateProjectile(hitCollider.transform);
                        ApplyEffects(hitCollider.transform);
                        Knockback(hitCollider.transform, Vector3.up * 10, ForceMode.Impulse);
                    }
                }
                else
                {
                    Collider collider = Utilities.GetClosestToPoint(hitColliders, position);
                    if (collider)
                    {
                        CreateProjectile(collider.transform);
                        ApplyEffects(collider.transform);
                        Knockback(collider.transform, Vector3.up * 10, ForceMode.Impulse);
                    }
                }

                yield return Timing.WaitForSeconds(hitbox.GetDuration());
                hitbox.hitboxObject.SetActive(false);
            }
            if (!isToggle)
            {
                EndAbility(transform);
            }
        }

        void Knockback(Transform entity, Vector3 force, ForceMode forceMode)
        {
            Status status = entity.GetComponent<Status>();
            status.AddForce(force, forceMode);
        }

        protected override void Tick(Transform transform)
        {
            Timing.RunCoroutine(Test(transform));
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