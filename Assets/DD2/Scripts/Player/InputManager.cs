using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DD2
{
    public class InputManager : Singleton<InputManager>
    {
        public InputActions Actions { get; private set; }

        protected override void Awake()
        {
            base.Awake();

            Actions = new InputActions();
        }

        void Start()
        {
            Actions.Standard.Enable();
            Actions.Menu.Enable();
            Actions.DevConsole.Enable();
        }
    }
}
