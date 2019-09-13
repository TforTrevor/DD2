﻿using System.Collections;
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
            Collider[] hitColliders = Physics.OverlapSphere(position, radius, layerMask);

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
                CreateProjectile(collider.transform);
                ApplyEffects(collider.transform);
            }
        }

        protected override void ContinuousTick(Vector3 position)
        {
            StartAbility(position);
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