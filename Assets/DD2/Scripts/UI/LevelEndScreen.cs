using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityAtoms.BaseAtoms;

namespace DD2.UI
{
    public class LevelEndScreen : MonoBehaviour
    {
        [SerializeField] Transform winScreen;
        [SerializeField] Transform loseScreen;
        [SerializeField] VoidEvent closeEndScreen;
        [SerializeField] BoolVariable enableInput;

        public void Start()
        {
            winScreen.gameObject.SetActive(false);
            loseScreen.gameObject.SetActive(false);
        }

        public void Show(bool value)
        {
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
            enableInput.Value = false;
            if (value)
            {
                winScreen.gameObject.SetActive(true);
            }
            else
            {
                loseScreen.gameObject.SetActive(true);
            }
        }

        public void Close()
        {
            winScreen.gameObject.SetActive(false);
            loseScreen.gameObject.SetActive(false);
            closeEndScreen.Raise();
        }
    }
}
