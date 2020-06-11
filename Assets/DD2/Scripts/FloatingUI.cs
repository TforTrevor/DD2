using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace DD2.UI
{
    public class FloatingUI : MonoBehaviour
    {
        [SerializeField] Canvas canvas;
        [SerializeField] float heightOffset = 2;

        public Canvas Canvas { get => canvas; private set => canvas = value; }
        public float HeightOffset { get => heightOffset; set => heightOffset = value; }
    }
}