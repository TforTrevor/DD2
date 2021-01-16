using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace DD2.UI
{
    [RequireComponent(typeof(Canvas))]
    public class Menu : MonoBehaviour
    {
        [SerializeField] protected InputActionAsset inputActions;

        protected Menu previousMenu;
        protected Canvas canvas;
        protected bool isOpen;

        void Awake()
        {
            canvas = GetComponent<Canvas>();
        }

        protected virtual void Start()
        {
            isOpen = true;
            Hide();
        }

        public void Show()
        {
            Show(null);
        }

        public virtual void Show(Menu menu)
        {
            if (!isOpen)
            {
                canvas.enabled = true;
                if (menu != null)
                {
                    previousMenu = menu;
                }
                inputActions.FindActionMap("Menu").FindAction("Cancel").performed += OnCancel;

                isOpen = true;
            }            
        }

        public virtual void Hide()
        {
            if (isOpen)
            {
                canvas.enabled = false;
                previousMenu = null;
                inputActions.FindActionMap("Menu").FindAction("Cancel").performed -= OnCancel;

                isOpen = false;
            }
        }

        public void OnCancel(InputAction.CallbackContext context)
        {
            if (previousMenu != null)
            {
                previousMenu.Show(null);
            }
            Hide();
        }
    }
}
