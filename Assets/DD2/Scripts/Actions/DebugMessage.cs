using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RoboRyanTron.SearchableEnum;
using NaughtyAttributes;

namespace DD2.Actions
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Actions/Debug Message")]
    public class DebugMessage : Action
    {
        [SerializeField] string message;

        public override void DoAction(Entity target, Entity caller, object payload)
        {
            if (!string.IsNullOrWhiteSpace(message))
            {
                Debug.Log(message);
            }
        }
    }
}
