using DD2.AI;
using NaughtyAttributes;
using RoboRyanTron.SearchableEnum;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DD2
{
    public class TowerWheel : MonoBehaviour
    {
        [SerializeField] BuildTower buildTower;
        [SerializeField] TowerWheelButton towerButton;
        [SerializeField] Transform parent;
        [SerializeField] [ReorderableList] List<Tower> towerPrefabs;

        Canvas canvas;
        List<TowerWheelButton> towerButtons = new List<TowerWheelButton>();

        void Awake()
        {
            canvas = GetComponent<Canvas>();
        }

        void Start()
        {
            canvas.enabled = false;
            foreach (Tower tower in towerPrefabs)
            {
                TowerWheelButton temp = Instantiate(towerButton, parent);
                temp.Text.text = tower.name;
                towerButtons.Add(temp);
                temp.Button.onClick.AddListener(() =>
                {
                    if (buildTower.Begin(tower))
                    {
                        canvas.enabled = false;
                        Cursor.lockState = CursorLockMode.Locked;
                        Cursor.visible = false;
                    }                    
                });
            }
        }

        public void Build()
        {
            canvas.enabled = true;
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
        }

        public void Cancel()
        {
            canvas.enabled = false;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}