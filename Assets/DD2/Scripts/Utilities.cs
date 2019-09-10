using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DD2.Util
{
    public static class Utilities
    {
        public static Collider GetClosestToPoint(Collider[] colliders, Vector3 position)
        {
            Collider closestCollider = null;
            float closestDistance = 0;

            for (int i = 0; i < colliders.Length; i++)
            {
                if (closestCollider == null)
                {
                    closestCollider = colliders[i];
                }
                else
                {
                    float distance = Vector3.Distance(colliders[i].transform.position, position);
                    if (distance < closestDistance)
                    {
                        closestCollider = colliders[i];
                        closestDistance = distance;
                    }
                }
            }

            return closestCollider;
        }

        public static Transform GetClosestToPoint(Transform[] transforms, Vector3 position)
        {
            Transform closestTransform = null;
            float closestDistance = 0;

            for (int i = 0; i < transforms.Length; i++)
            {
                if (closestTransform == null)
                {
                    closestTransform = transforms[i];
                }
                else
                {
                    float distance = Vector3.Distance(transforms[i].transform.position, position);
                    if (distance < closestDistance)
                    {
                        closestTransform = transforms[i];
                        closestDistance = distance;
                    }
                }
            }

            return closestTransform;
        }
    }
}
