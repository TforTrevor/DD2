using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityAtoms.BaseAtoms;

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

        public void Play()
        {
            introDirector.Play();
            enableInput.Value = false;
            fading = false;
        }

        public void Finished()
        {
            introFinished.Raise();
            virtualCamera.gameObject.SetActive(false);
            enableInput.Value = true;
        }

        public void StartFade()
        {
            if (!fading)
            {
                fadeDirector.Play();
                fading = true;
            }
        }
    }
}
