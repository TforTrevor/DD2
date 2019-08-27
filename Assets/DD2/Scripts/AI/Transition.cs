using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DD2.AI
{
    [System.Serializable]
    public class Transition
    {
        public Decision decision;
        [Expandable] public State trueState;
        [Expandable] public State falseState;
    }
}
