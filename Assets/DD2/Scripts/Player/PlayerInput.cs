using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityAtoms.BaseAtoms;

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

    void OnMove(InputValue value)
    {
        if (enableInput.Value)
            moveVector.Value = value.Get<Vector2>();
        else
            moveVector.Value = Vector2.zero;
    }

    void OnLook(InputValue value)
    {
        if (enableInput.Value)
            lookVector.Value = value.Get<Vector2>();
        else
            lookVector.Value = Vector2.zero;
    }

    void OnJump(InputValue value)
    {
        if (enableInput.Value)
            jump.Value = value.isPressed;
        else
            jump.Value = false;
    }

    void OnPrimaryFire(InputValue value)
    {
        if (enableInput.Value)
            primaryFire.Value = value.isPressed;
        else
            primaryFire.Value = false;
    }

    void OnSecondaryFire(InputValue value)
    {
        if (enableInput.Value)
            secondaryFire.Value = value.isPressed;
        else
            secondaryFire.Value = false;
    }

    void OnAbility1(InputValue value)
    {
        if (enableInput.Value)
            ability1.Value = value.isPressed;
        else
            ability1.Value = false;
    }

    void OnAbility2(InputValue value)
    {
        if (enableInput.Value)
            ability2.Value = value.isPressed;
        else
            ability2.Value = false;
    }

    void OnMenu()
    {
        menu?.Raise();
    }

    void OnRepairTower()
    {
        if (enableInput.Value)
            repairTower?.Raise();
    }

    void OnReady()
    {
        if (enableInput.Value)
            ready?.Raise();
    }

    void OnBuildTower()
    {
        if (enableInput.Value)
            buildTower?.Raise();
    }

    void OnSellTower()
    {
        if (enableInput.Value)
            sellTower?.Raise();
    }

    void OnUpgradeTower()
    {
        if (enableInput.Value)
            upgradeTower?.Raise();
    }
}
