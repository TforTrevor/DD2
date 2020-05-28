﻿using UnityEngine;
using MEC;
using System.Collections.Generic;

namespace DD2.Abilities
{
    [System.Serializable]
    public class Hitbox
    {
        enum Shape { Sphere, Box, Capsule };
        [SerializeField] Shape hitboxShape;
        [SerializeField] GameObject hitboxObject;
        [SerializeField] float delay;
        [SerializeField] float duration;
        [SerializeField] float repeatDelay;
        [SerializeField] int maxCollisions = 100;
        List<Collider> repeatList = new List<Collider>();
        Collider[] hitColliders;
        int hitCollidersCount;
        Collider[] results;
        int resultsCount;
        Collider[] returnResults;
        int returnResultsCount;

        SphereCollider sphereCollider;
        BoxCollider boxCollider;
        CapsuleCollider capsuleCollider;

        public int MaxCollisions { get => maxCollisions; private set => maxCollisions = value; }
        public GameObject HitboxObject { get => hitboxObject; private set => hitboxObject = value; }
        public float Delay { get => delay; private set => delay = value; }
        public float Duration { get => duration; private set => duration = value; }

        Hitbox()
        {
            results = new Collider[MaxCollisions];
            hitColliders = new Collider[MaxCollisions];
            returnResults = new Collider[MaxCollisions];
            //repeatList.Capacity = maxCollisions;
        }

        void Initialize()
        {
            if (hitboxShape == Shape.Sphere)
            {
                sphereCollider = HitboxObject.GetComponent<SphereCollider>();
            }
            else if (hitboxShape == Shape.Box)
            {
                boxCollider = HitboxObject.GetComponent<BoxCollider>();
            }
            else if (hitboxShape == Shape.Capsule)
            {
                capsuleCollider = HitboxObject.GetComponent<CapsuleCollider>();
            }
        }

        public int GetCollisionNonAlloc(Vector3 position, LayerMask layerMask, Collider[] returnResults)
        {
            Util.Utilities.ClearArray(this.returnResults, returnResultsCount);
            Util.Utilities.ClearArray(results, resultsCount);
            Util.Utilities.ClearArray(hitColliders, hitCollidersCount);
            hitCollidersCount = 0;
            returnResultsCount = 0;

            if (hitboxShape == Shape.Sphere)
            {
                if (sphereCollider == null)
                {
                    Initialize();
                }
                
                resultsCount = Physics.OverlapSphereNonAlloc(position, sphereCollider.radius * HitboxObject.transform.lossyScale.x, results, layerMask);
                for (int i = 0; i < results.Length; i++)
                {
                    if (results[i] != null)
                    {
                        hitColliders[hitCollidersCount] = results[i];
                        hitCollidersCount++;
                    }
                }
                //hitColliders.AddRange(results);
                //hitColliders.AddRange(Physics.OverlapSphere(position, sphereCollider.radius * hitboxObject.transform.lossyScale.x, layerMask));
            }

            if (hitCollidersCount > 0)
            {
                for (int i = 0; i < hitCollidersCount; i++)
                {
                    if (!repeatList.Contains(hitColliders[i]) && hitColliders[i] != null)
                    {
                        this.returnResults[returnResultsCount] = hitColliders[i];
                        returnResultsCount++;
                    }
                }
                if (returnResultsCount > 0)
                {
                    for (int i = 0; i < returnResultsCount; i++)
                    {
                        repeatList.Add(this.returnResults[i]);
                        Timing.RunCoroutine(RemoveRoutine(this.returnResults, returnResultsCount));
                    }
                }
            }

            Util.Utilities.ClearArray(returnResults, returnResultsCount);
            int tempCount = 0;
            for (int i = 0; i < returnResultsCount; i++)
            {
                if (this.returnResults[i] != null)
                {
                    returnResults[tempCount] = this.returnResults[i];
                    tempCount++;
                }
            }

            return tempCount;
        }

        IEnumerator<float> RemoveRoutine(Collider[] colliders, int count)
        {
            yield return Timing.WaitForSeconds(repeatDelay);
            for (int i = 0; i < count; i++)
            {
                repeatList.Remove(colliders[i]);
            }
        }
    }
}
