using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DD2.Util
{
    public static class Utilities
    {
        static Dictionary<Vector3, float> viewDirections = new Dictionary<Vector3, float>();

        static Utilities()
        {
            float numViewDirections = 200;
            float goldenRatio = (1 + Mathf.Sqrt(5)) / 2;
            float angleIncrement = Mathf.PI * 2 * goldenRatio;

            for (int i = 0; i < numViewDirections; i++)
            {
                float t = (float)i / numViewDirections;
                float inclination = Mathf.Acos(1 - 2 * t);
                float azimuth = angleIncrement * i;

                float x = Mathf.Sin(inclination) * Mathf.Cos(azimuth);
                float y = Mathf.Sin(inclination) * Mathf.Sin(azimuth);
                float z = Mathf.Cos(inclination);
                viewDirections.Add(new Vector3(x, y, z), Vector3.Dot(Vector3.forward, new Vector3(x, y, z)));
            }
        }

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

        public static void ClearArray(object[] array, int count)
        {
            for (int i = 0; i < count; i++)
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

        public static float Remap(float value, float from1, float to1, float from2, float to2)
        {
            return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
        }

        public static float GetLargestDimension(Vector3 vector)
        {
            float largest = 0;
            if (vector.x > largest)
            {
                largest = vector.x;
            }
            if (vector.y > largest)
            {
                largest = vector.y;
            }
            if (vector.z > largest)
            {
                largest = vector.z;
            }
            return largest;
        }

        public static bool IsColliderInCone(Collider collider, Transform transform, float angle, float range, LayerMask layerMask)
        {
            Vector3 center = collider.bounds.center;
            float desiredDot = Mathf.Cos(Mathf.Deg2Rad * angle / 2f);
            float dot = Vector3.Dot(transform.forward, Vector3.Normalize(center - transform.position));

            if (dot > desiredDot)
            {
                return true;
            }
            else
            {
                foreach (KeyValuePair<Vector3, float> entry in viewDirections)
                {
                    if (entry.Value > desiredDot)
                    {
                        Vector3 dir = transform.TransformDirection(entry.Key);
                        Debug.DrawRay(transform.position, dir * range, Color.red, 10f);
                        if (Physics.Raycast(transform.position, dir, range, layerMask))
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        public static Vector3 DirectionTowards(Vector3 start, Vector3 end)
        {
            Vector3 heading = end - start;
            return heading.normalized;
        }
    }
}
