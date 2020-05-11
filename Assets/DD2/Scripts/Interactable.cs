using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DD2.SOArchitecture;

namespace DD2
{
    public class Interactable : MonoBehaviour
    {
        [SerializeField] UnityEvent<Entity> unityEvent;
        [SerializeField] GameEvent gameEvent;

        public void Interact(Entity entity)
        {
            unityEvent?.Invoke(entity);
            gameEvent?.Raise();
        }
    }
}