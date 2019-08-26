using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SmartData.SmartVector3;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] Vector3Reader lookInput;
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
        transform.Rotate(Vector3.right, -lookInput.value.y * sensitivity * Time.deltaTime);
    }
}
