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

        public void Initialize()
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

        public Collider[] GetCollision(Vector3 position, LayerMask layerMask)
        {
            if (hitboxShape == Shape.Sphere)
            {
                if (sphereCollider == null)
                {
                    Initialize();
                }

                List<Collider> hitColliders = new List<Collider>(Physics.OverlapSphere(position, sphereCollider.radius * hitboxObject.transform.lossyScale.x, layerMask));
                for (int i = 0; i < hitColliders.Count; i++)
                {
                    if (repeatList.Contains(hitColliders[i]))
                    {
                        hitColliders.RemoveAt(i);
                    }
                }
                repeatList.AddRange(hitColliders);
                Timing.CallDelayed(repeatDelay, () =>
                {
                    foreach (Collider hitCollider in hitColliders)
                    {
                        repeatList.Remove(hitCollider);
                    }
                });

                return hitColliders.ToArray();
            }

            return null;
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
