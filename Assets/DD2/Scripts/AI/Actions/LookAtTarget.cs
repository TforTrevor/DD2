using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Apex.AI;
using Apex.Serialization;
using DD2.AI.Context;
using UnityEditor.Rendering.LookDev;

namespace DD2.AI.Actions
{
    public class LookAtTarget : ActionBase
    {
        [ApexSerialization] State state;

        public override void Execute(IAIContext context)
        {
            AIContext ctx = (AIContext)context;
            EntityAI entity = ctx.entity;
            Entity target = ctx.target;
            
            if (state == State.Start && target != ctx.lookTarget)
            {
                ctx.lookTarget = target;
                entity.LookAt(target.transform);
            }
            else
            {
                ctx.lookTarget = null;
                entity.StopLookAt();
            }
        }

        enum State { Start, Stop }
    }
}