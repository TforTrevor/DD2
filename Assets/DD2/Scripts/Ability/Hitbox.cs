using UnityEngine;
using MEC;
using System.Collections.Generic;

namespace DD2.Abilities
{
    [System.Serializable]
    public class Hitbox
    {
        enum Shape { Sphere, Box, Capsule };
        [SerializeField] Shape hitboxShape;
        public GameObject hitboxObject;
        [SerializeField] float delay;
        [SerializeField] float duration;
        [SerializeField] float repeatDelay;
        [SerializeField] int maxCollisions = 100;
        Collider[] repeatList;
        int repeatListCount;
        Collider[] hitColliders;
        int hitCollidersCount;
        Collider[] results;
        Collider[] returnResults;
        int returnResultsCount;

        SphereCollider sphereCollider;
        BoxCollider boxCollider;
        CapsuleCollider capsuleCollider;

        Hitbox()
        {
            results = new Collider[maxCollisions];
            hitColliders = new Collider[maxCollisions];
            returnResults = new Collider[maxCollisions];
            repeatList = new Collider[maxCollisions];
        }

        void Initialize()
        {
            if (hitboxShape == Shape.Sphere)
            {
                sphereCollider = hitboxObject.GetComponent<SphereCollider>();
            }
            else if (hitboxShape == Shape.Box)
            {
                boxCollider = hitboxObject.GetComponent<BoxCollider>();
            }
            else if (hitboxShape == Shape.Capsule)
            {
                capsuleCollider = hitboxObject.GetComponent<CapsuleCollider>();
            }
        }

        public int GetCollisionNonAlloc(Vector3 position, LayerMask layerMask, Collider[] returnResults)
        {
            Util.Utilities.ClearArray(this.returnResults);
            Util.Utilities.ClearArray(results);
            Util.Utilities.ClearArray(hitColliders);
            hitCollidersCount = 0;
            returnResultsCount = 0;

            if (hitboxShape == Shape.Sphere)
            {
                if (sphereCollider == null)
                {
                    Initialize();
                }
                
                Physics.OverlapSphereNonAlloc(position, sphereCollider.radius * hitboxObject.transform.lossyScale.x, results, layerMask);
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
                    if (!Util.Utilities.ArrayContains(repeatList, hitColliders[i]) && hitColliders[i] != null)
                    {
                        this.returnResults[returnResultsCount] = hitColliders[i];
                        returnResultsCount++;
                    }
                }
                if (returnResultsCount > 0)
                {
                    for (int i = 0; i < returnResultsCount; i++)
                    {
                        repeatList[repeatListCount] = this.returnResults[i];
                        Timing.RunCoroutine(RemoveRoutine(this.returnResults, returnResultsCount));
                    }
                }
            }

            Util.Utilities.ClearArray(returnResults);
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
                Util.Utilities.RemoveFromArray(repeatList, colliders[i]);
                count--;
            }
        }

        public float GetDelay()
        {
            return delay;
        }

        public float GetDuration()
        {
            return duration;
        }

        public int GetMaxCollisions()
        {
            return maxCollisions;
        }
    }
}
