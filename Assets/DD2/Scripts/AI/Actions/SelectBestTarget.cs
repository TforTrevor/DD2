using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Apex.AI;
using DD2.AI.Context;

namespace DD2.AI.Actions
{
    public class SelectBestTarget : ActionWithOptions<Entity>
    {
        public override void Execute(IAIContext context)
        {
            AIContext ctx = (AIContext)context;

            Entity best = GetBest(context, ctx.targetList);
            ctx.target = best;
        }
    }
}