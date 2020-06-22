using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using DD2.SOArchitecture;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] Vector3Variable moveVector;
    [SerializeField] Vector3Variable lookVector;
    [SerializeField] GameEvent jump;
    [SerializeField] GameEvent secondaryFire;
    [SerializeField] UnityAtoms.BaseAtoms.BoolVariable primaryFire;
    [SerializeField] GameEvent menu;
    [SerializeField] GameEvent ready;
    [SerializeField] GameEvent ability1;
    [SerializeField] GameEvent ability2;
    [SerializeField] GameEvent buildTower;
    [SerializeField] GameEvent repairTower;
    [SerializeField] GameEvent sellTower;

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
        jump.Raise();
    }

    void OnAbility1()
    {
        ability1?.Raise();
    }

    void OnAbility2()
    {
        ability2?.Raise();
    }

    void OnSecondaryFire()
    {
        secondaryFire?.Raise();
    }

    void OnPrimaryFire(InputValue value)
    {
        primaryFire.Value = value.isPressed;
        //primaryFire?.Raise();
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
}
