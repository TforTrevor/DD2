using DD2.AI;

using RoboRyanTron.SearchableEnum;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

namespace DD2
{
    public class TowerWheel : MonoBehaviour
    {
        [SerializeField] BuildTower buildTower;
        [SerializeField] TowerWheelButton towerButton;
        [SerializeField] Transform parent;
        [SerializeField]  List<Tower> towerPrefabs;

        Canvas canvas;
        List<TowerWheelButton> towerButtons = new List<TowerWheelButton>();

        void Awake()
        {
            canvas = GetComponent<Canvas>();
        }

        void OnEnable()
        {
            InputManager.Instance.Actions.Player.BuildTower.performed += Build;
            InputManager.Instance.Actions.Player.SecondaryFire.performed += Cancel;
        }

        void OnDisable()
        {
            InputManager.Instance.Actions.Player.BuildTower.performed -= Build;
            InputManager.Instance.Actions.Player.SecondaryFire.performed -= Cancel;
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

        public void Build(InputAction.CallbackContext context)
        {
            canvas.enabled = true;
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
        }

        public void Cancel(InputAction.CallbackContext context)
        {
            canvas.enabled = false;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}