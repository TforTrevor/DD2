using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Apex.AI;
using Apex.Serialization;
using DD2.AI.Context;

namespace DD2.AI.Scorers
{
    public class RangeCheckTarget : ContextualScorerBase
    {
        [ApexSerialization] bool not;
        [ApexSerialization] bool coneCheck;
        [ApexSerialization] bool losCheck;
        [ApexSerialization] LayerMask losBlock;
        [ApexSerialization] bool includeRadius = true;
        [ApexSerialization] bool includeTargetRadius = true;        
        
        public override float Score(IAIContext context)
        {
            AIContext ctx = (AIContext)context;
            Entity entity = ctx.entity;
            Entity target = ctx.target;

            float distance = Vector3.Distance(ctx.target.transform.position, entity.transform.position) 
                                - (includeRadius ? entity.Radius : 0)
                                - (includeTargetRadius ? ctx.target.Radius : 0);

            if (distance <= entity.Stats.AttackRange)
            {
                bool cone = false;
                bool los = false;
                if (coneCheck)
                {
                    Vector2 start = new Vector2(entity.EyePosition.x, entity.EyePosition.z);
                    Vector2 direction = new Vector2(entity.transform.forward.x, entity.transform.forward.z);
                    Vector2 end = new Vector2(target.EyePosition.x, target.EyePosition.z);
                    cone = Util.Utilities.IsPositionInCone(start, direction, end, entity.Stats.AttackAngle / 2);
                }
                if (losCheck)
                {
                    los = !Physics.Raycast(entity.EyePosition, Util.Utilities.Direction(entity.EyePosition, target.EyePosition), Vector3.Distance(entity.EyePosition, target.EyePosition), losBlock);
                }
                if ((coneCheck ? cone : true) && (losCheck ? los : true))
                {
                    return not ? 0 : score;
                }
            }
            return not ? score : 0;
        }
    }
}

