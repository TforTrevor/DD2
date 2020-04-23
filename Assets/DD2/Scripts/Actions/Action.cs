using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DD2.Actions
{
    public abstract class Action : ScriptableObject
    {
        public void DoAction(Entity target, Entity caller)
        {
            DoAction(target, caller, null);
        }
        public abstract void DoAction(Entity target, Entity caller, object payload);
        public virtual void Cancel(Entity target, Entity caller, object payload)
        {

        }
    }
}