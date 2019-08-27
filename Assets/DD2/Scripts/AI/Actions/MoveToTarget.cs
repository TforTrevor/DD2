using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DD2.AI.Actions
{
    [CreateAssetMenu(menuName = "Scriptable Objects/AI/Actions/Move To Target")]
    public class MoveToTarget : Action
    {
        [SerializeField] bool ensureMaxRange;
        public override void Act(StateController controller)
        {
            if (controller.status.target != null)
            {
                float distance = Vector3.Distance(controller.status.target.position, controller.transform.position);
                if (distance > controller.status.stats.GetAttackRange() || ensureMaxRange)
                {
                    Vector3 direction = Vector3.Normalize(controller.status.target.position - controller.transform.position);
                    float distanceFromRange = distance - controller.status.stats.GetAttackRange();
                    Vector3 position = (direction * distanceFromRange) + controller.transform.position;
                    controller.status.navMeshAgent.SetDestination(position);
                }
            }
        }
    }
}