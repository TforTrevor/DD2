using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DD2.Actions
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Actions/Cancel Action")]
    public class CancelAction : Action
    {
        public Action action;

        public override void DoAction(Entity target, Entity caller, object payload)
        {
            action.Cancel(target, caller, payload);
        }
    }
}