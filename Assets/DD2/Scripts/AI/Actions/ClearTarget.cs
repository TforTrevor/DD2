using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Apex.AI;
using Apex.Serialization;
using DD2.AI.Context;

namespace DD2.AI.Actions
{
    public class ClearTarget : ActionBase
    {
        public override void Execute(IAIContext context)
        {
            AIContext c = (AIContext)context;
            c.target = null;
        }
    }
}