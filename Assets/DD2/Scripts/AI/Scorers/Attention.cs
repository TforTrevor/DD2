using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Apex.AI;
using Apex.Serialization;
using DD2.AI.Context;

namespace DD2.AI.Scorers
{
    public class Attention : ContextualScorerBase
    {
        [ApexSerialization] float multiplier = 1;

        public override float Score(IAIContext context)
        {
            AIContext ctx = (AIContext)context;
            float score = ctx.attention * multiplier;
            return score;
        }
    }
}