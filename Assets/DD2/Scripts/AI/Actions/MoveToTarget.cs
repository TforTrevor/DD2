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
            Entity target = ctx.target;

            if (ctx.target != null && ctx.pathTarget != ctx.target && entity.IsGrounded && !entity.IsRagdolled)
            {
                float range = this.range == Range.Attack ? entity.Stats.AttackRange : entity.Stats.SearchRange;
                float entityRadius = includeRadius ? entity.Radius : 0;
                float targetRadius = includeTargetRadius ? ctx.target.Radius : 0;
                float distance = Vector3.Distance(ctx.target.transform.position, entity.transform.position) - entityRadius - targetRadius;

                if (ensureMaxRange)
                {
                    Vector3 direction = Vector3.Normalize(ctx.target.transform.position - entity.transform.position);
                    float distanceFromRange = distance - range + 0.1f;
                    Vector3 position = (direction * distanceFromRange) + entity.transform.position;
                    entity.MoveToPosition(position);
                }
                else
                {
                    Vector3 direction = Util.Utilities.Direction(target.transform.position, entity.transform.position);
                    Vector3 position = (direction * (entityRadius + targetRadius)) + target.transform.position;
                    entity.MoveToPosition(position);
                }
            }
        }
    }
}