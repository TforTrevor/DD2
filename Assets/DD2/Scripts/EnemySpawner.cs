using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using MEC;

namespace DD2
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] Wave wave;
        [SerializeField] float randomPosition;
        [SerializeField] [ReorderableList] List<Spawn> spawns;

        public void SpawnEnemies()
        {
            foreach (Spawn spawn in spawns)
            {
                if (spawn.IsBatch)
                {
                    for (int i = 0; i < spawn.Amount; i++)
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
            yield return Timing.WaitForSeconds(spawn.Delay);
            for (int i = 0; i < spawn.Amount; i++)
            {
                SpawnEntity(spawn);
                yield return Timing.WaitForSeconds(spawn.Rate);
            }
        }

        void SpawnEntity(Spawn spawn)
        {
            Entity entity = EntityPool.Instance.GetObject(spawn.Key);
            float randomX = Random.Range(-randomPosition, randomPosition);
            float randomZ = Random.Range(-randomPosition, randomPosition);
            entity.transform.position = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);
            entity.transform.forward = transform.forward;
            entity.gameObject.SetActive(true);
            entity.onDeath += DecrementCount;
        }

        void DecrementCount(object sender, Entity entity)
        {
            wave.DecrementCount(sender, entity);
        }

        public Dictionary<string, int> GetEnemies()
        {
            Dictionary<string, int> list = new Dictionary<string, int>();
            foreach (Spawn spawn in spawns)
            {
                if (list.ContainsKey(spawn.Key))
                {
                    list[spawn.Key] += spawn.Amount;
                }
                else
                {
                    list.Add(spawn.Key, spawn.Amount);
                }
            }
            return list;
        }

        public int GetEnemyCount()
        {
            int count = 0;
            foreach (Spawn spawn in spawns)
            {
                count += spawn.Amount;
            }
            return count;
        }
    }

    [System.Serializable]
    public class Spawn
    {
        [SerializeField] string key;
        [SerializeField] int amount;
        [SerializeField] float rate;
        [SerializeField] bool isBatch;
        [SerializeField] float delay;

        public string Key { get => key; private set => key = value; }
        public int Amount { get => amount; private set => amount = value; }
        public float Rate { get => rate; private set => rate = value; }
        public bool IsBatch { get => isBatch; private set => isBatch = value; }
        public float Delay { get => delay; private set => delay = value; }
    }
}