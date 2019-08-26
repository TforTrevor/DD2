using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DD2.AI
{
    public abstract class Decision : ScriptableObject
    {
        public abstract bool Decide(StateController controller);
    }
}

