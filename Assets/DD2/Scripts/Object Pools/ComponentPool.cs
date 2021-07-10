using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;

namespace DD2
{
    [System.Serializable]
    public class Pool<T>
    {
        public string tag;
        public T prefab;
        public int size;
        public bool canExpand;
    }

    public class ComponentPool<T> : Singleton<ComponentPool<T>> where T : MonoBehaviour
    {
        [SerializeField]  protected List<Pool<T>> pools;
        protected Dictionary<string, Queue<T>> poolDictionary = new Dictionary<string, Queue<T>>();

        protected override void Awake()
        {
            base.Awake();
            CreatePool();
        }

        protected virtual void CreatePool()
        {
            foreach (Pool<T> pool in pools)
            {
                Queue<T> queue = new Queue<T>();

                for (int i = 0; i < pool.size; i++)
                {
                    T poolObject = Instantiate(pool.prefab);
                    poolObject.name = "Object Pool/" + name + "/" + pool.prefab.name + " (" + i + ")";
                    poolObject.gameObject.SetActive(false);
                    poolObject.transform.position = new Vector3(0, -1000, 0);
                    if (poolObject != null)
                    {
                        queue.Enqueue(poolObject);
                    }
                }

                poolDictionary.Add(pool.tag, queue);
            }
        }

        public virtual T GetObject(string key)
        {
            if (!poolDictionary.ContainsKey(key))
            {
                return null;
            }

            if (poolDictionary[key].Count == 0)
            {
                Pool<T> pool = pools.Find(x => x.tag == key);
                if (pool.canExpand)
                {
                    T poolObject = Instantiate(pool.prefab);
                    poolObject.gameObject.SetActive(false);
                    return poolObject;
                }
                return null;
            }
            return poolDictionary[key].Dequeue();
        }

        public virtual void ReturnObject(string key, T poolComponent)
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

        public virtual int GetCount(string key)
        {
            return poolDictionary[key] != null ? poolDictionary[key].Count : 0;
        }
    }
}