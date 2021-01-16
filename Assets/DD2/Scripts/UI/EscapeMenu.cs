using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityAtoms.BaseAtoms;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityAtoms.SceneMgmt;
using UnityEngine.UI;

namespace DD2.UI
{
    public class EscapeMenu : Menu
    {
        [SerializeField] BoolVariable playerInput;
        [SerializeField] Menu settingsMenu;
        [SerializeField] VoidEvent exitLevel;
        [SerializeField] SceneFieldVariable hubScene;
        [SerializeField] Button exitLevelButton;

        protected override void Start()
        {
            base.Start();
            inputActions.FindActionMap("Menu").FindAction("Cancel").performed += Toggle;

            if (SceneManager.GetActiveScene().buildIndex == hubScene.Value.BuildIndex)
            {
                exitLevelButton.gameObject.SetActive(false);
            }
        }

        public void Toggle(InputAction.CallbackContext context)
        {
            if (isOpen)
            {
                Hide();
            }
            else
            {
                Show();
            }
        }

        public override void Show(Menu menu)
        {
            if (!isOpen)
            {
                canvas.enabled = true;
                if (menu != null)
                {
                    previousMenu = menu;
                }
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                Time.timeScale = 0;
                playerInput.Value = false;

                isOpen = true;
            }          
        }

        public override void Hide()
        {
            if (isOpen)
            {
                canvas.enabled = false;
                previousMenu = null;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                Time.timeScale = 1;
                playerInput.Value = true;

                isOpen = false;
            }
        }

        public void Resume()
        {
            Hide();
        }

        public void OpenSettings()
        {
            canvas.enabled = false;
            settingsMenu.Show(this);
        }

        public void ExitLevel()
        {
            exitLevel.Raise();
        }

        public void ExitGame()
        {
            Application.Quit();
        }

        void OnDestroy()
        {
            inputActions.FindActionMap("Menu").FindAction("Cancel").performed -= Toggle;
        }
    }
}
