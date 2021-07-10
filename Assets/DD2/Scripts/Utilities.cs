using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DD2.Util
{
    public static class Utilities
    {
        public static Color ErrorColor { get { return Color.red; } }

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

        public static bool IsPositionInCone(Vector3 start, Vector3 direction, Vector3 end, float angle)
        {
            float desiredDot = Mathf.Cos(Mathf.Deg2Rad * angle);
            float dot = Vector3.Dot(direction, Direction(start, end));
            return dot > desiredDot;
        }

        public static bool IsPositionInCone(Vector2 start, Vector2 direction, Vector2 end, float angle)
        {
            float desiredDot = Mathf.Cos(Mathf.Deg2Rad * angle);
            float dot = Vector2.Dot(direction, Direction(start, end));
            return dot > desiredDot;
        }

        ///Direction starting from "start" and ending at "end"
        public static Vector3 Direction(Vector3 start, Vector3 end)
        {
            Vector3 heading = end - start;
            return heading.normalized;
        }

        ///Direction starting from "start" and ending at "end"
        public static Vector2 Direction(Vector2 start, Vector2 end)
        {
            Vector2 heading = end - start;
            return heading.normalized;
        }

        public static Vector3 CapsuleDirection(int direction)
        {
            switch (direction)
            {
                case 0:
                    return Vector3.right;
                case 1:
                    return Vector3.up;
                case 2:
                    return Vector3.forward;
                default:
                    return Vector3.zero;
            }
        }
    }
}
