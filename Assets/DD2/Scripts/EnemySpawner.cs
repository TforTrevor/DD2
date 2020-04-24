using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DD2.Actions;

namespace DD2
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] string key;
        [SerializeField] int amount;

        void Start()
        {
            for (int i = 0; i < amount; i++)
            {
                Entity entity = EntityPool.Instance.GetObject(key);
                entity.transform.position = transform.position;
                entity.gameObject.SetActive(true);
            }
        }
    }
}