using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace DD2.UI
{
    public class FloatingUICanvas<T> : MonoBehaviour where T : FloatingUI
    {
        [SerializeField] T uiElement;
        [SerializeField] int maxElements;

        T[] inactiveElements;
        T[] activeElements;
        int activeCount;

        public int MaxElements { get => maxElements; }

        void Awake()
        {
            inactiveElements = new T[maxElements];
            activeElements = new T[maxElements];

            for (int i = 0; i < maxElements; i++)
            {
                T temp = Instantiate(uiElement, transform);
                temp.Hide();
                inactiveElements[i] = temp;
            }
        }

        public virtual T GetElement()
        {
            T element;
            //Take oldest active element (front of queue) and send it to the back of the active queue
            if (activeCount == maxElements)
            {
                element = activeElements[0];
                for (int i = 0; i < activeCount - 1; i++)
                {
                    activeElements[i] = activeElements[i + 1];
                }
                activeElements[activeCount - 1] = element;
            }
            //Take inactive element and send it to the back of the active queue
            else
            {
                element = inactiveElements[0];
                for (int i = 0; i < maxElements - activeCount - 1; i++)
                {
                    inactiveElements[i] = inactiveElements[i + 1];
                }
                inactiveElements[maxElements - activeCount - 1] = null;
                //inactiveElements[activeCount] = null;
                activeElements[activeCount] = element;
                activeCount++;
            }

            return element;
        }

        public virtual void ReturnElement(T element)
        {
            if (activeCount > 0)
            {
                inactiveElements[maxElements - activeCount] = element;
                activeElements[activeCount - 1] = null;
                activeCount--;
            }
        }
    }
}