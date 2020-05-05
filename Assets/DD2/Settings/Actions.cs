// GENERATED AUTOMATICALLY FROM 'Assets/DD2/Settings/Actions.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

namespace DD2.Input
{
    public class @Actions : IInputActionCollection, IDisposable
    {
        public InputActionAsset asset { get; }
        public @Actions()
        {
            asset = InputActionAsset.FromJson(@"{
    ""name"": ""Actions"",
    ""maps"": [
        {
            ""name"": ""Standard"",
            ""id"": ""4bd46b80-34b9-4dc7-9d1a-d2c853f8d1e7"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""bdaa640a-f715-4c48-9e05-8030772d7cb2"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Look"",
                    ""type"": ""Value"",
                    ""id"": ""34285968-dc32-477e-b229-4d17bffd0682"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""cd81f375-4d5a-4848-adbd-08e3703df000"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Ability1"",
                    ""type"": ""Button"",
                    ""id"": ""e770327e-f883-455d-b37b-73146efcd76d"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""SecondaryFire"",
                    ""type"": ""Button"",
                    ""id"": ""c206c827-8ecb-479d-a661-4388133be1bd"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""PrimaryFire"",
                    ""type"": ""Button"",
                    ""id"": ""95ee6682-b7d3-40a9-8b52-8fc72ca06a94"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Menu"",
                    ""type"": ""Button"",
                    ""id"": ""498a5b4d-94a8-42ae-bf71-9ed37e76d2e5"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Ability2"",
                    ""type"": ""Button"",
                    ""id"": ""f23f0d7e-b920-487d-bc27-28b63033e5d7"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""RepairTower"",
                    ""type"": ""Button"",
                    ""id"": ""2e0e8b46-9e4c-4a1b-a522-9e18af5bf1c0"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""2116d651-8adb-4250-9ef0-6078b4f10fc9"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""30cbf6b9-92de-4480-ad20-9f05c04c8587"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Keyboard and Mouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""98c1c78f-5272-427b-8769-594ad9fe52e8"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Keyboard and Mouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""a4943e23-5aac-4a45-9059-2de16593b041"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Keyboard and Mouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""28e72a64-284c-4be0-b7fc-36048a87cc13"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Keyboard and Mouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""4875599f-0a72-4ff2-aa6c-75f8241b6c15"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Look"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a7b14872-17a7-40f2-bf4d-e1d50aa9c1d3"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""50d9e7ee-3a69-4d9a-adb5-f31a07452759"",
                    ""path"": ""<Keyboard>/1"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Ability1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d45fc395-283d-4ca7-a9f7-74b23be53f97"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""SecondaryFire"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""91115c7d-90b0-43b6-b135-aa29386890b5"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""PrimaryFire"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a090313e-d725-457b-95f7-e2c80075ee8f"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Menu"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""fe73ab6b-341f-4c4c-8ad1-8aae5729a3b0"",
                    ""path"": ""<Keyboard>/2"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Ability2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a8196f3c-10c9-4ff1-9f72-b676c59add94"",
                    ""path"": ""<Keyboard>/r"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""RepairTower"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Keyboard and Mouse"",
            ""bindingGroup"": ""Keyboard and Mouse"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
            // Standard
            m_Standard = asset.FindActionMap("Standard", throwIfNotFound: true);
            m_Standard_Move = m_Standard.FindAction("Move", throwIfNotFound: true);
            m_Standard_Look = m_Standard.FindAction("Look", throwIfNotFound: true);
            m_Standard_Jump = m_Standard.FindAction("Jump", throwIfNotFound: true);
            m_Standard_Ability1 = m_Standard.FindAction("Ability1", throwIfNotFound: true);
            m_Standard_SecondaryFire = m_Standard.FindAction("SecondaryFire", throwIfNotFound: true);
            m_Standard_PrimaryFire = m_Standard.FindAction("PrimaryFire", throwIfNotFound: true);
            m_Standard_Menu = m_Standard.FindAction("Menu", throwIfNotFound: true);
            m_Standard_Ability2 = m_Standard.FindAction("Ability2", throwIfNotFound: true);
            m_Standard_RepairTower = m_Standard.FindAction("RepairTower", throwIfNotFound: true);
        }

        public void Dispose()
        {
            UnityEngine.Object.Destroy(asset);
        }

        public InputBinding? bindingMask
        {
            get => asset.bindingMask;
            set => asset.bindingMask = value;
        }

        public ReadOnlyArray<InputDevice>? devices
        {
            get => asset.devices;
            set => asset.devices = value;
        }

        public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

        public bool Contains(InputAction action)
        {
            return asset.Contains(action);
        }

        public IEnumerator<InputAction> GetEnumerator()
        {
            return asset.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Enable()
        {
            asset.Enable();
        }

        public void Disable()
        {
            asset.Disable();
        }

        // Standard
        private readonly InputActionMap m_Standard;
        private IStandardActions m_StandardActionsCallbackInterface;
        private readonly InputAction m_Standard_Move;
        private readonly InputAction m_Standard_Look;
        private readonly InputAction m_Standard_Jump;
        private readonly InputAction m_Standard_Ability1;
        private readonly InputAction m_Standard_SecondaryFire;
        private readonly InputAction m_Standard_PrimaryFire;
        private readonly InputAction m_Standard_Menu;
        private readonly InputAction m_Standard_Ability2;
        private readonly InputAction m_Standard_RepairTower;
        public struct StandardActions
        {
            private @Actions m_Wrapper;
            public StandardActions(@Actions wrapper) { m_Wrapper = wrapper; }
            public InputAction @Move => m_Wrapper.m_Standard_Move;
            public InputAction @Look => m_Wrapper.m_Standard_Look;
            public InputAction @Jump => m_Wrapper.m_Standard_Jump;
            public InputAction @Ability1 => m_Wrapper.m_Standard_Ability1;
            public InputAction @SecondaryFire => m_Wrapper.m_Standard_SecondaryFire;
            public InputAction @PrimaryFire => m_Wrapper.m_Standard_PrimaryFire;
            public InputAction @Menu => m_Wrapper.m_Standard_Menu;
            public InputAction @Ability2 => m_Wrapper.m_Standard_Ability2;
            public InputAction @RepairTower => m_Wrapper.m_Standard_RepairTower;
            public InputActionMap Get() { return m_Wrapper.m_Standard; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(StandardActions set) { return set.Get(); }
            public void SetCallbacks(IStandardActions instance)
            {
                if (m_Wrapper.m_StandardActionsCallbackInterface != null)
                {
                    @Move.started -= m_Wrapper.m_StandardActionsCallbackInterface.OnMove;
                    @Move.performed -= m_Wrapper.m_StandardActionsCallbackInterface.OnMove;
                    @Move.canceled -= m_Wrapper.m_StandardActionsCallbackInterface.OnMove;
                    @Look.started -= m_Wrapper.m_StandardActionsCallbackInterface.OnLook;
                    @Look.performed -= m_Wrapper.m_StandardActionsCallbackInterface.OnLook;
                    @Look.canceled -= m_Wrapper.m_StandardActionsCallbackInterface.OnLook;
                    @Jump.started -= m_Wrapper.m_StandardActionsCallbackInterface.OnJump;
                    @Jump.performed -= m_Wrapper.m_StandardActionsCallbackInterface.OnJump;
                    @Jump.canceled -= m_Wrapper.m_StandardActionsCallbackInterface.OnJump;
                    @Ability1.started -= m_Wrapper.m_StandardActionsCallbackInterface.OnAbility1;
                    @Ability1.performed -= m_Wrapper.m_StandardActionsCallbackInterface.OnAbility1;
                    @Ability1.canceled -= m_Wrapper.m_StandardActionsCallbackInterface.OnAbility1;
                    @SecondaryFire.started -= m_Wrapper.m_StandardActionsCallbackInterface.OnSecondaryFire;
                    @SecondaryFire.performed -= m_Wrapper.m_StandardActionsCallbackInterface.OnSecondaryFire;
                    @SecondaryFire.canceled -= m_Wrapper.m_StandardActionsCallbackInterface.OnSecondaryFire;
                    @PrimaryFire.started -= m_Wrapper.m_StandardActionsCallbackInterface.OnPrimaryFire;
                    @PrimaryFire.performed -= m_Wrapper.m_StandardActionsCallbackInterface.OnPrimaryFire;
                    @PrimaryFire.canceled -= m_Wrapper.m_StandardActionsCallbackInterface.OnPrimaryFire;
                    @Menu.started -= m_Wrapper.m_StandardActionsCallbackInterface.OnMenu;
                    @Menu.performed -= m_Wrapper.m_StandardActionsCallbackInterface.OnMenu;
                    @Menu.canceled -= m_Wrapper.m_StandardActionsCallbackInterface.OnMenu;
                    @Ability2.started -= m_Wrapper.m_StandardActionsCallbackInterface.OnAbility2;
                    @Ability2.performed -= m_Wrapper.m_StandardActionsCallbackInterface.OnAbility2;
                    @Ability2.canceled -= m_Wrapper.m_StandardActionsCallbackInterface.OnAbility2;
                    @RepairTower.started -= m_Wrapper.m_StandardActionsCallbackInterface.OnRepairTower;
                    @RepairTower.performed -= m_Wrapper.m_StandardActionsCallbackInterface.OnRepairTower;
                    @RepairTower.canceled -= m_Wrapper.m_StandardActionsCallbackInterface.OnRepairTower;
                }
                m_Wrapper.m_StandardActionsCallbackInterface = instance;
                if (instance != null)
                {
                    @Move.started += instance.OnMove;
                    @Move.performed += instance.OnMove;
                    @Move.canceled += instance.OnMove;
                    @Look.started += instance.OnLook;
                    @Look.performed += instance.OnLook;
                    @Look.canceled += instance.OnLook;
                    @Jump.started += instance.OnJump;
                    @Jump.performed += instance.OnJump;
                    @Jump.canceled += instance.OnJump;
                    @Ability1.started += instance.OnAbility1;
                    @Ability1.performed += instance.OnAbility1;
                    @Ability1.canceled += instance.OnAbility1;
                    @SecondaryFire.started += instance.OnSecondaryFire;
                    @SecondaryFire.performed += instance.OnSecondaryFire;
                    @SecondaryFire.canceled += instance.OnSecondaryFire;
                    @PrimaryFire.started += instance.OnPrimaryFire;
                    @PrimaryFire.performed += instance.OnPrimaryFire;
                    @PrimaryFire.canceled += instance.OnPrimaryFire;
                    @Menu.started += instance.OnMenu;
                    @Menu.performed += instance.OnMenu;
                    @Menu.canceled += instance.OnMenu;
                    @Ability2.started += instance.OnAbility2;
                    @Ability2.performed += instance.OnAbility2;
                    @Ability2.canceled += instance.OnAbility2;
                    @RepairTower.started += instance.OnRepairTower;
                    @RepairTower.performed += instance.OnRepairTower;
                    @RepairTower.canceled += instance.OnRepairTower;
                }
            }
        }
        public StandardActions @Standard => new StandardActions(this);
        private int m_KeyboardandMouseSchemeIndex = -1;
        public InputControlScheme KeyboardandMouseScheme
        {
            get
            {
                if (m_KeyboardandMouseSchemeIndex == -1) m_KeyboardandMouseSchemeIndex = asset.FindControlSchemeIndex("Keyboard and Mouse");
                return asset.controlSchemes[m_KeyboardandMouseSchemeIndex];
            }
        }
        public interface IStandardActions
        {
            void OnMove(InputAction.CallbackContext context);
            void OnLook(InputAction.CallbackContext context);
            void OnJump(InputAction.CallbackContext context);
            void OnAbility1(InputAction.CallbackContext context);
            void OnSecondaryFire(InputAction.CallbackContext context);
            void OnPrimaryFire(InputAction.CallbackContext context);
            void OnMenu(InputAction.CallbackContext context);
            void OnAbility2(InputAction.CallbackContext context);
            void OnRepairTower(InputAction.CallbackContext context);
        }
    }
}
