using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MEC;
using UnityEngine.VFX;

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
        [SerializeField] VisualEffect vfx;
        [SerializeField] Transform model;
        [SerializeField] float vfxLingerTime;

        public string PoolKey { get => poolKey; private set => poolKey = value; }
        public Entity Entity { get => entity; private set => entity = value; }

        CoroutineHandle vfxHandle;

        void Initialize()
        {
            gameObject.SetActive(true);
            model.gameObject.SetActive(true);
            vfx.Play();
            Timing.KillCoroutines(vfxHandle);
        }

        public void Initialize(Transform target, System.Action callback)
        {
            Initialize();
            Timing.RunCoroutine(MoveRoutine(target, callback));
        }

        public void Initialize(Vector3 direction, System.Action callback)
        {
            Initialize();
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

            model.gameObject.SetActive(false);
            vfx.Stop();
            vfxHandle = Timing.CallDelayed(vfxLingerTime, () =>
            {
                ProjectilePool.Instance.ReturnObject(PoolKey, this);
            });
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

            entity.transform.position = transform.position;
            model.gameObject.SetActive(false);
            vfx.Stop();
            vfxHandle = Timing.CallDelayed(vfxLingerTime, () =>
            {
                ProjectilePool.Instance.ReturnObject(PoolKey, this);
            });
        }
    }
}