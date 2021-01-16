using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace DD2.UI
{
    public class TabButton : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] Image image;

        TabGroup tabGroup;

        public TabGroup TabGroup { get => tabGroup; set => tabGroup = value; }

        public void OnPointerClick(PointerEventData eventData)
        {
            tabGroup.OnClick(this);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            tabGroup.OnSelect(this);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            tabGroup.OnDeselect(this);
        }

        public void SetColor(Color color)
        {
            image.color = color;
        }
    }
}
