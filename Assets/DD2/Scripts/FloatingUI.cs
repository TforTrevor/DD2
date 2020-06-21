using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Rendering;

namespace DD2.UI
{
    public class FloatingUI : MonoBehaviour
    {
        [SerializeField] CanvasGroup canvasGroup;
        [SerializeField] SortingGroup sortingGroup;
        [SerializeField] float heightOffset = 2;

        public CanvasGroup CanvasGroup { get => canvasGroup; private set => canvasGroup = value; }
        public SortingGroup SortingGroup { get => sortingGroup; private set => sortingGroup = value; }
        public float HeightOffset { get => heightOffset; set => heightOffset = value; }        

        public virtual void ToggleCanvas(bool value)
        {
            ToggleCanvas(value, null);
        }

        public virtual void ToggleCanvas(bool value, System.Action onComplete)
        {
            if (value)
            {
                canvasGroup.alpha = 1;
            }
            else
            {
                canvasGroup.alpha = 0;
            }
            onComplete?.Invoke();
        }
    }
}