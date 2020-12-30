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
        [SerializeField] float heightOffset;
        [SerializeField] float maxRange;
        [SerializeField] float minRange;

        List<EnemySpawnerUIElement> elements = new List<EnemySpawnerUIElement>();
        EnemySpawner currentSpawner;
        int currentWave;
        bool isEnabled;

        protected override void Awake()
        {
            base.Awake();
            isEnabled = true;
        }

        public void Show(EnemySpawner spawner)
        {
            if (!isEnabled)
            {
                currentSpawner = spawner;

                foreach (KeyValuePair<string, int> item in spawner.GetEnemies())
                {
                    EnemySpawnerUIElement instance = Instantiate(elementPrefab, transform);
                    instance.SetEnemy(item.Key, item.Value);
                    elements.Add(instance);
                }

                isEnabled = true;
            }
        }

        public override void Hide()
        {
            base.Hide();

            if (isEnabled)
            {
                foreach (EnemySpawnerUIElement element in elements)
                {
                    Destroy(element.gameObject);
                }
                elements.Clear();

                isEnabled = false;
            }
        }

        void LateUpdate()
        {
            if (isEnabled)
            {
                Vector3 pos = LevelManager.Instance.Camera.WorldToScreenPoint(currentSpawner.transform.position + Vector3.up * heightOffset);
                if (pos.z > 0 && pos.z < maxRange)
                {
                    CanvasGroup.alpha = 1;
                    transform.position = pos;

                    float value = Util.Utilities.Remap(pos.z, maxRange, minRange, 0, 1);
                    transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one * 0.5f, value);
                }
                else
                {
                    CanvasGroup.alpha = 0;
                }
            }
        }
    }
}