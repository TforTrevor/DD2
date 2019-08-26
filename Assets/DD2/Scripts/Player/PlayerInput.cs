using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using SmartData.SmartVector3;
using SmartData.SmartEvent;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] Vector3Writer moveVector;
    [SerializeField] Vector3Writer lookVector;
    [SerializeField] EventDispatcher buildTower;
    [SerializeField] EventDispatcher confirmBuild;

    void OnMove(InputValue value)
    {
        moveVector.value = value.Get<Vector2>();
    }

    void OnLook(InputValue value)
    {
        lookVector.value = value.Get<Vector2>();
    }

    void OnBuildTower()
    {
        buildTower.Dispatch();
    }

    void OnConfirmBuild()
    {
        confirmBuild.Dispatch();
    }
}
