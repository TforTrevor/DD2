using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using UnityAtoms.BaseAtoms;
using DD2.Actions;
using DD2.AI;

namespace DD2
{
    public class LevelManager : Singleton<LevelManager>
    {
        [SerializeField] new Camera camera;
        [SerializeField] [ReorderableList] List<Core> cores;
        [SerializeField] [ReorderableList] List<Wave> waves;
        [SerializeField] int currentWave = 0;
        [SerializeField] [ReadOnly] bool waveInProgress;
        [SerializeField] VoidEvent waveStarted;
        [SerializeField] VoidEvent waveUpdated;
        [SerializeField] VoidEvent waveEnded;
        [SerializeField] LoadLevel loadHub;

        public List<Core> Cores { get => cores; private set => cores = value; }
        public int WaveCount { get => waves.Count; }
        public int CurrentWave { get => currentWave; private set => currentWave = value; }
        public Camera Camera { get => camera; set => camera = value; }
        public VoidEvent WaveStarted { get => waveStarted; private set => waveStarted = value; }
        public VoidEvent WaveUpdated { get => waveUpdated; private set => waveUpdated = value; }
        public VoidEvent WaveEnded { get => waveEnded; private set => waveEnded = value; }   
        public List<Enemy> Enemies { get; private set; }

        protected override void Awake()
        {
            base.Awake();
            Enemies = new List<Enemy>();
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
                WaveStarted.Raise();
                //waveStarted?.Invoke(this, CurrentWave);
            }            
        }

        public void UpdateWave()
        {
            WaveUpdated.Raise();
        }

        public void EndWave()
        {
            CurrentWave++;
            waveInProgress = false;
            WaveEnded.Raise();
            //waveEnded?.Invoke(this, CurrentWave);
        }

        public Wave GetWave()
        {
            return waves[CurrentWave];
        }

        public void LoseGame()
        {
            List<Enemy> tempEnemies = new List<Enemy>(Enemies);
            foreach (Enemy enemy in tempEnemies)
            {
                enemy.Damage(null, float.MaxValue);
            }

            loadHub.DoAction(null, null);
        }
    }
}