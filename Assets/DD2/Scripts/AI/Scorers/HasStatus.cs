using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Apex.AI;
using Apex.Serialization;
using DD2.AI.Context;


namespace DD2.AI.Scorers
{
    public class HasStatus : ContextualScorerBase
    {
        [ApexSerialization] bool not;
        [ApexSerialization] StatusEffect statusEffects;

        public override float Score(IAIContext context)
        {
            AIContext ctx = (AIContext)context;
            Entity entity = ctx.entity;

            if (entity.StatusEffects.HasFlag(statusEffects))
            {
                return not ? 0 : score;
            }
            return not ? score : 0;
        }
    }
}