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
        [SerializeField] GameObject hitboxObject;
        [SerializeField] float delay;
        [SerializeField] float duration;
        [SerializeField] float repeatDelay;
        [SerializeField] int maxCollisions = 100;
        List<Collider> repeatList = new List<Collider>();
        Collider[] results;
        int resultsCount;
        Collider[] returnResults;
        int returnResultsCount;
        Collider[] removeResults;
        int removeResultsCount;

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
            returnResults = new Collider[MaxCollisions];
            removeResults = new Collider[MaxCollisions];
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
            returnResultsCount = 0;

            if (hitboxShape == Shape.Sphere)
            {
                if (sphereCollider == null)
                {
                    Initialize();
                }
                
                resultsCount = Physics.OverlapSphereNonAlloc(position, sphereCollider.radius * HitboxObject.transform.lossyScale.x, results, layerMask);
            }

            if (resultsCount > 0)
            {
                for (int i = 0; i < resultsCount; i++)
                {
                    if (!repeatList.Contains(results[i]) && results[i] != null)
                    {
                        this.returnResults[returnResultsCount] = results[i];
                        returnResultsCount++;
                    }
                }
                if (returnResultsCount > 0)
                {
                    repeatList.AddRange(this.returnResults);
                    for (int i = 0; i < returnResultsCount; i++)
                    {
                        removeResults[removeResultsCount] = this.returnResults[i];
                        removeResultsCount++;
                    }
                    Timing.RunCoroutine(RemoveRoutine(removeResults, returnResultsCount));
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
            int newCount = 0;
            int index = 0;
            while (count + index < removeResultsCount && colliders[count + index] != null)
            {
                colliders[index] = colliders[count + index];
                colliders[count + index] = null;
                newCount++;
                index++;
            }
            for (int i = index; i < count; i++)
            {
                colliders[i] = null;
            }
            removeResultsCount = newCount;
        }
    }
}
