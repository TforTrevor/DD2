using UnityEngine;
using MEC;

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

                return Physics.OverlapSphere(position, sphereCollider.radius * hitboxObject.transform.lossyScale.x, layerMask);
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
