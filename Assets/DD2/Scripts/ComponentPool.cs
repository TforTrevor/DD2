using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

namespace DD2
{
    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
        public bool canExpand;
    }

    public class ComponentPool<T> : Singleton<ComponentPool<T>> where T : MonoBehaviour
    {
        [SerializeField] [ReorderableList] List<Pool> pools;
        Dictionary<string, Queue<T>> poolDictionary = new Dictionary<string, Queue<T>>();
        Dictionary<string, GameObject> parentDictionary = new Dictionary<string, GameObject>();

        protected override void Awake()
        {
            base.Awake();
            foreach (Pool pool in pools)
            {
                GameObject poolParent = new GameObject();
                parentDictionary.Add(pool.tag, poolParent);
                poolParent.transform.SetParent(transform);
                poolParent.name = pool.tag;

                Queue<T> queue = new Queue<T>();

                for (int i = 0; i < pool.size; i++)
                {
                    GameObject poolObject = Instantiate(pool.prefab, poolParent.transform);
                    poolObject.SetActive(false);
                    T poolComponent = poolObject.GetComponent<T>();
                    if (poolComponent != null)
                    {
                        queue.Enqueue(poolComponent);
                    }
                }

                poolDictionary.Add(pool.tag, queue);
            }
        }

        public T GetObject(string key)
        {
            if (!poolDictionary.ContainsKey(key))
            {
                return null;
            }

            if (poolDictionary[key].Count == 0)
            {
                Pool pool = pools.Find(x => x.tag == key);
                if (pool.canExpand)
                {
                    GameObject poolObject = Instantiate(pool.prefab, parentDictionary[pool.tag].transform);
                    poolObject.SetActive(false);
                    T poolComponent = poolObject.GetComponent<T>();
                    return poolComponent;
                }
                return null;
            }
            return poolDictionary[key].Dequeue();
        }

        public void ReturnObject(string key, T poolComponent)
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