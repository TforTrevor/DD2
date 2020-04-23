using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Apex.AI;
using Apex.Serialization;
using DD2.AI.Context;

namespace DD2.AI.Scorers
{
    public class HasTarget : ContextualScorerBase
    {
        [ApexSerialization] bool not;
        [ApexSerialization] bool targetList;
        [ApexSerialization] LayerMask layerMask;

        public override float Score(IAIContext context)
        {
            AIContext ctx = (AIContext)context;
            if (targetList)
            {
                foreach (Entity entity in ctx.targetList)
                {
                    if (Util.Utilities.IsInLayer(entity.gameObject, layerMask))
                    {
                        return not ? 0 : score;
                    }
                }
                return not ? score : 0;
            }
            else
            {
                if (ctx.target != null && Util.Utilities.IsInLayer(ctx.target.gameObject, layerMask))
                {
                    return not ? 0 : score;
                }
                return not ? score : 0;
            }            
        }
    }
}