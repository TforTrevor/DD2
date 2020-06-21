using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MEC;

namespace DD2
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] string poolKey;
        [SerializeField] float speed;
        [SerializeField] float endDistance;
        [SerializeField] float radius;
        [SerializeField] Entity entity;
        [SerializeField] LayerMask layerMask;

        public string PoolKey { get => poolKey; private set => poolKey = value; }
        public Entity Entity { get => entity; private set => entity = value; }

        public void Initialize(Transform target, System.Action callback)
        {
            gameObject.SetActive(true);
            Timing.RunCoroutine(MoveRoutine(target, callback));
        }

        public void Initialize(Vector3 direction, System.Action callback)
        {
            gameObject.SetActive(true);
            Timing.RunCoroutine(MoveRoutine(direction, callback));
        }

        IEnumerator<float> MoveRoutine(Transform target, System.Action callback)
        {
            while (Vector3.Distance(transform.position, target.transform.position) > endDistance)
            {
                Vector3 direction = Vector3.Normalize(target.transform.position - transform.position);
                transform.position += direction * speed * Time.deltaTime;
                transform.LookAt(target.transform);

                yield return Timing.WaitForOneFrame;
            }

            ProjectilePool.Instance.ReturnObject(PoolKey, this);
        }

        IEnumerator<float> MoveRoutine(Vector3 direction, System.Action callback)
        {
            float distanceTraveled = 0;

            while (distanceTraveled < endDistance)
            {
                RaycastHit hit;
                if (Physics.SphereCast(transform.position, radius, direction, out hit, speed * Time.deltaTime, layerMask))
                {
                    transform.position += direction * speed * Time.deltaTime;
                    transform.forward = direction;
                    callback?.Invoke();
                    break;
                }

                transform.position += direction * speed * Time.deltaTime;
                distanceTraveled += speed * Time.deltaTime;
                transform.forward = direction;

                yield return Timing.WaitForOneFrame;
            }

            Vector3 position = transform.position;
            ProjectilePool.Instance.ReturnObject(PoolKey, this);
            entity.transform.position = position;
        }
    }
}