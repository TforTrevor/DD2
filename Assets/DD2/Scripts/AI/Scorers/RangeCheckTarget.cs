using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Apex.AI;
using Apex.Serialization;
using DD2.AI.Context;

namespace DD2.AI.Scorers
{
    public class RangeCheckTarget : ContextualScorerBase
    {
        [ApexSerialization] bool not;
        [ApexSerialization] bool checkCone;
        [ApexSerialization] Range rangeCheck;
        [ApexSerialization] bool includeRadius = true;
        [ApexSerialization] bool includeTargetRadius = true;        
        
        public override float Score(IAIContext context)
        {
            AIContext ctx = (AIContext)context;
            Entity entity = ctx.entity;

            float distance = Vector3.Distance(ctx.target.GetPosition(), entity.GetPosition()) 
                                - (includeRadius ? entity.Radius : 0)
                                - (includeTargetRadius ? ctx.target.Radius : 0);

            if (rangeCheck == Range.Attack ? distance <= entity.Stats.AttackRange : distance <= entity.Stats.SearchRange)
            {
                if (checkCone)
                {
                    float enemyDot = Vector3.Dot(entity.GetForward(), Vector3.Normalize(ctx.target.GetPosition() - entity.GetPosition()));
                    float desiredDot = Mathf.Cos(Mathf.Deg2Rad * (rangeCheck == Range.Attack ? entity.Stats.AttackAngle : entity.Stats.SearchAngle) / 2f);
                    if (enemyDot > desiredDot)
                    {
                        return not ? 0 : score;
                    }
                }
                else
                {
                    return not ? 0 : score;
                }
            }
            Vector3 dir = Vector3.Normalize(ctx.target.GetPosition() - entity.GetPosition());
            Vector3 startPos = entity.GetPosition() + dir * entity.Radius;
            Debug.DrawRay(startPos, dir * distance, Color.green, 0.5f);
            return not ? score : 0;
        }
    }

    public enum Range
    {
        Attack,
        Search
    }
}

