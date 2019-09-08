using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

namespace DD2
{
    public class ObjectPool : MonoBehaviour
    {
        [SerializeField] [ReorderableList] List<Pool> pools;
        Dictionary<string, Queue<GameObject>> poolDictionary = new Dictionary<string, Queue<GameObject>>();

        void Start()
        {
            foreach (Pool pool in pools)
            {
                Queue<GameObject> queue = new Queue<GameObject>();

                for (int i = 0; i < pool.size; i++)
                {
                    GameObject poolObject = Instantiate(pool.prefab);
                    poolObject.SetActive(false);
                    queue.Enqueue(poolObject);
                }

                poolDictionary.Add(pool.tag, queue);
            }
        }

        public GameObject GetObject(string key)
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
                    return poolObject;
                }
                return null;
            }
            return poolDictionary[key].Dequeue();
        }

        public void ReturnObject(string key, GameObject poolObject) 
        {
            if (!poolDictionary.ContainsKey(key))
            {
                Destroy(poolObject);
                return;
            }

            poolObject.SetActive(false);
            poolDictionary[key].Enqueue(poolObject);
        }
    }
}