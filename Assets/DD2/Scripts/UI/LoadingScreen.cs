using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MEC;
using TMPro;
using UnityEngine.UI;
using UnityAtoms.BaseAtoms;

namespace DD2.UI
{
    public class LoadingScreen : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI progressText;
        [SerializeField] Slider progressSlider;
        [SerializeField] VoidEvent levelLoaded;

        public void Show(AsyncOperation asyncLoad)
        {
            Timing.RunCoroutine(LoadRoutine(asyncLoad));
        }

        IEnumerator<float> LoadRoutine(AsyncOperation asyncLoad)
        {
            bool doneLoading = false;
            while (!doneLoading)
            {
                if (asyncLoad.progress == 1)
                {
                    doneLoading = true;
                }

                progressText.text = asyncLoad.progress * 100 + "%";
                progressSlider.value = asyncLoad.progress;

                yield return Timing.WaitForOneFrame;
            }
            yield return Timing.WaitForOneFrame;
            levelLoaded.Raise();
            Hide();
        }

        public void Hide()
        {
            Destroy(gameObject);
        }
    }
}
