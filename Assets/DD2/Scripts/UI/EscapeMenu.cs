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

            if (SceneManager.GetActiveScene().buildIndex == hubScene.Value.BuildIndex)
            {
                exitLevelButton.gameObject.SetActive(false);
            }
        }

        void OnEnable()
        {
            InputManager.Instance.Actions.Menu.Toggle.performed += Toggle;
        }

        void OnDisable()
        {
            InputManager.Instance.Actions.Menu.Toggle.performed -= Toggle;
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
                TimeManager.Pause();
                playerInput.Value = false;
                InputManager.Instance.DisableInput(InputManager.Instance.Actions.Player);

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
                TimeManager.UnPause();
                playerInput.Value = true;
                InputManager.Instance.EnableInput(InputManager.Instance.Actions.Player);

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
    }
}
