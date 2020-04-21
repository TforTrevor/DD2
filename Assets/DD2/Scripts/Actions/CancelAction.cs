using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DD2.Actions
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Actions/Cancel Action")]
    public class CancelAction : Action
    {
        public Action action;

        public override void DoAction(Transform target, Entity entity, Vector3 position)
        {
            action.Cancel(target, entity, position);
        }
    }
}