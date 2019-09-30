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
        ComponentPool<Projectile> objectPool;

        void Awake()
        {
            objectPool = GetComponent<ComponentPool<Projectile>>();
        }

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
                    }
                }
                else
                {
                    Collider collider = Utilities.GetClosestToPoint(hitColliders, position);
                    if (collider)
                    {
                        CreateProjectile(collider.transform);
                        ApplyEffects(collider.transform);
                        Status status = collider.transform.GetComponent<Status>();
                        status.Ragdoll();
                        status.AddForce(Vector3.up * 10, ForceMode.Impulse);
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

        protected override void Tick(Transform transform)
        {
            Timing.RunCoroutine(Test(transform));
        }

        void CreateProjectile(Transform target)
        {
            if (objectPool != null)
            {
                Projectile projectile = objectPool.GetObject(objectKey);
                if (projectile != null)
                {
                    projectile.transform.position = GetFirePosition();
                    projectile.target = target;
                    projectile.objectPool = objectPool;
                    projectile.gameObject.SetActive(true);
                }
            }
        }
    }
}