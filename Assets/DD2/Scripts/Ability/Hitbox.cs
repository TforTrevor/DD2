using UnityEngine;
using MEC;

namespace DD2.Abilities
{
    [System.Serializable]
    public class Hitbox
    {
        enum Shape { Sphere, Box, Capsule };
        [SerializeField] Shape hitboxShape;
        [SerializeField] GameObject hitbox;
        [SerializeField] float delay;
        [SerializeField] float lingerTime;
        SphereCollider sphereCollider;
        BoxCollider boxCollider;
        CapsuleCollider capsuleCollider;

        public void Initialize()
        {
            if (hitboxShape == Shape.Sphere)
            {
                sphereCollider = hitbox.GetComponent<SphereCollider>();
            }
            else if (hitboxShape == Shape.Box)
            {
                boxCollider = hitbox.GetComponent<BoxCollider>();
            }
            else if (hitboxShape == Shape.Capsule)
            {
                capsuleCollider = hitbox.GetComponent<CapsuleCollider>();
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

                hitbox.transform.position = position;
                hitbox.SetActive(true);
                Timing.CallDelayed(1f, () =>
                {
                    hitbox.SetActive(false);
                });
                return Physics.OverlapSphere(position, sphereCollider.radius * hitbox.transform.lossyScale.x, layerMask);
            }

            return null;
        }

        public float GetDelay()
        {
            return delay;
        }
    }
}
