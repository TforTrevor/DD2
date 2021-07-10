
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DD2.UI
{
    public class ProgressBar : MonoBehaviour
    {
        [SerializeField] int minimum;
        [SerializeField] int maximum;
        [SerializeField] int foregroundCurrent;
        [SerializeField] int backgroundCurrent;
        [SerializeField] RectTransform foregroundTransform;
        [SerializeField] RectMask2D foregroundMask;
        [SerializeField] RectTransform backgroundTransform;
        [SerializeField] RectMask2D backgroundMask;
        [SerializeField] Image foregroundFill;
        [SerializeField] Image backgroundFill;
        [SerializeField] Color foregroundColor;
        [SerializeField] Color backgroundColor;

        public int Minimum { get => minimum; set => minimum = value; }
        public int Maximum { get => maximum; set => maximum = value; }
        public int ForegroundCurrent { get => foregroundCurrent; private set => foregroundCurrent = value; }
        public int BackgroundCurrent { get => backgroundCurrent; private set => backgroundCurrent = value; }

        void Start()
        {
            UpdateColor();
        }

        public void SetForegroundValue(int value)
        {
            foregroundCurrent = value;
            float currentOffset = foregroundCurrent - minimum;
            float maximumOffset = maximum - minimum;
            float fillAmount = currentOffset / maximumOffset;
            foregroundMask.padding = new Vector4(0, foregroundTransform.rect.width * fillAmount, 0, 0);
            UpdateColor();
        }

        public void SetBackgroundValue(int value)
        {
            backgroundCurrent = value;
            float currentOffset = backgroundCurrent - minimum;
            float maximumOffset = maximum - minimum;
            float fillAmount = currentOffset / maximumOffset;
            backgroundMask.padding = new Vector4(0, backgroundTransform.rect.width * fillAmount, 0, 0);
            UpdateColor();
        }

        public void UpdateColor()
        {
            foregroundFill.color = foregroundColor;
            backgroundFill.color = backgroundColor;
        }
    }
}