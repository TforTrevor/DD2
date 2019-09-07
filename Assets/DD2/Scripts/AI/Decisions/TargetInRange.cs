using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DD2.AI.Decisions
{
    [CreateAssetMenu(menuName = "Scriptable Objects/AI/Decisions/Target In Range")]
    public class TargetInRange : Decision
    {
        public override bool Decide(StateController controller)
        {
            float enemyDistance = Vector3.Distance(controller.status.GetPosition(), controller.status.target.position);
            if (enemyDistance > controller.status.stats.GetSearchRange())
            {
                return false;
            }
            float enemyDot = Vector3.Dot(controller.status.GetForward(), Vector3.Normalize(controller.status.target.position - controller.status.GetPosition()));
            float desiredDot = Mathf.Cos((Mathf.Deg2Rad * controller.status.stats.GetSearchAngle()) / 2f);
            if (enemyDot < desiredDot)
            {
                return false;
            }
            return true;
        }
    }
}