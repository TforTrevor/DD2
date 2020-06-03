using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DD2
{
    public class HealthBarPool : ComponentPool<HealthBar>
    {
        protected override void CreatePool()
        {
            foreach (Pool<HealthBar> pool in pools)
            {
                Queue<HealthBar> queue = new Queue<HealthBar>();

                for (int i = 0; i < pool.size; i++)
                {
                    HealthBar poolObject = Instantiate(pool.prefab, transform);
                    poolObject.transform.localScale = Vector3.zero;
                    if (poolObject != null)
                    {
                        queue.Enqueue(poolObject);
                    }
                }

                poolDictionary.Add(pool.tag, queue);
            }
        }

        public override HealthBar GetObject(string key)
        {
            if (!poolDictionary.ContainsKey(key))
            {
                return null;
            }

            if (poolDictionary[key].Count == 0)
            {
                Pool<HealthBar> pool = pools.Find(x => x.tag == key);
                if (pool.canExpand)
                {
                    HealthBar poolObject = Instantiate(pool.prefab);
                    poolObject.transform.localScale = Vector3.zero;
                    return poolObject;
                }
                return null;
            }
            return poolDictionary[key].Dequeue();
        }

        public override void ReturnObject(string key, HealthBar poolComponent)
        {
            if (!poolDictionary.ContainsKey(key))
            {
                Destroy(poolComponent);
                return;
            }

            poolComponent.transform.localScale = Vector3.zero;
            poolDictionary[key].Enqueue(poolComponent);
        }
    }
}