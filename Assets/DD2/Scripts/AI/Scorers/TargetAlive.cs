﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Apex.AI;
using Apex.Serialization;
using DD2.AI.Context;

namespace DD2.AI.Scorers
{
    public class TargetAlive : ContextualScorerBase
    {
        [ApexSerialization] bool not;

        public override float Score(IAIContext context)
        {
            AIContext ctx = (AIContext)context;
            if (ctx.target.IsAlive)
            {
                return not ? 0 : score;
            }
            return not ? score : 0;
        }
    }
}