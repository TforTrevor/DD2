using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Apex.AI;
using DD2.AI.Context;

namespace DD2.AI.Actions
{
    public class SelectBestTarget : ActionWithOptions<Status>
    {
        public override void Execute(IAIContext context)
        {
            EnemyContext c = (EnemyContext)context;

            Status best = GetBest(context, c.targetList);
            c.target = best;
        }
    }
}