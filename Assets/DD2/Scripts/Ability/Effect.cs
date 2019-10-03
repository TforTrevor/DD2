using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DD2.Abilities
{
    public abstract class Effect : ScriptableObject
    {
        public abstract void ApplyEffect(Transform target, Status status, Vector3 position);
    }
}