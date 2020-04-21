using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DD2.Actions
{
    public abstract class Action : ScriptableObject
    {
        public abstract void DoAction(Transform target, Entity entity, Vector3 position);
        public virtual void Cancel(Transform target, Entity entity, Vector3 position)
        {

        }
    }
}