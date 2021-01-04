using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using MEC;

namespace DD2.UI
{
    public class DamageNumber : FloatingUI
    {
        [SerializeField] TextMeshProUGUI text;
        [SerializeField] float delay;
        [SerializeField] float duration;
        [SerializeField] float speed;
        [SerializeField] float positionVariance;

        CoroutineHandle animationHandle;

        Vector3 worldPosition;
        float xVariance;
        float yVariance;
        Vector3 offset;

        public void Show(float difference, Vector3 position)
        {
            text.text = difference.ToString();
            worldPosition = position;
            offset = Vector3.zero;

            transform.position = GetPosition();
            if (transform.position.z > 0)
            {
                CanvasGroup.alpha = 1;
            }
            else
            {
                CanvasGroup.alpha = 0;
            }

            Timing.KillCoroutines(animationHandle);
            animationHandle = Timing.RunCoroutine(AnimationRoutine());
        }

        void OnDestroy()
        {
            Timing.KillCoroutines(animationHandle);
        }

        IEnumerator<float> AnimationRoutine()
        {
            xVariance = Random.Range(-positionVariance, positionVariance);
            yVariance = Random.Range(-positionVariance, positionVariance);

            float showTime = 0;
            while (showTime < delay)
            {
                transform.position = GetPosition();
                if (transform.position.z > 0)
                {
                    CanvasGroup.alpha = 1;
                }
                else
                {
                    CanvasGroup.alpha = 0;
                }
                yield return Timing.WaitForOneFrame;
                showTime += Time.deltaTime;
            }

            float fadeTime = 0;
            while (fadeTime < duration)
            {
                offset += new Vector3(0, -speed * Time.deltaTime, 0);
                transform.position = GetPosition();
                if (transform.position.z > 0)
                {
                    float alpha = 1 - (fadeTime / duration);
                    CanvasGroup.alpha = alpha;
                }
                else
                {
                    CanvasGroup.alpha = 0;
                }                
                yield return Timing.WaitForOneFrame;
                fadeTime += Time.deltaTime;
            }

            CanvasGroup.alpha = 0;
        }

        Vector3 GetPosition()
        {
            Vector3 projectPos = Camera.main.WorldToScreenPoint(worldPosition);
            Vector3 newPosition = new Vector3(projectPos.x + xVariance, projectPos.y + yVariance, projectPos.z) + offset;

            return newPosition;
        }
    }
}
