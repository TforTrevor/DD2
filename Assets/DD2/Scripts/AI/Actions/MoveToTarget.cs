using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Apex.AI;
using Apex.Serialization;
using DD2.AI.Context;
using DD2.AI.Scorers;

namespace DD2.AI.Actions
{
    public class MoveToTarget : ActionBase
    {
        [ApexSerialization] bool ensureMaxRange;
        [ApexSerialization] Range range;
        [ApexSerialization] bool includeRadius = true;
        [ApexSerialization] bool includeTargetRadius = true;

        public override void Execute(IAIContext context)
        {
            AIContext ctx = (AIContext)context;
            Enemy entity = (Enemy)ctx.entity;

            if (ctx.target && ctx.pathTarget != ctx.target && entity.IsGrounded && !entity.IsRagdolled)
            {
                float range = this.range == Range.Attack ? entity.Stats.AttackRange : entity.Stats.SearchRange;
                float distance = Vector3.Distance(ctx.target.GetPosition(), entity.GetPosition())
                                    - (includeRadius ? entity.Radius : 0)
                                    - (includeTargetRadius ? ctx.target.Radius : 0);
                if (distance > range || ensureMaxRange)
                {
                    Vector3 direction = Vector3.Normalize(ctx.target.GetPosition() - entity.GetPosition());
                    float distanceFromRange = distance - range + 0.1f;
                    Vector3 position = (direction * distanceFromRange) + entity.GetPosition();
                    entity.MoveToPosition(position);
                    //ctx.pathTarget = ctx.target;
                }
            }
        }
    }
}