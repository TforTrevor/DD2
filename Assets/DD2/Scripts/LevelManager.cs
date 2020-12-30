using DD2.Actions;
using DD2.AI;
using MEC;
using System.Collections.Generic;
using UnityAtoms.BaseAtoms;
using UnityEngine;

namespace DD2
{
    public class LevelManager : Singleton<LevelManager>
    {
        [SerializeField] Player player;
        [SerializeField] new Camera camera;
        [SerializeField] Transform playerSpawn;
        [SerializeField] float respawnTime;
        [SerializeField] List<Core> cores;
        [SerializeField] List<Wave> waves;
        [SerializeField] int currentWave = 0;
        [SerializeField] bool waveInProgress;
        [SerializeField] VoidEvent levelLoaded;
        [SerializeField] VoidEvent waveStarted;
        [SerializeField] VoidEvent waveUpdated;
        [SerializeField] VoidEvent waveEnded;
        [SerializeField] LoadLevel loadHub;

        bool gameOver;

        public List<Core> Cores { get => cores; private set => cores = value; }
        public int WaveCount { get => waves.Count; }
        public int CurrentWave { get => currentWave; private set => currentWave = value; }
        public Camera Camera { get => camera; set => camera = value; }
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
            levelLoaded.Raise();
        }

        public void StartWave()
        {
            if (!waveInProgress)
            {
                waves[CurrentWave].StartWave();
                waveInProgress = true;
                waveStarted.Raise();
                //waveStarted?.Invoke(this, CurrentWave);
            }            
        }

        public void UpdateWave()
        {
            waveUpdated.Raise();
        }

        public void EndWave()
        {
            if (!gameOver)
            {
                CurrentWave++;
                waveInProgress = false;
                waveEnded.Raise();
                //waveEnded?.Invoke(this, CurrentWave);
            }
        }

        public Wave GetWave()
        {
            if (CurrentWave < waves.Count)
            {
                return waves[CurrentWave];
            }
            return null;
        }

        public void LoseGame()
        {
            gameOver = true;

            List<Enemy> tempEnemies = new List<Enemy>(Enemies);
            foreach (Enemy enemy in tempEnemies)
            {
                enemy.Damage(null, float.MaxValue);
            }

            loadHub.DoAction(null, null);
        }

        public void RespawnPlayer()
        {
            if (!player.IsAlive)
            {
                Timing.RunCoroutine(RespawnRoutine());
            }
        }

        IEnumerator<float> RespawnRoutine()
        {
            yield return Timing.WaitForSeconds(respawnTime);
            player.Respawn();
            player.transform.position = playerSpawn.transform.position;
            player.transform.forward = playerSpawn.transform.forward;
            player.gameObject.SetActive(true);
        }
    }
}