using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace DD2
{
    public class Wave : MonoBehaviour
    {
        [SerializeField] List<EnemySpawner> spawners;
        [SerializeField] int maxCount;
        [SerializeField] int currentCount;

        public int CurrentCount { get => currentCount; private set => currentCount = value; }
        public int MaxCount { get => maxCount; private set => maxCount = value; }
        public List<EnemySpawner> Spawners { get => spawners; private set => spawners = value; }
        public event EventHandler WaveUpdated;

        public void StartWave()
        {
            CurrentCount = 0;
            foreach (EnemySpawner spawner in spawners)
            {
                spawner.SpawnEnemies();
                MaxCount += spawner.GetEnemyCount();
            }
            CurrentCount = MaxCount;
            //LevelManager.Instance.UpdateWave();
            WaveUpdated?.Invoke(this, null);
        }

        public void DecrementCount(object sender, Entity entity)
        {
            CurrentCount--;
            entity.onDeath -= DecrementCount;
            //LevelManager.Instance.UpdateWave();
            WaveUpdated?.Invoke(this, null);
            if (CurrentCount <= 0)
            {
                //LevelManager.Instance.EndWave();
            }
        }
    }
}