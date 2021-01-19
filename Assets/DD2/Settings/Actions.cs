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
                    ""name"": ""PrimaryFire"",
                    ""type"": ""Button"",
                    ""id"": ""95ee6682-b7d3-40a9-8b52-8fc72ca06a94"",
                    ""expectedControlType"": ""Button"",
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
                    ""name"": ""Ability1"",
                    ""type"": ""Button"",
                    ""id"": ""e770327e-f883-455d-b37b-73146efcd76d"",
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
                    ""name"": ""Menu"",
                    ""type"": ""Button"",
                    ""id"": ""498a5b4d-94a8-42ae-bf71-9ed37e76d2e5"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Ready"",
                    ""type"": ""Button"",
                    ""id"": ""f6b9770b-cdb8-4a5b-80e1-33b2d516a9e8"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""BuildTower"",
                    ""type"": ""Button"",
                    ""id"": ""aed07601-49e3-4925-a46c-cede2ef1b67f"",
                    ""expectedControlType"": ""Button"",
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
                },
                {
                    ""name"": ""SellTower"",
                    ""type"": ""Button"",
                    ""id"": ""6d4c44b7-37f4-4a28-8c2e-f1a4d1f3fb2a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""UpgradeTower"",
                    ""type"": ""Button"",
                    ""id"": ""520f21c1-0f01-4883-9e0c-07e144503b67"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ShowTowerRange"",
                    ""type"": ""Value"",
                    ""id"": ""11a7de43-68eb-4081-9722-5640a1502425"",
                    ""expectedControlType"": ""Button"",
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
                    ""interactions"": ""Press(behavior=2)"",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d45fc395-283d-4ca7-a9f7-74b23be53f97"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": ""Press(behavior=2)"",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""SecondaryFire"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a090313e-d725-457b-95f7-e2c80075ee8f"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Menu"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9dfe5875-e80b-4a7c-bab2-cc6a201f2c15"",
                    ""path"": ""<Keyboard>/g"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Ready"",
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
                },
                {
                    ""name"": """",
                    ""id"": ""922f3f92-2433-478c-a97b-9f8673ff383b"",
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
                    ""id"": ""93a69c23-2bc8-479c-b5ac-0f81bec673f1"",
                    ""path"": ""<Keyboard>/x"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""SellTower"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""91115c7d-90b0-43b6-b135-aa29386890b5"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": ""Press(behavior=2)"",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""PrimaryFire"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""50d9e7ee-3a69-4d9a-adb5-f31a07452759"",
                    ""path"": ""<Keyboard>/1"",
                    ""interactions"": ""Press(behavior=2)"",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Ability1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""fe73ab6b-341f-4c4c-8ad1-8aae5729a3b0"",
                    ""path"": ""<Keyboard>/2"",
                    ""interactions"": ""Press(behavior=2)"",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Ability2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1630a0a0-f22c-4d13-991f-d9f13f769e78"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""UpgradeTower"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c0861abe-a4c1-4e62-99b6-538be3661d01"",
                    ""path"": ""<Keyboard>/leftShift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""ShowTowerRange"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Menu"",
            ""id"": ""c48bb213-1696-4614-99f9-4944359c0635"",
            ""actions"": [
                {
                    ""name"": ""Cancel"",
                    ""type"": ""Button"",
                    ""id"": ""ecd19eb3-fae9-45cf-bfe6-acd7413bcea3"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Point"",
                    ""type"": ""Value"",
                    ""id"": ""64fb3c36-1e07-4172-b815-10231f813724"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Left Click"",
                    ""type"": ""Button"",
                    ""id"": ""17a9b946-073b-4600-a1be-0320c682ca2a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""2df58991-a278-4856-ac42-151e8a1506de"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Cancel"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""32759208-cfaa-4cd4-817e-ab8060a95d11"",
                    ""path"": ""<Pointer>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Point"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f7845033-f963-4384-b433-7e4470fa5729"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Left Click"",
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
            m_Standard_PrimaryFire = m_Standard.FindAction("PrimaryFire", throwIfNotFound: true);
            m_Standard_SecondaryFire = m_Standard.FindAction("SecondaryFire", throwIfNotFound: true);
            m_Standard_Ability1 = m_Standard.FindAction("Ability1", throwIfNotFound: true);
            m_Standard_Ability2 = m_Standard.FindAction("Ability2", throwIfNotFound: true);
            m_Standard_Menu = m_Standard.FindAction("Menu", throwIfNotFound: true);
            m_Standard_Ready = m_Standard.FindAction("Ready", throwIfNotFound: true);
            m_Standard_BuildTower = m_Standard.FindAction("BuildTower", throwIfNotFound: true);
            m_Standard_RepairTower = m_Standard.FindAction("RepairTower", throwIfNotFound: true);
            m_Standard_SellTower = m_Standard.FindAction("SellTower", throwIfNotFound: true);
            m_Standard_UpgradeTower = m_Standard.FindAction("UpgradeTower", throwIfNotFound: true);
            m_Standard_ShowTowerRange = m_Standard.FindAction("ShowTowerRange", throwIfNotFound: true);
            // Menu
            m_Menu = asset.FindActionMap("Menu", throwIfNotFound: true);
            m_Menu_Cancel = m_Menu.FindAction("Cancel", throwIfNotFound: true);
            m_Menu_Point = m_Menu.FindAction("Point", throwIfNotFound: true);
            m_Menu_LeftClick = m_Menu.FindAction("Left Click", throwIfNotFound: true);
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
        private readonly InputAction m_Standard_PrimaryFire;
        private readonly InputAction m_Standard_SecondaryFire;
        private readonly InputAction m_Standard_Ability1;
        private readonly InputAction m_Standard_Ability2;
        private readonly InputAction m_Standard_Menu;
        private readonly InputAction m_Standard_Ready;
        private readonly InputAction m_Standard_BuildTower;
        private readonly InputAction m_Standard_RepairTower;
        private readonly InputAction m_Standard_SellTower;
        private readonly InputAction m_Standard_UpgradeTower;
        private readonly InputAction m_Standard_ShowTowerRange;
        public struct StandardActions
        {
            private @Actions m_Wrapper;
            public StandardActions(@Actions wrapper) { m_Wrapper = wrapper; }
            public InputAction @Move => m_Wrapper.m_Standard_Move;
            public InputAction @Look => m_Wrapper.m_Standard_Look;
            public InputAction @Jump => m_Wrapper.m_Standard_Jump;
            public InputAction @PrimaryFire => m_Wrapper.m_Standard_PrimaryFire;
            public InputAction @SecondaryFire => m_Wrapper.m_Standard_SecondaryFire;
            public InputAction @Ability1 => m_Wrapper.m_Standard_Ability1;
            public InputAction @Ability2 => m_Wrapper.m_Standard_Ability2;
            public InputAction @Menu => m_Wrapper.m_Standard_Menu;
            public InputAction @Ready => m_Wrapper.m_Standard_Ready;
            public InputAction @BuildTower => m_Wrapper.m_Standard_BuildTower;
            public InputAction @RepairTower => m_Wrapper.m_Standard_RepairTower;
            public InputAction @SellTower => m_Wrapper.m_Standard_SellTower;
            public InputAction @UpgradeTower => m_Wrapper.m_Standard_UpgradeTower;
            public InputAction @ShowTowerRange => m_Wrapper.m_Standard_ShowTowerRange;
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
                    @PrimaryFire.started -= m_Wrapper.m_StandardActionsCallbackInterface.OnPrimaryFire;
                    @PrimaryFire.performed -= m_Wrapper.m_StandardActionsCallbackInterface.OnPrimaryFire;
                    @PrimaryFire.canceled -= m_Wrapper.m_StandardActionsCallbackInterface.OnPrimaryFire;
                    @SecondaryFire.started -= m_Wrapper.m_StandardActionsCallbackInterface.OnSecondaryFire;
                    @SecondaryFire.performed -= m_Wrapper.m_StandardActionsCallbackInterface.OnSecondaryFire;
                    @SecondaryFire.canceled -= m_Wrapper.m_StandardActionsCallbackInterface.OnSecondaryFire;
                    @Ability1.started -= m_Wrapper.m_StandardActionsCallbackInterface.OnAbility1;
                    @Ability1.performed -= m_Wrapper.m_StandardActionsCallbackInterface.OnAbility1;
                    @Ability1.canceled -= m_Wrapper.m_StandardActionsCallbackInterface.OnAbility1;
                    @Ability2.started -= m_Wrapper.m_StandardActionsCallbackInterface.OnAbility2;
                    @Ability2.performed -= m_Wrapper.m_StandardActionsCallbackInterface.OnAbility2;
                    @Ability2.canceled -= m_Wrapper.m_StandardActionsCallbackInterface.OnAbility2;
                    @Menu.started -= m_Wrapper.m_StandardActionsCallbackInterface.OnMenu;
                    @Menu.performed -= m_Wrapper.m_StandardActionsCallbackInterface.OnMenu;
                    @Menu.canceled -= m_Wrapper.m_StandardActionsCallbackInterface.OnMenu;
                    @Ready.started -= m_Wrapper.m_StandardActionsCallbackInterface.OnReady;
                    @Ready.performed -= m_Wrapper.m_StandardActionsCallbackInterface.OnReady;
                    @Ready.canceled -= m_Wrapper.m_StandardActionsCallbackInterface.OnReady;
                    @BuildTower.started -= m_Wrapper.m_StandardActionsCallbackInterface.OnBuildTower;
                    @BuildTower.performed -= m_Wrapper.m_StandardActionsCallbackInterface.OnBuildTower;
                    @BuildTower.canceled -= m_Wrapper.m_StandardActionsCallbackInterface.OnBuildTower;
                    @RepairTower.started -= m_Wrapper.m_StandardActionsCallbackInterface.OnRepairTower;
                    @RepairTower.performed -= m_Wrapper.m_StandardActionsCallbackInterface.OnRepairTower;
                    @RepairTower.canceled -= m_Wrapper.m_StandardActionsCallbackInterface.OnRepairTower;
                    @SellTower.started -= m_Wrapper.m_StandardActionsCallbackInterface.OnSellTower;
                    @SellTower.performed -= m_Wrapper.m_StandardActionsCallbackInterface.OnSellTower;
                    @SellTower.canceled -= m_Wrapper.m_StandardActionsCallbackInterface.OnSellTower;
                    @UpgradeTower.started -= m_Wrapper.m_StandardActionsCallbackInterface.OnUpgradeTower;
                    @UpgradeTower.performed -= m_Wrapper.m_StandardActionsCallbackInterface.OnUpgradeTower;
                    @UpgradeTower.canceled -= m_Wrapper.m_StandardActionsCallbackInterface.OnUpgradeTower;
                    @ShowTowerRange.started -= m_Wrapper.m_StandardActionsCallbackInterface.OnShowTowerRange;
                    @ShowTowerRange.performed -= m_Wrapper.m_StandardActionsCallbackInterface.OnShowTowerRange;
                    @ShowTowerRange.canceled -= m_Wrapper.m_StandardActionsCallbackInterface.OnShowTowerRange;
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
                    @PrimaryFire.started += instance.OnPrimaryFire;
                    @PrimaryFire.performed += instance.OnPrimaryFire;
                    @PrimaryFire.canceled += instance.OnPrimaryFire;
                    @SecondaryFire.started += instance.OnSecondaryFire;
                    @SecondaryFire.performed += instance.OnSecondaryFire;
                    @SecondaryFire.canceled += instance.OnSecondaryFire;
                    @Ability1.started += instance.OnAbility1;
                    @Ability1.performed += instance.OnAbility1;
                    @Ability1.canceled += instance.OnAbility1;
                    @Ability2.started += instance.OnAbility2;
                    @Ability2.performed += instance.OnAbility2;
                    @Ability2.canceled += instance.OnAbility2;
                    @Menu.started += instance.OnMenu;
                    @Menu.performed += instance.OnMenu;
                    @Menu.canceled += instance.OnMenu;
                    @Ready.started += instance.OnReady;
                    @Ready.performed += instance.OnReady;
                    @Ready.canceled += instance.OnReady;
                    @BuildTower.started += instance.OnBuildTower;
                    @BuildTower.performed += instance.OnBuildTower;
                    @BuildTower.canceled += instance.OnBuildTower;
                    @RepairTower.started += instance.OnRepairTower;
                    @RepairTower.performed += instance.OnRepairTower;
                    @RepairTower.canceled += instance.OnRepairTower;
                    @SellTower.started += instance.OnSellTower;
                    @SellTower.performed += instance.OnSellTower;
                    @SellTower.canceled += instance.OnSellTower;
                    @UpgradeTower.started += instance.OnUpgradeTower;
                    @UpgradeTower.performed += instance.OnUpgradeTower;
                    @UpgradeTower.canceled += instance.OnUpgradeTower;
                    @ShowTowerRange.started += instance.OnShowTowerRange;
                    @ShowTowerRange.performed += instance.OnShowTowerRange;
                    @ShowTowerRange.canceled += instance.OnShowTowerRange;
                }
            }
        }
        public StandardActions @Standard => new StandardActions(this);

        // Menu
        private readonly InputActionMap m_Menu;
        private IMenuActions m_MenuActionsCallbackInterface;
        private readonly InputAction m_Menu_Cancel;
        private readonly InputAction m_Menu_Point;
        private readonly InputAction m_Menu_LeftClick;
        public struct MenuActions
        {
            private @Actions m_Wrapper;
            public MenuActions(@Actions wrapper) { m_Wrapper = wrapper; }
            public InputAction @Cancel => m_Wrapper.m_Menu_Cancel;
            public InputAction @Point => m_Wrapper.m_Menu_Point;
            public InputAction @LeftClick => m_Wrapper.m_Menu_LeftClick;
            public InputActionMap Get() { return m_Wrapper.m_Menu; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(MenuActions set) { return set.Get(); }
            public void SetCallbacks(IMenuActions instance)
            {
                if (m_Wrapper.m_MenuActionsCallbackInterface != null)
                {
                    @Cancel.started -= m_Wrapper.m_MenuActionsCallbackInterface.OnCancel;
                    @Cancel.performed -= m_Wrapper.m_MenuActionsCallbackInterface.OnCancel;
                    @Cancel.canceled -= m_Wrapper.m_MenuActionsCallbackInterface.OnCancel;
                    @Point.started -= m_Wrapper.m_MenuActionsCallbackInterface.OnPoint;
                    @Point.performed -= m_Wrapper.m_MenuActionsCallbackInterface.OnPoint;
                    @Point.canceled -= m_Wrapper.m_MenuActionsCallbackInterface.OnPoint;
                    @LeftClick.started -= m_Wrapper.m_MenuActionsCallbackInterface.OnLeftClick;
                    @LeftClick.performed -= m_Wrapper.m_MenuActionsCallbackInterface.OnLeftClick;
                    @LeftClick.canceled -= m_Wrapper.m_MenuActionsCallbackInterface.OnLeftClick;
                }
                m_Wrapper.m_MenuActionsCallbackInterface = instance;
                if (instance != null)
                {
                    @Cancel.started += instance.OnCancel;
                    @Cancel.performed += instance.OnCancel;
                    @Cancel.canceled += instance.OnCancel;
                    @Point.started += instance.OnPoint;
                    @Point.performed += instance.OnPoint;
                    @Point.canceled += instance.OnPoint;
                    @LeftClick.started += instance.OnLeftClick;
                    @LeftClick.performed += instance.OnLeftClick;
                    @LeftClick.canceled += instance.OnLeftClick;
                }
            }
        }
        public MenuActions @Menu => new MenuActions(this);
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
            void OnPrimaryFire(InputAction.CallbackContext context);
            void OnSecondaryFire(InputAction.CallbackContext context);
            void OnAbility1(InputAction.CallbackContext context);
            void OnAbility2(InputAction.CallbackContext context);
            void OnMenu(InputAction.CallbackContext context);
            void OnReady(InputAction.CallbackContext context);
            void OnBuildTower(InputAction.CallbackContext context);
            void OnRepairTower(InputAction.CallbackContext context);
            void OnSellTower(InputAction.CallbackContext context);
            void OnUpgradeTower(InputAction.CallbackContext context);
            void OnShowTowerRange(InputAction.CallbackContext context);
        }
        public interface IMenuActions
        {
            void OnCancel(InputAction.CallbackContext context);
            void OnPoint(InputAction.CallbackContext context);
            void OnLeftClick(InputAction.CallbackContext context);
        }
    }
}
