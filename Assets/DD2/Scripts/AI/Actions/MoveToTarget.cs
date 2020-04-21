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
            AIContext ctx = (AIContext)context;
            Enemy enemy = (Enemy)ctx.entity;

            if (ctx.target)
            {
                float distance = Vector3.Distance(ctx.target.GetPosition(), enemy.GetPosition());
                if (distance > enemy.GetAttackRange() || ensureMaxRange)
                {
                    Vector3 direction = Vector3.Normalize(ctx.target.GetPosition() - enemy.GetPosition());
                    float distanceFromRange = distance - enemy.GetAttackRange();
                    Vector3 position = (direction * distanceFromRange) + enemy.GetPosition();
                    enemy.navMeshAgent.SetDestination(position);
                }
            }
        }
    }
}