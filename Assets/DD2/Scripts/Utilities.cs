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

        public static Collider GetClosestToPoint(List<Collider> colliders, Vector3 position)
        {
            Collider closestCollider = null;
            float closestDistance = 0;

            for (int i = 0; i < colliders.Count; i++)
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

        public static bool IsInLayer(GameObject gameObject, LayerMask mask)
        {
            return mask == (mask | (1 << gameObject.layer));
        }

        public static void ClearArray(object[] array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = null;
            }
        }

        public static bool ArrayContains(object[] array, object obj)
        {
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] == obj)
                {
                    return true;
                }
            }
            return false;
        }

        public static void RemoveFromArray(object[] array, object obj)
        {
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] == obj)
                {
                    array[i] = null;
                    for (int j = i + 1; j < array.Length; j++)
                    {
                        if (array[j] == null)
                        {
                            break;
                        }
                        else
                        {
                            object temp = array[j - 1];
                            array[j - 1] = array[j];
                            array[j] = temp;
                        }
                    }
                }
            }
        }
    }
}
