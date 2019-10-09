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
            EnemyContext c = (EnemyContext)context;

            if (c.target)
            {
                float distance = Vector3.Distance(c.target.GetPosition(), c.enemy.GetPosition());
                if (distance > c.enemy.GetAttackRange() || ensureMaxRange)
                {
                    Vector3 direction = Vector3.Normalize(c.target.GetPosition() - c.enemy.GetPosition());
                    float distanceFromRange = distance - c.enemy.GetAttackRange();
                    Vector3 position = (direction * distanceFromRange) + c.enemy.GetPosition();
                    c.enemy.navMeshAgent.SetDestination(position);
                }
            }
        }
    }
}