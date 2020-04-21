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
                    ""name"": ""BuildTower"",
                    ""type"": ""Button"",
                    ""id"": ""cce43130-1ba2-4bb2-97bb-22fe1ccb46d7"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ConfirmBuild"",
                    ""type"": ""Button"",
                    ""id"": ""9b2529f8-8161-4bb8-ade3-9a6a9b94de03"",
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
                    ""id"": ""4b928016-5683-41cd-8a52-2ba45eaa187f"",
                    ""path"": ""<Keyboard>/b"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""BuildTower"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2c17d20d-25d5-46ae-b43f-71c71b56640b"",
                    ""path"": ""<Keyboard>/c"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""ConfirmBuild"",
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
                    ""groups"": """",
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
                    ""groups"": """",
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
                    ""groups"": """",
                    ""action"": ""PrimaryFire"",
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
            m_Standard_BuildTower = m_Standard.FindAction("BuildTower", throwIfNotFound: true);
            m_Standard_ConfirmBuild = m_Standard.FindAction("ConfirmBuild", throwIfNotFound: true);
            m_Standard_Jump = m_Standard.FindAction("Jump", throwIfNotFound: true);
            m_Standard_Ability1 = m_Standard.FindAction("Ability1", throwIfNotFound: true);
            m_Standard_SecondaryFire = m_Standard.FindAction("SecondaryFire", throwIfNotFound: true);
            m_Standard_PrimaryFire = m_Standard.FindAction("PrimaryFire", throwIfNotFound: true);
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
        private readonly InputAction m_Standard_BuildTower;
        private readonly InputAction m_Standard_ConfirmBuild;
        private readonly InputAction m_Standard_Jump;
        private readonly InputAction m_Standard_Ability1;
        private readonly InputAction m_Standard_SecondaryFire;
        private readonly InputAction m_Standard_PrimaryFire;
        public struct StandardActions
        {
            private @Actions m_Wrapper;
            public StandardActions(@Actions wrapper) { m_Wrapper = wrapper; }
            public InputAction @Move => m_Wrapper.m_Standard_Move;
            public InputAction @Look => m_Wrapper.m_Standard_Look;
            public InputAction @BuildTower => m_Wrapper.m_Standard_BuildTower;
            public InputAction @ConfirmBuild => m_Wrapper.m_Standard_ConfirmBuild;
            public InputAction @Jump => m_Wrapper.m_Standard_Jump;
            public InputAction @Ability1 => m_Wrapper.m_Standard_Ability1;
            public InputAction @SecondaryFire => m_Wrapper.m_Standard_SecondaryFire;
            public InputAction @PrimaryFire => m_Wrapper.m_Standard_PrimaryFire;
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
                    @BuildTower.started -= m_Wrapper.m_StandardActionsCallbackInterface.OnBuildTower;
                    @BuildTower.performed -= m_Wrapper.m_StandardActionsCallbackInterface.OnBuildTower;
                    @BuildTower.canceled -= m_Wrapper.m_StandardActionsCallbackInterface.OnBuildTower;
                    @ConfirmBuild.started -= m_Wrapper.m_StandardActionsCallbackInterface.OnConfirmBuild;
                    @ConfirmBuild.performed -= m_Wrapper.m_StandardActionsCallbackInterface.OnConfirmBuild;
                    @ConfirmBuild.canceled -= m_Wrapper.m_StandardActionsCallbackInterface.OnConfirmBuild;
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
                    @BuildTower.started += instance.OnBuildTower;
                    @BuildTower.performed += instance.OnBuildTower;
                    @BuildTower.canceled += instance.OnBuildTower;
                    @ConfirmBuild.started += instance.OnConfirmBuild;
                    @ConfirmBuild.performed += instance.OnConfirmBuild;
                    @ConfirmBuild.canceled += instance.OnConfirmBuild;
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
            void OnBuildTower(InputAction.CallbackContext context);
            void OnConfirmBuild(InputAction.CallbackContext context);
            void OnJump(InputAction.CallbackContext context);
            void OnAbility1(InputAction.CallbackContext context);
            void OnSecondaryFire(InputAction.CallbackContext context);
            void OnPrimaryFire(InputAction.CallbackContext context);
        }
    }
}
