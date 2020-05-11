using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Apex.AI;
using Apex.Serialization;
using DD2.AI.Context;

namespace DD2.AI.Scorers
{
    public class Health : OptionScorerBase<Entity>
    {
        [ApexSerialization] float multiplier = 1;

        public override float Score(IAIContext context, Entity entity)
        {
            AIContext ctx = (AIContext)context;
            float score = entity.GetCurrentHealth() / entity.Stats.MaxHealth * multiplier;
            return score;
        }
    }
}