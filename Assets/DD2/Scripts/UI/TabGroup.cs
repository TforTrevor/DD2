using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DD2.UI
{
    public class TabGroup : MonoBehaviour
    {
        [SerializeField] List<TabButton> tabButtons;
        [SerializeField] List<Transform> tabContent;
        [SerializeField] Color selectedColor;
        [SerializeField] Color hoverColor;
        [SerializeField] Color defaultColor;

        TabButton selectedButton;

        void Awake()
        {
            foreach (TabButton button in tabButtons)
            {
                button.TabGroup = this;
            }
        }

        void Start()
        {
            foreach (Transform content in tabContent)
            {
                if (content != null)
                {
                    content.gameObject.SetActive(false);
                }                
            }
        }

        public void OnClick(TabButton button)
        {
            if (selectedButton != null)
            {
                int oldIndex = tabButtons.IndexOf(selectedButton);
                if (oldIndex < tabContent.Count)
                {
                    tabContent[oldIndex].gameObject.SetActive(false);
                }                
                selectedButton.SetColor(defaultColor);
            }
            selectedButton = button;
            int newIndex = tabButtons.IndexOf(button);
            if (newIndex < tabContent.Count)
            {
                tabContent[newIndex].gameObject.SetActive(true);
            }            
            button.SetColor(selectedColor);
        }

        public void OnSelect(TabButton button)
        {
            if (button != selectedButton)
            {
                button.SetColor(hoverColor);
            }
        }

        public void OnDeselect(TabButton button)
        {
            if (button != selectedButton)
            {
                button.SetColor(defaultColor);
            }            
        }
    }
}
