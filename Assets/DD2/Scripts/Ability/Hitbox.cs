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
        List<Collider> repeatList = new List<Collider>();

        SphereCollider sphereCollider;
        BoxCollider boxCollider;
        CapsuleCollider capsuleCollider;

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

        public List<Collider> GetCollision(Vector3 position, LayerMask layerMask)
        {
            List<Collider> hitColliders = new List<Collider>();
            List<Collider> colliders = new List<Collider>();
            if (hitboxShape == Shape.Sphere)
            {
                if (sphereCollider == null)
                {
                    Initialize();
                }
                hitColliders.AddRange(Physics.OverlapSphere(position, sphereCollider.radius * hitboxObject.transform.lossyScale.x, layerMask));
            }

            if (hitColliders.Count > 0)
            {
                foreach (Collider collider in hitColliders)
                {
                    if (!repeatList.Contains(collider))
                    {
                        colliders.Add(collider);
                    }
                }
                if (colliders.Count > 0)
                {
                    repeatList.AddRange(colliders);
                    Timing.RunCoroutine(RemoveRoutine(colliders));
                }
            }

            return colliders;
        }

        IEnumerator<float> RemoveRoutine(List<Collider> colliders)
        {
            yield return Timing.WaitForSeconds(repeatDelay);
            foreach(Collider collider in colliders)
            {
                repeatList.Remove(collider);
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
    }
}
