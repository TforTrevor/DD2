using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

namespace DD2
{
    public class Wave : MonoBehaviour
    {
        [SerializeField] [ReorderableList] List<EnemySpawner> spawners;
        [SerializeField] [ReadOnly] int maxCount;
        [SerializeField] [ReadOnly] int currentCount;

        public int CurrentCount { get => currentCount; private set => currentCount = value; }
        public int MaxCount { get => maxCount; private set => maxCount = value; }
        public List<EnemySpawner> Spawners { get => spawners; private set => spawners = value; }

        public void StartWave()
        {
            CurrentCount = 0;
            foreach (EnemySpawner spawner in Spawners)
            {
                spawner.SpawnEnemies();
                MaxCount += spawner.GetEnemyCount();
            }
            CurrentCount = MaxCount;
            LevelManager.Instance.UpdateWave();
        }

        public void DecrementCount(object sender, Entity entity)
        {
            CurrentCount--;
            entity.onDeath -= DecrementCount;
            LevelManager.Instance.UpdateWave();
            if (CurrentCount <= 0)
            {
                LevelManager.Instance.EndWave();
            }
        }
    }
}