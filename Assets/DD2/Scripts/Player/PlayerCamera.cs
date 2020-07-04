using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityAtoms.BaseAtoms;
using Cinemachine;

namespace DD2
{
    public class PlayerCamera : MonoBehaviour
    {
        [SerializeField] Vector2Variable lookInput;
        [SerializeField] BoolVariable enableLook;
        [SerializeField] float sensitivity;
        [SerializeField] float maxPitch;
        [SerializeField] float minPitch;
        [SerializeField] CinemachineVirtualCamera shoulderCamera;
        [SerializeField] CinemachineVirtualCamera topCamera;

        bool shoulderView;
        float pitch;

        void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            shoulderView = true;
        }

        void Update()
        {
            if (shoulderView && enableLook.Value)
            {
                pitch -= lookInput.Value.y * sensitivity * Time.deltaTime;
                pitch = Mathf.Clamp(pitch, minPitch, maxPitch);
                transform.localEulerAngles = new Vector3(pitch, 0, 0);
            }            
        }

        public void ShoulderView()
        {
            pitch = 0;
            shoulderView = true;
            //transform.localEulerAngles = Vector3.zero;
            shoulderCamera.Priority = 10;
            topCamera.Priority = 0;
        }

        public void TopView()
        {
            //pitch = 0;
            shoulderView = false;
            //transform.localEulerAngles = new Vector3(0, 90, 0);
            //transform.localEulerAngles = Vector3.zero;
            topCamera.Priority = 10;
            shoulderCamera.Priority = 0;
            
        }

        public void ToggleCursorLock()
        {
            if (Cursor.lockState == CursorLockMode.Locked)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }
    }
}