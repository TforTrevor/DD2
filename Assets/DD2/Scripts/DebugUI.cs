using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace DD2
{
    public class DebugUI : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI cpuTime;
        [SerializeField] TextMeshProUGUI gpuTime;

        void Update()
        {
            FrameTimingManager.CaptureFrameTimings();
            cpuTime.text = FrameTimingManager.GetCpuTimerFrequency().ToString() + "ms";
            gpuTime.text = FrameTimingManager.GetGpuTimerFrequency().ToString() + "ms";
        }
    }
}