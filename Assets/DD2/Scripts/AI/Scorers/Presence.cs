using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Apex.AI;
using Apex.Serialization;
using DD2.AI.Context;

namespace DD2.AI.Scorers
{
    public class PresenceOption : OptionScorerBase<Entity>
    {
        [ApexSerialization] float multiplier = 1;

        public override float Score(IAIContext context, Entity status)
        {
            AIContext c = (AIContext)context;
            float score = status.GetPresence() * multiplier;
            return score;
        }
    }

    public class Presence : ContextualScorerBase
    {
        public override float Score(IAIContext context)
        {
            AIContext c = (AIContext)context;
            return c.target.GetPresence() * score;
        }
    }
}