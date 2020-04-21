using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Apex.AI;
using Apex.Serialization;
using DD2.AI;
using DD2.AI.Context;

namespace DD2.AI.Scorers
{
    public class DistanceOption : OptionScorerBase<Entity>
    {
        [ApexSerialization] float multiplier = 1;

        public override float Score(IAIContext context, Entity status)
        {
            AIContext c = (AIContext)context;
            float score = Vector3.Distance(c.entity.GetPosition(), status.GetPosition()) * multiplier;
            return score;
        }
    }
    public class Distance : ContextualScorerBase
    {
        [ApexSerialization] bool subtractRange;
        [ApexSerialization] Range range;

        public override float Score(IAIContext context)
        {
            AIContext c = (AIContext)context;
            float distance;
            if (subtractRange)
            {
                distance = Vector3.Distance(c.entity.GetPosition(), c.target.GetPosition()) - (range == Range.Attack ? c.entity.GetAttackRange() : c.entity.GetSearchRange());
                distance = Mathf.Max(0, distance);
            }
            else
            {
                distance = Vector3.Distance(c.entity.GetPosition(), c.target.GetPosition());
            }
            return distance  * score;
        }
    }
}