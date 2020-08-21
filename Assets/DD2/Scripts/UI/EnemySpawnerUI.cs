using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace DD2
{
    public class EnemySpawnerUI : MonoBehaviour
    {
        [SerializeField] EnemySpawnerUIElement elementPrefab;
        [SerializeField] Transform parent;

        List<EnemySpawnerUIElement> elements = new List<EnemySpawnerUIElement>();
        Canvas canvas;
        bool visible;

        void Awake()
        {
            canvas = GetComponent<Canvas>();
            visible = canvas.enabled;
        }

        public void ToggleVisibility(bool value)
        {
            if (value != visible)
            {
                canvas.enabled = value;
                visible = value;
            }
        }

        public void Clear()
        {
            foreach (EnemySpawnerUIElement element in elements)
            {
                Destroy(element);
            }
        }

        public void AddEnemy(string name, int count)
        {
            EnemySpawnerUIElement instance = Instantiate(elementPrefab, parent);
            instance.SetEnemy(name, count);
            elements.Add(instance);
        }
    }
}