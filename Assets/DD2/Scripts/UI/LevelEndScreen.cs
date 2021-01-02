using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityAtoms.BaseAtoms;

namespace DD2.UI
{
    public class LevelEndScreen : MonoBehaviour
    {
        [SerializeField] Transform endScreen;
        [SerializeField] VoidEvent closeEndScreen;

        public void Start()
        {
            endScreen.gameObject.SetActive(false);
        }

        public void Show()
        {
            endScreen.gameObject.SetActive(true);
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
        }

        public void Close()
        {
            endScreen.gameObject.SetActive(false);
            closeEndScreen.Raise();
        }
    }
}
