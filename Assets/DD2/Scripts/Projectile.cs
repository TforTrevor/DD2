using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MEC;

namespace DD2
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] float speed;
        [SerializeField] float endDistance;

        public void Initialize(Entity target, string key)
        {
            gameObject.SetActive(true);
            Timing.RunCoroutine(MoveRoutine(target, key));
        }

        IEnumerator<float> MoveRoutine(Entity target, string key)
        {
            while (Vector3.Distance(transform.position, target.transform.position) > endDistance)
            {
                Vector3 direction = Vector3.Normalize(target.transform.position - transform.position);
                transform.position += direction * speed * Time.deltaTime;
                transform.LookAt(target.transform);

                yield return Timing.WaitForOneFrame;
            }

            ProjectilePool.Instance.ReturnObject(key, this);
        }
    }
}