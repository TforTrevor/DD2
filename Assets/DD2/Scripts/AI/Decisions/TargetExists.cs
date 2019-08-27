using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DD2.AI.Decisions
{
    [CreateAssetMenu(menuName = "Scriptable Objects/AI/Decisions/Target Exists")]
    public class TargetExists : Decision
    {
        public override bool Decide(StateController controller)
        {
            if (controller.status.target != null)
            {
                return true;
            }
            return false;
        }
    }
}