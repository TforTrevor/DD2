using UnityEngine;
using UnityEngine.Events;
using NaughtyAttributes;
using System.Collections.Generic;

namespace DD2.SOArchitecture
{
    public class GameEventListener : MonoBehaviour
    {
        [ReorderableList] public List<GameEventListenerContainer> gameEvents;

        void OnEnable()
        {
            foreach (GameEventListenerContainer container in gameEvents)
            {
                container.Event.RegisterListener(this);
            }
            //Event.RegisterListener(this);
        }

        void OnDisable()
        {
            foreach (GameEventListenerContainer container in gameEvents)
            {
                container.Event.UnregisterListener(this);
            }
            //Event.UnregisterListener(this);
        }

        public void OnEventRaised(GameEvent gameEvent)
        {
            foreach (GameEventListenerContainer container in gameEvents)
            {
                if (gameEvent == container.Event)
                {
                    container.Response.Invoke();
                }
            }
            //Response.Invoke();
        }
    }
}