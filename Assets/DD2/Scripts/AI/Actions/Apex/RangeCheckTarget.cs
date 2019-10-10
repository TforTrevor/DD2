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
        
        public override float Score(IAIContext context)
        {
            AIContext c = (AIContext)context;

            float distance = Vector3.Distance(c.target.GetPosition(), c.entity.GetPosition());
            if (rangeCheck == Range.Attack ? distance > c.entity.GetAttackRange() : distance > c.entity.GetSearchRange())
            {
                if (checkCone)
                {
                    float enemyDot = Vector3.Dot(c.entity.GetForward(), Vector3.Normalize(c.target.GetPosition() - c.entity.GetPosition()));
                    float desiredDot = Mathf.Cos((Mathf.Deg2Rad * c.entity.GetSearchAngle()) / 2f);
                    if (enemyDot > desiredDot)
                    {
                        return not ? score : 0;
                    }
                }
                else
                {
                    return not ? score : 0;
                }
                
            }
            return not ? 0 : score;
        }
    }

    public enum Range
    {
        Attack,
        Search
    }
}

