using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityAtoms.BaseAtoms;
using UnityEngine.InputSystem;

namespace DD2
{
    public class LevelIntro : MonoBehaviour
    {
        [SerializeField] PlayableDirector introDirector;
        [SerializeField] PlayableDirector fadeDirector;
        [SerializeField] VoidEvent introFinished;
        [SerializeField] Transform virtualCamera;
        [SerializeField] BoolVariable enableInput;

        bool fading;

        void OnEnable()
        {
            InputManager.Instance.Actions.Menu.Toggle.performed += StartFade;
        }

        void OnDisable()
        {
            InputManager.Instance.Actions.Menu.Toggle.performed -= StartFade;
        }

        public void Play()
        {
            introDirector.Play();
            enableInput.Value = false;
            fading = false;
            //InputManager.Instance.DisableInput(InputManager.Instance.Actions.Player);
        }

        public void Finished()
        {
            introFinished.Raise();
            virtualCamera.gameObject.SetActive(false);
            enableInput.Value = true;
            //InputManager.Instance.EnableInput(InputManager.Instance.Actions.Player);
        }

        public void StartFade(InputAction.CallbackContext context)
        {
            if (!fading)
            {
                fadeDirector.Play();
                fading = true;
            }
        }
    }
}
