﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityAtoms.BaseAtoms;

namespace DD2
{
    public class PlayerCamera : MonoBehaviour
    {
        [SerializeField] Vector2Variable lookInput;
        [SerializeField] BoolVariable enableLook;
        [SerializeField] float sensitivity;
        [SerializeField] float maxPitch;
        [SerializeField] float minPitch;

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
            transform.parent.localEulerAngles = Vector3.zero;
            shoulderView = true;
        }

        public void BirdsEyeView()
        {
            transform.parent.localEulerAngles = new Vector3(0, 90, 0);
            shoulderView = false;
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