using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace DD2
{
    public class InputManager : Singleton<InputManager>
    {
        public InputActions Actions { get; private set; }

        Dictionary<InputActionMap, int> disabledInputs = new Dictionary<InputActionMap, int>();

        protected override void Awake()
        {
            base.Awake();

            Actions = new InputActions();
        }

        void Start()
        {
            Actions.Player.Enable();
            Actions.Menu.Enable();
            Actions.DevConsole.Enable();
        }

        public void EnableInput(InputActionMap map)
        {
            if (disabledInputs.ContainsKey(map))
            {
                disabledInputs[map] = disabledInputs[map] - 1;
                if (disabledInputs[map] <= 0)
                {
                    disabledInputs.Remove(map);
                    map.Enable();
                }
            }            
        }

        public void DisableInput(InputActionMap map)
        {
            if (!disabledInputs.ContainsKey(map))
            {
                disabledInputs.Add(map, 1);
                map.Disable();
            }
            else
            {
                disabledInputs[map] = disabledInputs[map] + 1;
            }            
        }
    }
}
