﻿using DD2.AI.Scorers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DD2
{
    public class EnemySpawnerCanvas : MonoBehaviour
    {
        [SerializeField] EnemySpawnerUI spawnerUI;
        [SerializeField] float uiOffset;

        Dictionary<EnemySpawner, EnemySpawnerUI> uiDictionary = new Dictionary<EnemySpawner, EnemySpawnerUI>();
        bool show;

        void Start()
        {
            //LevelManager.Instance.WaveEnded += Show;
            //LevelManager.Instance.WaveStarted += Hide;
            Show();
        }

        public void Show()
        {
            uiDictionary.Clear();
            Wave wave = LevelManager.Instance.GetWave();
            foreach (EnemySpawner spawner in wave.Spawners)
            {
                EnemySpawnerUI instance = Instantiate(spawnerUI, transform);
                foreach (KeyValuePair<string, int> pair in spawner.GetEnemies())
                {
                    instance.AddEnemy(pair.Key, pair.Value);
                }
                uiDictionary.Add(spawner, instance);
            }
            show = true;
        }

        public void Hide()
        {
            foreach (KeyValuePair<EnemySpawner, EnemySpawnerUI> pair in uiDictionary)
            {
                Destroy(uiDictionary[pair.Key].gameObject);
            }
            show = false;
        }

        void LateUpdate()
        {
            if (show)
            {
                foreach (KeyValuePair<EnemySpawner, EnemySpawnerUI> pair in uiDictionary)
                {
                    EnemySpawner spawner = pair.Key;
                    EnemySpawnerUI spawnerUI = pair.Value;

                    Vector3 pos = LevelManager.Instance.Camera.WorldToScreenPoint(spawner.transform.position + Vector3.up * uiOffset);
                    if (pos.z > 0 && pos.z < 15)
                    {
                        spawnerUI.ToggleVisibility(true);
                        spawnerUI.transform.position = pos;

                        float value = Util.Utilities.Remap(pos.z, 15, 5, 0, 1);
                        spawnerUI.transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one * 0.5f, value);
                    }
                    else
                    {
                        spawnerUI.ToggleVisibility(false);
                    }
                }
            }            
        }
    }
}