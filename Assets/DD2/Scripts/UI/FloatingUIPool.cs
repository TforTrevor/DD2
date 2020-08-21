using DD2.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DD2
{
    public class FloatingUIPool : ComponentPool<FloatingUI>
    {
        protected override void CreatePool()
        {
            foreach (Pool<FloatingUI> pool in pools)
            {
                Queue<FloatingUI> queue = new Queue<FloatingUI>();

                for (int i = 0; i < pool.size; i++)
                {
                    FloatingUI poolObject = Instantiate(pool.prefab, transform);
                    //poolObject.transform.localScale = Vector3.zero;
                    poolObject.ToggleCanvas(false);
                    if (poolObject != null)
                    {
                        queue.Enqueue(poolObject);
                    }
                }

                poolDictionary.Add(pool.tag, queue);
            }
        }

        public override FloatingUI GetObject(string key)
        {
            if (!poolDictionary.ContainsKey(key))
            {
                return null;
            }

            if (poolDictionary[key].Count == 0)
            {
                Pool<FloatingUI> pool = pools.Find(x => x.tag == key);
                if (pool.canExpand)
                {
                    FloatingUI poolObject = Instantiate(pool.prefab);
                    poolObject.transform.localScale = Vector3.zero;
                    return poolObject;
                }
                return null;
            }
            return poolDictionary[key].Dequeue();
        }

        public override void ReturnObject(string key, FloatingUI poolComponent)
        {
            if (!poolDictionary.ContainsKey(key))
            {
                Destroy(poolComponent);
                return;
            }

            //poolComponent.transform.localScale = Vector3.zero;
            poolComponent.ToggleCanvas(false, () =>
            {
                poolDictionary[key].Enqueue(poolComponent);
            });            
        }
    }
}