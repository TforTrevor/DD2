using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityAtoms.BaseAtoms;

namespace DD2
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Wave Info")]
    public class WaveInfo : ScriptableObject
    {
        [SerializeField] VoidEvent waveStarted;
        [SerializeField] VoidEvent waveUpdated;
        [SerializeField] VoidEvent waveEnded;

        List<Wave> waves;

        public int WaveIndex { get; private set; }
        public int TotalWaves { get; private set; }
        public bool InProgress { get; private set; }
        public int CurrentEnemyCount { get; private set; }
        public int TotalEnemyCount { get; private set; }
        public List<EnemySpawner> Spawners { get => waves.Count > 0 ? waves[WaveIndex].Spawners : null; }

        public void Initialize(List<Wave> waves)
        {
            WaveIndex = 0;
            TotalWaves = waves.Count;
            InProgress = false;
            this.waves = waves;
        }

        public void NextWave()
        {
            SetWave(WaveIndex + 1);
        }

        public void SetWave(int index)
        {
            WaveIndex = index;
        }

        public void StartWave()
        {
            if (!InProgress)
            {
                waves[WaveIndex].WaveUpdated += WaveUpdated;
                CurrentEnemyCount = waves[WaveIndex].CurrentCount;
                TotalEnemyCount = waves[WaveIndex].MaxCount;
                InProgress = true;
                waves[WaveIndex].StartWave();
                waveStarted.Raise();
            }            
        }

        void WaveUpdated(object sender, EventArgs eventArgs)
        {
            CurrentEnemyCount = waves[WaveIndex].CurrentCount;
            TotalEnemyCount = waves[WaveIndex].MaxCount;
            waveUpdated.Raise();

            if (CurrentEnemyCount <= 0)
            {
                InProgress = false;
                waveEnded.Raise();
            }
        }
    }
}
