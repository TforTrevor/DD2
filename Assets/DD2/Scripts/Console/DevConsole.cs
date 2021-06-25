using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

namespace DD2
{
    public class DevConsole : Singleton<DevConsole>
    {
        [SerializeField] TextMeshProUGUI entityName;
        [SerializeField] TextMeshProUGUI consoleText;
        [SerializeField] TMP_InputField consoleInput;
        [SerializeField] InputActionAsset inputAsset;
        [SerializeField] LayerMask entityMask;
        [SerializeField] ScrollRect scrollRect;

        Entity selectedEntity;
        Canvas canvas;

        void Start()
        {
            enabled = false;
        }

        void OnEnable()
        {
            inputAsset.FindActionMap("Standard").FindAction("Shoot").performed += SelectEntity;
            TimeManager.Pause();
            canvas.enabled = true;
        }

        void OnDisable()
        {
            inputAsset.FindActionMap("Standard").FindAction("Shoot").performed -= SelectEntity;
            TimeManager.UnPause();
            canvas.enabled = false;
        }

        public void Toggle()
        {
            enabled = !enabled;
        }

        public void Print(string message)
        {
            if (consoleText.text.Length > 0)
            {
                consoleText.text += "\n" + message;
            }
            else
            {
                consoleText.text += message;
            }
        }

        public void SelectEntity(InputAction.CallbackContext context)
        {
            Vector2 mousePos = Mouse.current.position.ReadValue();

            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(mousePos);
            if (Physics.Raycast(ray, out hit, float.PositiveInfinity, entityMask))
            {
                Entity entity = hit.transform.GetComponent<Entity>();
                if (entity != null)
                {
                    selectedEntity = entity;
                    entityName.text = entity.name;
                }
            }
        }
    }
}
