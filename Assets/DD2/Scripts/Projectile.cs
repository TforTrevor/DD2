using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DD2
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] float speed;
        [SerializeField] float destroyDistance;
        public Transform target;
        [HideInInspector] public ComponentPool<Projectile> objectPool;

        void Update()
        {
            if (target == null)
            {
                return;
            }

            Vector3 direction = Vector3.Normalize(target.position - transform.position);
            transform.position += direction * speed * Time.deltaTime;

            if (Vector3.Distance(transform.position, target.position) < destroyDistance)
            {
                objectPool.ReturnObject("Projectile", this);
            }
        }
    }
}