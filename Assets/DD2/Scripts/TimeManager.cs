using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DD2
{
    public static class TimeManager
    {
        static float previousTimeScale = 1;
        static int pauseCounter = 0;

        public static void SetTimeScale(float timeScale)
        {
            Time.timeScale = timeScale;
            previousTimeScale = timeScale;
        }

        public static void Pause()
        {
            if (pauseCounter <= 0)
            {
                previousTimeScale = Time.timeScale;
                Time.timeScale = 0;
            }
            pauseCounter++;
        }

        public static void UnPause()
        {
            pauseCounter--;
            if (pauseCounter <= 0)
            {
                Time.timeScale = previousTimeScale;
                pauseCounter = 0;
            }
        }
    }
}
