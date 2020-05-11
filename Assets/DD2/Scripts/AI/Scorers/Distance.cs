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
        [ApexSerialization] bool includeRadius = true;
        [ApexSerialization] bool includeTargetRadius = true;

        public override float Score(IAIContext context, Entity target)
        {
            AIContext ctx = (AIContext)context;
            Entity entity = ctx.entity;
            float score = Vector3.Distance(entity.GetPosition(), target.GetPosition())
                            - (includeRadius ? entity.Radius : 0)
                            - (includeTargetRadius ? target.Radius : 0);
            return score * multiplier;
        }
    }

    public class Distance : ContextualScorerBase
    {
        [ApexSerialization] bool includeRadius = true;
        [ApexSerialization] bool includeTargetRadius = true;

        public override float Score(IAIContext context)
        {
            AIContext ctx = (AIContext)context;
            Entity entity = ctx.entity;
            Entity target = ctx.target;
            float distance = Vector3.Distance(entity.GetPosition(), target.GetPosition())
                            - (includeRadius ? entity.Radius : 0)
                            - (includeTargetRadius ? target.Radius : 0);
            return distance * score;
        }
    }
}