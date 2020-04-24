using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using MEC;

namespace DD2
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] [ReorderableList] List<Spawn> spawns;

        void Start()
        {
            foreach (Spawn spawn in spawns)
            {
                if (spawn.IsBatch())
                {
                    for (int i = 0; i < spawn.GetAmount(); i++)
                    {
                        SpawnEntity(spawn);
                    }
                }
                else
                {
                    Timing.RunCoroutine(SpawnRoutine(spawn));
                }                
            }
        }

        IEnumerator<float> SpawnRoutine(Spawn spawn)
        {
            yield return Timing.WaitForSeconds(spawn.GetDelay());
            for (int i = 0; i < spawn.GetAmount(); i++)
            {
                SpawnEntity(spawn);
                yield return Timing.WaitForSeconds(spawn.GetRate());
            }
        }

        void SpawnEntity(Spawn spawn)
        {
            Entity entity = EntityPool.Instance.GetObject(spawn.GetKey());
            entity.transform.position = transform.position;
            entity.gameObject.SetActive(true);
        }
    }

    [System.Serializable]
    public class Spawn
    {
        [SerializeField] string key;
        [SerializeField] int amount;
        [SerializeField] float rate;
        [SerializeField] bool batch;
        [SerializeField] float delay;

        public string GetKey()
        {
            return key;
        }

        public int GetAmount()
        {
            return amount;
        }

        public float GetRate()
        {
            return rate;
        }

        public bool IsBatch()
        {
            return batch;
        }

        public float GetDelay()
        {
            return delay;
        }
    }
}