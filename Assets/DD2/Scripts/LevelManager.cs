using DD2.Actions;
using DD2.AI;
using MEC;
using System.Collections.Generic;
using UnityAtoms.BaseAtoms;
using UnityEngine;

namespace DD2
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField] Player player;
        [SerializeField] Transform playerSpawn;
        [SerializeField] float respawnTime;
        [SerializeField] List<Core> cores;
        [SerializeField] List<Wave> waves;
        [SerializeField] VoidEvent levelLoaded;
        [SerializeField] BoolEvent levelEnded;
        [SerializeField] LoadLevel loadHub;
        [SerializeField] WaveInfo waveInfo;
        [SerializeField] EntityList enemyList;
        [SerializeField] EntityList coreList;
        [SerializeField] EntityList towerList;
        [SerializeField] BoolVariable enableInput;
        [SerializeField] bool callLevelLoaded;

        bool gameOver;

        void Awake()
        {
            waveInfo.Initialize(waves);
            enemyList.Entities.Clear();
            coreList.Entities.Clear();
            foreach (Core core in cores)
            {
                coreList.Entities.Add(core);
            }
        }
        
        void Start()
        {
            enableInput.Value = true;
            if (callLevelLoaded)
            {
                levelLoaded.Raise();
            }            
        }

        public void StartWave()
        {
            if (!gameOver)
            {
                waveInfo.StartWave();
            }            
        }

        public void EndWave()
        {
            if (!gameOver)
            {
                waveInfo.NextWave();
            }
        }

        public void WinGame()
        {
            levelEnded.Raise(true);
        }

        public void LoseGame()
        {
            gameOver = true;

            List<Entity> tempEnemies = new List<Entity>(enemyList.Entities);
            foreach (Entity enemy in tempEnemies)
            {
                enemy.Damage(null, float.MaxValue);
            }
            enemyList.Entities.Clear();

            levelEnded.Raise(false);
        }

        public void ExitLevel()
        {
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

        public void ToggleTowerRange(bool value)
        {
            foreach (Entity entity in towerList.Entities)
            {
                Tower tower = (Tower)entity;
                tower.ToggleRangeIndicator(value);
            }
        }
    }
}