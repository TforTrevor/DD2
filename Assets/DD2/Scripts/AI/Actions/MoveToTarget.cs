using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Apex.AI;
using Apex.Serialization;
using DD2.AI.Context;

namespace DD2.AI.Actions
{
    public class MoveToTarget : ActionBase
    {
        [ApexSerialization] bool ensureMaxRange;
        public override void Execute(IAIContext context)
        {
            AIContext c = (AIContext)context;

            if (c.target)
            {
                float distance = Vector3.Distance(c.target.GetPosition(), c.entity.GetPosition());
                if (distance > c.entity.GetAttackRange() || ensureMaxRange)
                {
                    Vector3 direction = Vector3.Normalize(c.target.GetPosition() - c.entity.GetPosition());
                    float distanceFromRange = distance - c.entity.GetAttackRange();
                    Vector3 position = (direction * distanceFromRange) + c.entity.GetPosition();
                    c.entity.navMeshAgent.SetDestination(position);
                }
            }
        }
    }
}