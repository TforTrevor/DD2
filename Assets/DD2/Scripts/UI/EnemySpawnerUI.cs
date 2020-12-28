using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace DD2.UI
{
    public class EnemySpawnerUI : FloatingUI
    {
        [SerializeField] EnemySpawnerUIElement elementPrefab;

        List<EnemySpawnerUIElement> elements = new List<EnemySpawnerUIElement>();
        bool isEnabled;

        protected override void Awake()
        {
            base.Awake();
            isEnabled = true;
        }

        public void ToggleVisible(bool value, EnemySpawner spawner)
        {
            if (!isEnabled && value)
            {
                foreach (KeyValuePair<string, int> item in spawner.GetEnemies())
                {
                    EnemySpawnerUIElement instance = Instantiate(elementPrefab, transform);
                    instance.SetEnemy(item.Key, item.Value);
                    elements.Add(instance);
                }

                CanvasGroup.alpha = 1;
                isEnabled = true;
            }
            else if (isEnabled && !value)
            {
                CanvasGroup.alpha = 0;

                foreach (EnemySpawnerUIElement element in elements)
                {
                    Destroy(element.gameObject);
                }
                elements.Clear();

                isEnabled = false;
            }
        }
    }
}