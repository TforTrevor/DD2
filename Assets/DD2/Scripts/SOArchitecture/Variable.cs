using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DD2.SOArchitecture
{
    public class Variable<T> : ScriptableObject
    {
        public T Value;
        [SerializeField] [TextArea] string description;
    }
}