using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DD2.AI.Actions
{
    [CreateAssetMenu(menuName = "Scriptable Objects/AI/Actions/Use Ability")]
    public class UseAbility : Action
    {
        public override void Act(StateController controller)
        {
            controller.status.GetAbility(0).UseAbility(controller.status.target.position);
        }
    }
}