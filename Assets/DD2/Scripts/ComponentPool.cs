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

    public class ComponentPool<T> : MonoBehaviour where T : MonoBehaviour
    {
        [SerializeField] [ReorderableList] List<Pool> pools;
        Dictionary<string, Queue<T>> poolDictionary = new Dictionary<string, Queue<T>>();

        void Start()
        {
            GameObject poolParent = new GameObject();
            poolParent.name = "Object Pool";
            foreach (Pool pool in pools)
            {
                GameObject groupParent = new GameObject();
                groupParent.transform.SetParent(poolParent.transform);
                groupParent.name = pool.tag;

                Queue<T> queue = new Queue<T>();

                for (int i = 0; i < pool.size; i++)
                {
                    GameObject poolObject = Instantiate(pool.prefab, groupParent.transform);
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
                    GameObject poolObject = Instantiate(pool.prefab);
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