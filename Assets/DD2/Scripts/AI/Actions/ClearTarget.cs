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
        [ApexSerialization] bool clearTarget;
        [ApexSerialization] bool clearTargetList;
        [ApexSerialization] LayerMask layerMask;

        public override void Execute(IAIContext context)
        {
            AIContext ctx = (AIContext)context;

            if (clearTarget && ctx.target != null && Util.Utilities.IsInLayer(ctx.target.gameObject, layerMask))
            {
                ctx.target = null;
            }
            if (clearTargetList)
            {
                List<Entity> entities = new List<Entity>();
                foreach (Entity entity in ctx.targetList)
                {
                    if (!Util.Utilities.IsInLayer(entity.gameObject, layerMask))
                    {
                        entities.Add(entity);
                    }
                }
                ctx.targetList = entities;
            }
        }
    }
}