using DD2.AI.Scorers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DD2.UI
{
    public class EnemySpawnerCanvas : FloatingUICanvas<EnemySpawnerUI>
    {
        [SerializeField] WaveInfo waveInfo;

        List<EnemySpawnerUI> uiElements = new List<EnemySpawnerUI>();

        public void Show()
        {
            List<EnemySpawner> spawners = waveInfo.Spawners;
            if (spawners != null)
            {
                foreach (EnemySpawner spawner in waveInfo.Spawners)
                {
                    EnemySpawnerUI instance = GetElement();
                    instance.Show(spawner);
                    uiElements.Add(instance);
                }
            }            
        }

        public void Hide()
        {
            foreach (EnemySpawnerUI ui in uiElements)
            {
                ui.Hide();
                ReturnElement(ui);
            }
        }
    }
}