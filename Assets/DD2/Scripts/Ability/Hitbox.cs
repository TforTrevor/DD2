using UnityEngine;
using MEC;
using System.Collections.Generic;

namespace DD2.Abilities
{
    [RequireComponent(typeof(CapsuleCollider))]
    public class Hitbox : MonoBehaviour
    {
        [SerializeField] Transform parent;
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

        CapsuleCollider capsuleCollider;
        CoroutineHandle repeatRoutine;

        public int MaxCollisions { get => maxCollisions; private set => maxCollisions = value; }
        public float Delay { get => delay; private set => delay = value; }
        public float Duration { get => duration; private set => duration = value; }

        void Awake()
        {
            capsuleCollider = GetComponent<CapsuleCollider>();
            capsuleCollider.enabled = false;
            results = new Collider[MaxCollisions];
            returnResults = new Collider[MaxCollisions];
            removeResults = new Collider[MaxCollisions];

            if (parent != null)
            {
                transform.SetParent(parent);
            }
        }

        public int GetCollisionNonAlloc(Vector3 position, LayerMask layerMask, Collider[] returnResults)
        {
            Util.Utilities.ClearArray(this.returnResults, returnResultsCount);
            Util.Utilities.ClearArray(results, resultsCount);
            returnResultsCount = 0;

            Vector3 pos;
            if (parent == null)
            {
                pos = position;
            }
            else
            {
                pos = transform.TransformPoint(capsuleCollider.center);
            }            
            Vector3 direction = transform.TransformDirection(Util.Utilities.CapsuleDirection(capsuleCollider.direction));
            Vector3 start = pos - (direction * capsuleCollider.height / 2 * transform.lossyScale.x);
            Vector3 end = pos + (direction * capsuleCollider.height / 2 * transform.lossyScale.x);
            resultsCount = Physics.OverlapCapsuleNonAlloc(start, end, capsuleCollider.radius * transform.lossyScale.x, results, layerMask);

            //resultsCount = Physics.OverlapSphereNonAlloc(position, capsuleCollider.radius, results, layerMask);

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
                    repeatRoutine = Timing.RunCoroutine(RemoveRoutine(removeResults, returnResultsCount));
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

        public void ClearRepeat()
        {
            Timing.KillCoroutines(repeatRoutine);
            repeatList.Clear();
        }
    }
}
