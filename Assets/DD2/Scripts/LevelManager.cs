using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

namespace DD2
{
    public class LevelManager : Singleton<LevelManager>
    {
        [SerializeField] new Camera camera;
        [SerializeField] [ReorderableList] List<Core> cores;
        [SerializeField] [ReorderableList] List<Wave> waves;
        [SerializeField] int currentWave = 0;
        [SerializeField] [ReadOnly] bool waveInProgress;

        public List<Core> Cores { get => cores; private set => cores = value; }
        public int WaveCount { get => waves.Count; }
        public int CurrentWave { get => currentWave; private set => currentWave = value; }
        public Camera Camera { get => camera; set => camera = value; }

        public event EventHandler<Wave> waveUpdated;
        public event EventHandler<int> waveStarted;
        public event EventHandler<int> waveEnded;

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
            CurrentWave = 0;
            waveInProgress = false;
        }

        public void StartWave()
        {
            if (!waveInProgress)
            {
                waves[CurrentWave].StartWave();
                waveInProgress = true;
                waveStarted?.Invoke(this, CurrentWave);
            }            
        }

        public void WaveUpdated(Wave wave)
        {
            waveUpdated?.Invoke(this, wave);
        }

        public void EndWave()
        {
            CurrentWave++;
            waveInProgress = false;
            waveEnded?.Invoke(this, CurrentWave);
        }

        public Wave GetWave(int index)
        {
            return waves[index];
        }
    }
}