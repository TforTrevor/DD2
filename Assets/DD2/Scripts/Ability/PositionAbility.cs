using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MEC;
using RoboRyanTron.SearchableEnum;
using DD2.Util;

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

        protected override void StartAbility(Vector3 position)
        {
            base.StartAbility(position);
            Timing.RunCoroutine(Test(position));
        }

        IEnumerator<float> Test(Vector3 position)
        {
            List<Collider> hitColliders = new List<Collider>();
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
                    }
                }

                yield return Timing.WaitForSeconds(hitbox.GetDuration());
                hitbox.hitboxObject.SetActive(false);
            }
            if (!isToggle)
                EndAbility(position);
        }

        protected override void Tick(Vector3 position)
        {
            Timing.RunCoroutine(Test(position));
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