using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

namespace DD2
{
    public class LevelManager : Singleton<LevelManager>
    {
        [SerializeField] [ReorderableList] List<Core> cores;
        [SerializeField] [ReorderableList] List<Wave> waves;
        [SerializeField] int currentWave = 0;
        [SerializeField] [ReadOnly] bool waveInProgress;

        public List<Core> Cores { get => cores; private set => cores = value; }
        public event EventHandler<Wave> waveUpdated;

        protected override void Awake()
        {
            base.Awake();

            Core[] temp = FindObjectsOfType<Core>();
            if (Cores == null)
            {
                Cores = new List<Core>(temp);
            }
            else
            {
                foreach (Core core in temp)
                {
                    if (!Cores.Contains(core))
                    {
                        Cores.Add(core);
                    }
                }
            }            
        }
        
        void Start()
        {
            currentWave = 0;
            waveInProgress = false;
        }

        public void StartWave()
        {
            if (!waveInProgress)
            {
                waves[currentWave].StartWave();
                waveInProgress = true;
            }            
        }

        public void WaveUpdated(Wave wave)
        {
            waveUpdated?.Invoke(this, wave);
        }

        public void EndWave()
        {
            currentWave++;
            waveInProgress = false;
        }
    }
}