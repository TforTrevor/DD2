using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DD2.Ability
{
    public abstract class Effect : ScriptableObject
    {
        public abstract void ApplyEffect(Ability controller, Transform hit);
    }
}