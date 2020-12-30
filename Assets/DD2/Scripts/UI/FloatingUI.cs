using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Rendering;

namespace DD2.UI
{
    [RequireComponent(typeof(CanvasGroup), typeof(SortingGroup))]
    public class FloatingUI : MonoBehaviour
    {
        CanvasGroup canvasGroup;
        SortingGroup sortingGroup;

        public CanvasGroup CanvasGroup { get => canvasGroup; private set => canvasGroup = value; }
        public SortingGroup SortingGroup { get => sortingGroup; private set => sortingGroup = value; }

        protected virtual void Awake()
        {
            canvasGroup = GetComponent<CanvasGroup>();
            sortingGroup = GetComponent<SortingGroup>();
        }

        public virtual void Show()
        {
            canvasGroup.alpha = 1;
        }

        public virtual void Hide()
        {
            canvasGroup.alpha = 0;
        }
    }
}