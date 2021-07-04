using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;
using MEC;

namespace DD2
{
    public class DevConsole : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI entityName;
        [SerializeField] TextMeshProUGUI consoleText;
        [SerializeField] TMP_InputField consoleInput;
        [SerializeField] InputActionAsset inputAsset;
        [SerializeField] LayerMask entityMask;
        [SerializeField] ScrollRect scrollRect;

        Entity selectedEntity;
        Canvas canvas;
        List<ConsoleCommandBase> consoleCommands = new List<ConsoleCommandBase>();

        void Awake()
        {
            canvas = GetComponent<Canvas>();
        }

        void Start()
        {
            canvas.enabled = false;

            AddConsoleCommands();
        }

        void OnEnable()
        {
            InputManager.Instance.Actions.DevConsole.Toggle.performed += Toggle;
            InputManager.Instance.Actions.DevConsole.Select.performed += SelectEntity;
        }

        void OnDisable()
        {
            InputManager.Instance.Actions.DevConsole.Toggle.performed -= Toggle;
            InputManager.Instance.Actions.DevConsole.Select.performed -= SelectEntity;
        }

        public void Toggle(InputAction.CallbackContext context)
        {
            if (!canvas.enabled)
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                TimeManager.Pause();
                canvas.enabled = true;

                InputManager.Instance.DisableInput(InputManager.Instance.Actions.Player);
            }
            else
            {
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Confined;
                TimeManager.UnPause();
                canvas.enabled = false;

                InputManager.Instance.EnableInput(InputManager.Instance.Actions.Player);
            }
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

        public void ParseCommand(string input)
        {
            if (input.Length > 0)
            {
                Print(input);
                consoleInput.text = "";
                consoleInput.Select();
                consoleInput.ActivateInputField();

                string[] properties = input.Split(' ');

                foreach (ConsoleCommandBase command in consoleCommands)
                {
                    if (input.Contains(command.Id))
                    {
                        if (command as ConsoleCommand != null)
                        {
                            (command as ConsoleCommand).Invoke();
                        }
                        else if (command as ConsoleCommand<int> != null)
                        {
                            (command as ConsoleCommand<int>).Invoke(int.Parse(properties[1]));
                        }
                        else if (command as ConsoleCommand<float> != null)
                        {
                            (command as ConsoleCommand<float>).Invoke(int.Parse(properties[1]));
                        }
                    }
                }

                Timing.CallDelayed(0, () =>
                {
                    scrollRect.verticalNormalizedPosition = 0;
                });
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

        void AddConsoleCommands()
        {
            consoleCommands.Add(new ConsoleCommand<float>("damage", "Damages the selected entity", "damage <amount>", (x) =>
            {
                selectedEntity.Damage(null, x);
            }));

            consoleCommands.Add(new ConsoleCommand<int>("heal", "Heals the selected entity", "heal <amount>", (x) =>
            {
                selectedEntity.Heal(null, x);
            }));

            consoleCommands.Add(new ConsoleCommand("getHealth", "Gets the health of the selected entity", "getHealth", () =>
            {
                Print(selectedEntity.name + " health: " + selectedEntity.CurrentHealth);
            }));

            consoleCommands.Add(new ConsoleCommand("help", "Prints all commands", "help", () =>
            {
                foreach (ConsoleCommandBase command in consoleCommands)
                {
                    Print(command.Format + " - " + command.Description);
                }
            }));
        }
    }
}
