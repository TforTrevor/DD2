using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

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
        [SerializeField] [ReorderableList] protected List<Pool<T>> pools;
        protected Dictionary<string, Queue<T>> poolDictionary = new Dictionary<string, Queue<T>>();
        protected Dictionary<string, GameObject> parentDictionary = new Dictionary<string, GameObject>();

        protected override void Awake()
        {
            base.Awake();
            CreatePool();
        }

        protected virtual void CreatePool()
        {
            foreach (Pool<T> pool in pools)
            {
                GameObject poolParent = new GameObject();
                parentDictionary.Add(pool.tag, poolParent);
                poolParent.transform.SetParent(transform);
                poolParent.name = pool.tag;

                Queue<T> queue = new Queue<T>();

                for (int i = 0; i < pool.size; i++)
                {
                    T poolObject = Instantiate(pool.prefab, poolParent.transform);
                    poolObject.gameObject.SetActive(false);
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
                    T poolObject = Instantiate(pool.prefab, parentDictionary[pool.tag].transform);
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
            poolDictionary[key].Enqueue(poolComponent);
        }
    }
}