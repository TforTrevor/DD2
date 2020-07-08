using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DD2
{
    public class ProjectilePool : ComponentPool<Projectile>
    {
        public override void ReturnObject(string key, Projectile poolComponent)
        {
            if (!poolDictionary.ContainsKey(key))
            {
                Destroy(poolComponent);
                return;
            }

            poolComponent.gameObject.SetActive(false);
            poolComponent.transform.position = new Vector3(0, -1000, 0);
            poolDictionary[key].Enqueue(poolComponent);
        }
    }
}