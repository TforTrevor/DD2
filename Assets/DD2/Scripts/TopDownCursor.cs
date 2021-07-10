using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityAtoms.BaseAtoms;
using UnityEngine;
using UnityEngine.VFX;
using MEC;
using UnityEngine.InputSystem;

namespace DD2
{
    public class TopDownCursor : MonoBehaviour
    {
        [SerializeField] protected Player player;
        [SerializeField] protected Transform cursor;
        [SerializeField] float sensitivity;
        [SerializeField] float maxRange;
        [SerializeField] LayerMask cursorMask;
        [SerializeField] VoidEvent shoulderView;
        [SerializeField] VoidEvent topView;
        [SerializeField] Transform rangeIndicator;
        [SerializeField] VisualEffect cursorEffect;
        [SerializeField] Light cursorLight;
        [SerializeField] BoolVariable enableUse;

        bool toggleState;
        Vector3 localPos;
        CoroutineHandle effectHandle;
        LTDescr lightHandle;
        float lightIntensity;
        Renderer rangeRenderer;
        protected Vector2 lookInput;
        CoroutineHandle moveHandle;

        void Awake()
        {
            rangeRenderer = rangeIndicator.GetComponent<Renderer>();
        }

        protected virtual void Start()
        {
            toggleState = false;
            cursor.gameObject.SetActive(false);
            rangeIndicator.gameObject.SetActive(false);
            lightIntensity = cursorLight.intensity;
        }

        protected virtual void OnEnable()
        {
            
            
            
        }

        protected virtual void OnDisable()
        {
            
        }

        void OnLook(InputAction.CallbackContext context)
        {
            lookInput = context.ReadValue<Vector2>();
        }

        void OnContinue(InputAction.CallbackContext context)
        {
            if (context.ReadValueAsButton())
            {
                Continue();
            }
        }

        void OnCancel(InputAction.CallbackContext context)
        {
            if (context.ReadValueAsButton())
            {
                Cancel();
            }
        }

        protected virtual void Begin()
        {
            InputManager.Instance.Actions.Player.Look.performed += OnLook;
            InputManager.Instance.Actions.Player.PrimaryFire.performed += OnContinue;
            InputManager.Instance.Actions.Player.SecondaryFire.performed += OnCancel;

            topView.Raise();
            player.ToggleLook(false);
            
            cursor.gameObject.SetActive(true);
            rangeIndicator.gameObject.SetActive(true);
            rangeRenderer.material.color = cursorLight.color;

            moveHandle = Timing.RunCoroutine(MoveRoutine());
        }

        protected virtual void Continue()
        {

        }

        protected virtual void Cancel()
        {
            InputManager.Instance.Actions.Player.Look.performed -= OnLook;
            InputManager.Instance.Actions.Player.PrimaryFire.performed -= OnContinue;
            InputManager.Instance.Actions.Player.SecondaryFire.performed -= OnCancel;

            shoulderView.Raise();
            player.ToggleLook(true);

            cursor.gameObject.SetActive(false);
            rangeIndicator.gameObject.SetActive(false);

            Timing.KillCoroutines(moveHandle);
        }
        
        IEnumerator<float> MoveRoutine()
        {
            localPos = Vector3.zero;

            while (true)
            {
                Vector3 direction = new Vector3(lookInput.x * sensitivity * Time.deltaTime, 0, lookInput.y * sensitivity * Time.deltaTime);
                localPos += direction;
                localPos = Vector3.ClampMagnitude(localPos, maxRange);
                Vector3 worldPos = player.transform.TransformPoint(localPos);

                RaycastHit hit;
                if (Physics.Raycast(new Vector3(cursor.position.x, Camera.main.transform.position.y, cursor.position.z), Vector3.down, out hit, 1000, cursorMask))
                {
                    worldPos.y = hit.point.y;
                }

                cursor.transform.position = worldPos;

                yield return Timing.WaitForOneFrame;
            }
        }

        protected void OnDestroy()
        {
            InputManager.Instance.Actions.Player.Look.performed -= OnLook;
            InputManager.Instance.Actions.Player.PrimaryFire.performed -= OnContinue;
            InputManager.Instance.Actions.Player.SecondaryFire.performed -= OnCancel;
        }

        //protected virtual void Begin()
        //{
        //    topView.Raise();
        //    cursor.transform.position = player.transform.position;
        //    localPos = Vector3.zero;
        //    cursor.gameObject.SetActive(true);
        //    rangeIndicator.gameObject.SetActive(true);
        //    player.ToggleLook(false);

        //    Timing.KillCoroutines(effectHandle);
        //    //cursorEffect.Play();
        //    cursorEffect.Stop();
        //    if (lightHandle != null)
        //        LeanTween.cancel(lightHandle.uniqueId);
        //    cursorLight.intensity = lightIntensity;
        //    rangeRenderer.material.color = cursorLight.color;
        //}

        //public virtual void Cancel()
        //{
        //    if (toggleState)
        //    {
        //        toggleState = false;
        //        shoulderView.Raise();
        //        rangeIndicator.gameObject.SetActive(false);
        //        player.ToggleLook(true);
        //        cursorEffect.Stop();
        //        lightIntensity = cursorLight.intensity;
        //        lightHandle = LeanTween.value(cursorLight.intensity, 0, Mathf.Pow(cursorEffect.GetFloat("Lifetime"), 2));
        //        lightHandle.setOnUpdate((value) => cursorLight.intensity = value);
        //        effectHandle = Timing.CallDelayed(Mathf.Pow(cursorEffect.GetFloat("Lifetime"), 2), () => cursor.gameObject.SetActive(false));
        //    }
        //}

        //void FixedUpdate()
        //{
        //    if (toggleState)
        //    {
        //        Vector3 direction = new Vector3(lookInput.x * sensitivity * Time.deltaTime, 0, lookInput.y * sensitivity * Time.deltaTime);
        //        localPos += direction;
        //        localPos = Vector3.ClampMagnitude(localPos, maxRange);
        //        RaycastHit hit;
        //        Vector3 worldPos = player.transform.TransformPoint(localPos);
        //        if (Physics.Raycast(new Vector3(cursor.position.x, Camera.main.transform.position.y, cursor.position.z), Vector3.down, out hit, 1000, cursorMask))
        //        {
        //            worldPos.y = hit.point.y;
        //        }
        //        cursor.MovePosition(worldPos);
        //    }
        //}
    }
}