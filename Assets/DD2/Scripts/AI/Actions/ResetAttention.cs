using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Apex.AI;
using Apex.Serialization;
using DD2.AI.Context;

namespace DD2.AI.Actions
{
    public class ResetAttention : ActionBase
    {
        public override void Execute(IAIContext context)
        {
            AIContext ctx = (AIContext)context;

            ctx.attention = ctx.target.Stats.Presence;
        }
    }
}