using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace DD2.UI
{
    public class VideoSettings : MonoBehaviour
    {
        [SerializeField] TMP_Dropdown vsyncDropdown;
        [SerializeField] TMP_InputField fpsInput;

        public void SetVsync(int value)
        {
            switch (value)
            {
                //Vsync off
                case 0:
                    QualitySettings.vSyncCount = 0;
                    fpsInput.enabled = true;
                    break;
                //Double buffered
                case 1:
                    QualitySettings.vSyncCount = 1;
                    QualitySettings.maxQueuedFrames = 1;
                    fpsInput.enabled = false;
                    break;
                //Triple buffered
                case 2:
                    QualitySettings.vSyncCount = 1;
                    QualitySettings.maxQueuedFrames = 2;
                    fpsInput.enabled = false;
                    break;
            }
        }

        public void SetFPSCap(string value)
        {
            int fps;
            bool parsed = int.TryParse(value, out fps);
            if (parsed && QualitySettings.vSyncCount == 0)
            {
                fps = Mathf.Clamp(fps, 15, 300);
                Application.targetFrameRate = fps;
                fpsInput.text = fps.ToString();
            }            
        }
    }
}
