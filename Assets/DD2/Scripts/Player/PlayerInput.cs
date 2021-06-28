using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityAtoms.BaseAtoms;

namespace DD2
{
    public class PlayerInput : MonoBehaviour
    {
        [SerializeField] BoolVariable enableInput;
        [SerializeField] Vector2Variable moveVector;
        [SerializeField] Vector2Variable lookVector;
        [SerializeField] BoolVariable jump;
        [SerializeField] BoolVariable primaryFire;
        [SerializeField] BoolVariable secondaryFire;
        [SerializeField] BoolVariable ability1;
        [SerializeField] BoolVariable ability2;
        [SerializeField] VoidEvent buildTower;
        [SerializeField] VoidEvent repairTower;
        [SerializeField] VoidEvent sellTower;
        [SerializeField] VoidEvent upgradeTower;
        [SerializeField] VoidEvent menu;
        [SerializeField] VoidEvent ready;
        [SerializeField] BoolEvent toggleTowerRange;

        void OnEnable()
        {
            InputManager.Instance.Actions.Player.Move.performed += Move;
            InputManager.Instance.Actions.Player.Look.performed += Look;
            InputManager.Instance.Actions.Player.Jump.performed += Jump;
            InputManager.Instance.Actions.Player.PrimaryFire.performed += PrimaryFire;
            InputManager.Instance.Actions.Player.SecondaryFire.performed += SecondaryFire;
            InputManager.Instance.Actions.Player.Ability1.performed += Ability1;
            InputManager.Instance.Actions.Player.Ability2.performed += Ability2;
            InputManager.Instance.Actions.Player.Ready.performed += Ready;
            InputManager.Instance.Actions.Player.BuildTower.performed += BuildTower;
            InputManager.Instance.Actions.Player.RepairTower.performed += RepairTower;
            InputManager.Instance.Actions.Player.SellTower.performed += SellTower;
            InputManager.Instance.Actions.Player.UpgradeTower.performed += UpgradeTower;
            InputManager.Instance.Actions.Player.ShowTowerRange.performed += ShowTowerRange;
        }

        void OnDisable()
        {
            InputManager.Instance.Actions.Player.Move.performed -= Move;
            InputManager.Instance.Actions.Player.Look.performed -= Look;
            InputManager.Instance.Actions.Player.Jump.performed -= Jump;
            InputManager.Instance.Actions.Player.PrimaryFire.performed -= PrimaryFire;
            InputManager.Instance.Actions.Player.SecondaryFire.performed -= SecondaryFire;
            InputManager.Instance.Actions.Player.Ability1.performed -= Ability1;
            InputManager.Instance.Actions.Player.Ability2.performed -= Ability2;
            InputManager.Instance.Actions.Player.Ready.performed -= Ready;
            InputManager.Instance.Actions.Player.BuildTower.performed -= BuildTower;
            InputManager.Instance.Actions.Player.RepairTower.performed -= RepairTower;
            InputManager.Instance.Actions.Player.SellTower.performed -= SellTower;
            InputManager.Instance.Actions.Player.UpgradeTower.performed -= UpgradeTower;
            InputManager.Instance.Actions.Player.ShowTowerRange.performed -= ShowTowerRange;
        }

        void Move(InputAction.CallbackContext context)
        {
            moveVector.Value = context.ReadValue<Vector2>();
        }

        void Look(InputAction.CallbackContext context)
        {
            lookVector.Value = context.ReadValue<Vector2>();
        }

        void Jump(InputAction.CallbackContext context)
        {
            jump.Value = context.ReadValueAsButton();
        }

        void PrimaryFire(InputAction.CallbackContext context)
        {
            primaryFire.Value = context.ReadValueAsButton();
        }

        void SecondaryFire(InputAction.CallbackContext context)
        {
            secondaryFire.Value = context.ReadValueAsButton();
        }

        void Ability1(InputAction.CallbackContext context)
        {
            ability1.Value = context.ReadValueAsButton();
        }

        void Ability2(InputAction.CallbackContext context)
        {
            ability2.Value = context.ReadValueAsButton();
        }

        void Ready(InputAction.CallbackContext context)
        {
            ready?.Raise();
        }

        void BuildTower(InputAction.CallbackContext context)
        {
            buildTower?.Raise();
        }

        void RepairTower(InputAction.CallbackContext context)
        {
            repairTower?.Raise();
        }

        void SellTower(InputAction.CallbackContext context)
        {
            sellTower?.Raise();
        }

        void UpgradeTower(InputAction.CallbackContext context)
        {
            upgradeTower?.Raise();
        }

        void ShowTowerRange(InputAction.CallbackContext context)
        {
            toggleTowerRange?.Raise(context.ReadValueAsButton());
        }
    }

}