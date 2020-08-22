using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Apex.AI;
using Apex.Serialization;
using DD2.AI.Context;

namespace DD2.AI.Actions
{
    public class UseAbility : ActionBase
    {
        [ApexSerialization] int index;
        [ApexSerialization] bool enable;
        public override void Execute(IAIContext context)
        {
            AIContext ctx = (AIContext)context;
            Entity entity = ctx.entity;

            if (!entity.Abilities[index].ToggleState && enable)
            {
                entity.Abilities[index].UseAbility(ctx.target, null);
            }
            else if (entity.Abilities[index].ToggleState && !enable)
            {
                entity.Abilities[index].UseAbility(ctx.target, null);
            }
        }
    }
}