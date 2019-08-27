using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DD2.AI.Actions
{
    [CreateAssetMenu(menuName = "Scriptable Objects/AI/Actions/Move To Target")]
    public class MoveToTarget : Action
    {
        public override void Act(StateController controller)
        {
            Vector3 direction = Vector3.Normalize(controller.status.target.position - controller.transform.position);
            float distance = Vector3.Distance(controller.status.target.position, controller.transform.position) - controller.status.stats.GetAttackRange();
            Vector3 position = (direction * distance) + controller.transform.position;
            controller.status.navMeshAgent.SetDestination(position);
        }
    }
}