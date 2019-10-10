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
            AIContext c = (AIContext)context;

            if (!c.entity.GetAbility(index).GetToggleState() && enable)
            {
                c.entity.GetAbility(index).UseAbility(c.target.transform);
            }
            else if (c.entity.GetAbility(index).GetToggleState() && !enable)
            {
                c.entity.GetAbility(index).UseAbility(c.target.transform);
            }
        }
    }
}