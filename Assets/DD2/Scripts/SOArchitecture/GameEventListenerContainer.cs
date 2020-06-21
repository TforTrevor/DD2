using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace DD2.SOArchitecture 
{
    [System.Serializable]
    public class GameEventListenerContainer
    {
        public GameEvent Event;
        public UnityEvent Response;
    }
}

