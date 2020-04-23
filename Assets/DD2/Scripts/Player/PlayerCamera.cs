using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DD2.SOArchitecture;

namespace DD2
{
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
    }
}