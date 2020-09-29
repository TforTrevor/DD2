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
        [SerializeField] LayerMask hitMask;
        [SerializeField] LayerMask penetrateMask;
        [SerializeField] int maxPenetrations;
        [SerializeField] VisualEffect vfx;
        [SerializeField] Light vfxLight;
        [SerializeField] Transform model;
        [SerializeField] float vfxLingerTime;

        public string PoolKey { get => poolKey; private set => poolKey = value; }
        public Entity Entity { get => entity; private set => entity = value; }

        LTDescr lightHandle;
        float lightIntensity;
        CoroutineHandle vfxHandle;
        int currentPenetrations;

        void Initialize()
        {
            gameObject.SetActive(true);
            model.gameObject.SetActive(true);
            currentPenetrations = 0;
            vfxLight.intensity = lightIntensity;
            vfx?.Play();            
            Timing.KillCoroutines(vfxHandle);
        }

        public void Initialize(Transform target, System.Action callback)
        {
            Initialize();
            Timing.RunCoroutine(MoveRoutine(target, callback));
        }

        public void Initialize(Vector3 direction, System.Action<Entity> callback)
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
            vfx?.Stop();
            lightIntensity = vfxLight.intensity;
            lightHandle = LeanTween.value(lightIntensity, 0, vfxLingerTime).setOnUpdate((value) => vfxLight.intensity = value);
            vfxHandle = Timing.CallDelayed(vfxLingerTime, () => ProjectilePool.Instance.ReturnObject(PoolKey, this));
        }

        IEnumerator<float> MoveRoutine(Vector3 direction, System.Action<Entity> callback)
        {
            float distanceTraveled = 0;

            while (distanceTraveled < endDistance)
            {
                RaycastHit hit;
                if (Physics.SphereCast(transform.position, radius, direction, out hit, speed * Time.deltaTime, hitMask))
                {
                    //transform.position += direction * speed * Time.deltaTime;
                    //transform.forward = direction;
                    transform.position = hit.point - (direction * speed * Time.deltaTime);
                    Entity entity = hit.transform.GetComponent<Entity>();
                    callback?.Invoke(entity);

                    if (Util.Utilities.IsInLayer(hit.transform.gameObject, penetrateMask))
                    {
                        currentPenetrations++;
                        if (currentPenetrations > maxPenetrations)
                            break;
                    }
                    else
                    {
                        break;
                    }
                }

                transform.position += direction * speed * Time.deltaTime;
                distanceTraveled += speed * Time.deltaTime;
                transform.forward = direction;

                yield return Timing.WaitForOneFrame;
            }

            entity.transform.position = transform.position;
            model.gameObject.SetActive(false);
            vfx?.Stop();
            lightIntensity = vfxLight.intensity;
            lightHandle = LeanTween.value(lightIntensity, 0, vfxLingerTime).setOnUpdate((value) => 
            {
                if (vfxLight != null)
                {
                    vfxLight.intensity = value;
                }                
            });
            vfxHandle = Timing.CallDelayed(vfxLingerTime, () =>
            {
                ProjectilePool.Instance?.ReturnObject(PoolKey, this);
            });
        }
    }
}