using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace DD2.SOArchitecture
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Variables/Game Event")]
    public class GameEvent : ScriptableObject
    {
        private readonly List<GameEventListener> eventListeners = new List<GameEventListener>();
        [SerializeField] [TextArea] string description;

        public void Raise()
        {
            for (int i = 0; i < eventListeners.Count; i++)
            {
                eventListeners[i].OnEventRaised(this);
            }
        }

        public void RegisterListener(GameEventListener listener)
        {
            if (!eventListeners.Contains(listener))
            {
                eventListeners.Add(listener);
            }
        }

        public void UnregisterListener(GameEventListener listener)
        {
            if (eventListeners.Contains(listener))
            {
                eventListeners.Remove(listener);
            }
        }
    }
}