﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using DD2.SOArchitecture;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] Vector3Variable moveVector;
    [SerializeField] Vector3Variable lookVector;
    [SerializeField] GameEvent ability1;
    [SerializeField] GameEvent ability2;
    [SerializeField] GameEvent secondaryFire;
    [SerializeField] GameEvent primaryFire;
    [SerializeField] GameEvent menu;
    [SerializeField] GameEvent repairTower;
    [SerializeField] GameEvent ready;

    void OnMove(InputValue value)
    {
        moveVector.Value = value.Get<Vector2>();
    }

    void OnLook(InputValue value)
    {
        lookVector.Value = value.Get<Vector2>();
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

    void OnPrimaryFire()
    {
        primaryFire?.Raise();
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
}
