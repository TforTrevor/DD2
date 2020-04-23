using Apex.AI;
using Apex.Serialization;
using DD2.AI.Context;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DD2.AI.Actions
{
    public class TargetCore : ActionBase
    {
        public override void Execute(IAIContext context)
        {
            AIContext ctx = (AIContext)context;

            List<Core> cores = LevelManager.Instance.GetCores();
            ctx.targetList.AddRange(cores);
        }
    }
}