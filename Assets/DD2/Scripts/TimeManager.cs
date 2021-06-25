using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DD2
{
    public static class TimeManager
    {
        static float previousTimeScale = 1;

        public static void SetTimeScale(float timeScale)
        {
            Time.timeScale = timeScale;
            previousTimeScale = timeScale;
        }

        public static void Pause()
        {
            previousTimeScale = Time.timeScale;
            Time.timeScale = 0;
        }

        public static void UnPause()
        {
            Time.timeScale = previousTimeScale;
        }
    }
}
