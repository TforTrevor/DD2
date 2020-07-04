using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityAtoms.BaseAtoms;

public class PlayerInput : MonoBehaviour
{
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
        moveVector.Value = value.Get<Vector2>();
    }

    void OnLook(InputValue value)
    {
        lookVector.Value = value.Get<Vector2>();
    }

    void OnJump(InputValue value)
    {
        jump.Value = value.isPressed;
    }

    void OnPrimaryFire(InputValue value)
    {
        primaryFire.Value = value.isPressed;
    }

    void OnSecondaryFire(InputValue value)
    {
        secondaryFire.Value = value.isPressed;
    }

    void OnAbility1(InputValue value)
    {
        ability1.Value = value.isPressed;
    }

    void OnAbility2(InputValue value)
    {
        ability2.Value = value.isPressed;
    }

    void OnMenu()
    {
        menu?.Raise();
    }

    void OnRepairTower()
    {
        repairTower?.Raise();
    }

    void OnReady()
    {
        ready?.Raise();
    }

    void OnBuildTower()
    {
        buildTower?.Raise();
    }

    void OnSellTower()
    {
        sellTower?.Raise();
    }

    void OnUpgradeTower()
    {
        upgradeTower?.Raise();
    }
}
