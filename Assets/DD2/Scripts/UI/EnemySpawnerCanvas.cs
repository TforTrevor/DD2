using DD2.AI.Scorers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DD2.UI
{
    public class EnemySpawnerCanvas : FloatingUICanvas<EnemySpawnerUI>
    {
        [SerializeField] float heightOffset;

        Dictionary<EnemySpawner, EnemySpawnerUI> uiDictionary = new Dictionary<EnemySpawner, EnemySpawnerUI>();
        bool isEnabled;

        void Start()
        {
            Show();
        }

        public void Show()
        {
            uiDictionary.Clear();
            Wave wave = LevelManager.Instance.GetWave();
            foreach (EnemySpawner spawner in wave.Spawners)
            {
                EnemySpawnerUI instance = ShowElement();
                uiDictionary.Add(spawner, instance);
            }
            isEnabled = true;
        }

        public void Hide()
        {
            foreach (KeyValuePair<EnemySpawner, EnemySpawnerUI> pair in uiDictionary)
            {
                HideElement(pair.Value);
                //Destroy(uiDictionary[pair.Key].gameObject);
            }
            isEnabled = false;
        }

        void LateUpdate()
        {
            if (isEnabled)
            {
                foreach (KeyValuePair<EnemySpawner, EnemySpawnerUI> pair in uiDictionary)
                {
                    EnemySpawner spawner = pair.Key;
                    EnemySpawnerUI spawnerUI = pair.Value;

                    Vector3 pos = LevelManager.Instance.Camera.WorldToScreenPoint(spawner.transform.position + Vector3.up * heightOffset);
                    if (pos.z > 0 && pos.z < 15)
                    {
                        spawnerUI.ToggleVisible(true, spawner);
                        spawnerUI.transform.position = pos;

                        float value = Util.Utilities.Remap(pos.z, 15, 5, 0, 1);
                        spawnerUI.transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one * 0.5f, value);
                    }
                    else
                    {
                        spawnerUI.ToggleVisible(false, spawner);
                    }
                }
            }            
        }
    }
}