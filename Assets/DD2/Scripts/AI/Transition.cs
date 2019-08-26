using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

namespace DD2.AI
{
    [System.Serializable]
    public class Transition
    {
        public Decision decision;
        [Expandable] [Required] public State trueState;
        [Expandable] [Required] public State falseState;
    }
}
