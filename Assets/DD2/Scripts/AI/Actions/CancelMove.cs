using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Apex.AI;
using Apex.Serialization;
using DD2.AI.Context;
using DD2.AI.Scorers;

namespace DD2.AI.Actions
{
    public class CancelMove : ActionBase
    {
        public override void Execute(IAIContext context)
        {
            AIContext ctx = (AIContext)context;
            Enemy entity = (Enemy)ctx.entity;

            ctx.pathTarget = null;
            entity.MoveToPosition(entity.GetPosition());
        }
    }
}