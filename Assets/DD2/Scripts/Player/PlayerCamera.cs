using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DD2.SOArchitecture;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] Vector3Variable lookInput;
    [SerializeField] float sensitivity;
    [SerializeField] float maxRot;
    [SerializeField] float minRot;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        transform.Rotate(Vector3.right, -lookInput.Value.y * sensitivity * Time.deltaTime);
    }
}
