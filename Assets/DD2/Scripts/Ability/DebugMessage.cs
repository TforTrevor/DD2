using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RoboRyanTron.SearchableEnum;
using NaughtyAttributes;

namespace DD2.Abilities.Effects
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Abilities/Effects/Debug Message")]
    public class DebugMessage : Effect
    {
        [SerializeField] string message;

        public override void ApplyEffect(Transform target)
        {
            if (!string.IsNullOrWhiteSpace(message))
            {
                Debug.Log(message);
            }
        }
    }
}
