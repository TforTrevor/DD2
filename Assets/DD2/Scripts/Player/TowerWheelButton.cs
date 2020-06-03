using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DD2
{
    public class TowerWheelButton : MonoBehaviour
    {
        [SerializeField] Button button;
        [SerializeField] TextMeshProUGUI text;

        public Button Button { get => button; private set => button = value; }
        public TextMeshProUGUI Text { get => text; private set => text = value; }
    }
}