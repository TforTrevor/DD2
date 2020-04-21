using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using DD2.SOArchitecture;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] Vector3Variable moveVector;
    [SerializeField] Vector3Variable lookVector;
    [SerializeField] GameEvent buildTower;
    [SerializeField] GameEvent confirmBuild;
    [SerializeField] GameEvent jumpEvent;
    [SerializeField] GameEvent ability1;
    [SerializeField] GameEvent secondaryFire;
    [SerializeField] GameEvent primaryFire;

    void OnMove(InputValue value)
    {
        moveVector.Value = value.Get<Vector2>();
    }

    void OnLook(InputValue value)
    {
        lookVector.Value = value.Get<Vector2>();
    }

    void OnBuildTower()
    {
        buildTower.Raise();
    }

    void OnConfirmBuild()
    {
        confirmBuild.Raise();
    }

    void OnJump()
    {
        jumpEvent.Raise();
    }

    void OnAbility1()
    {
        ability1.Raise();
    }

    void OnSecondaryFire()
    {
        secondaryFire.Raise();
    }

    void OnPrimaryFire()
    {
        primaryFire.Raise();
    }
}
