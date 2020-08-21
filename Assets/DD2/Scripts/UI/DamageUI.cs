using MEC;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace DD2.UI
{
    public class DamageUI : MonoBehaviour
    {
        [SerializeField] string poolKey;
        [SerializeField] TextMeshProUGUI text;
        [SerializeField] CanvasGroup canvasGroup;
        [SerializeField] float duration;
        [SerializeField] float fadeDuration;
        [SerializeField] float verticalMove;
        [SerializeField] float horizontalMove;
        [SerializeField] LeanTweenType verticalEase;
        [SerializeField] LeanTweenType horizontalEase;
        [SerializeField] LeanTweenType fadeEase;

        public string PoolKey { get => poolKey; private set => poolKey = value; }

        public void Play(float damage)
        {
            text.text = damage.ToString();
            canvasGroup.alpha = 1;
            Timing.RunCoroutine(TextRoutine());
        }

        IEnumerator<float> TextRoutine()
        {
            LeanTween.moveY(text.rectTransform, verticalMove, duration + fadeDuration).setEase(verticalEase);
            LeanTween.moveX(text.rectTransform, Random.Range(-horizontalMove, horizontalMove), duration + fadeDuration).setEase(horizontalEase);
            yield return Timing.WaitForSeconds(duration);
            LeanTween.alphaCanvas(canvasGroup, 0, fadeDuration).setEase(fadeEase);
            yield return Timing.WaitForSeconds(fadeDuration);
            DamageUICanvas.Instance.Return(this);
        }
    }
}