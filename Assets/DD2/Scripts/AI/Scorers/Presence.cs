using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Apex.AI;
using Apex.Serialization;
using DD2.AI.Context;

namespace DD2.AI.Scorers
{
    public class Presence : OptionScorerBase<Status>
    {
        [ApexSerialization] float multiplier = 1;

        public override float Score(IAIContext context, Status status)
        {
            EnemyContext c = (EnemyContext)context;
            float score = status.GetPresence() * multiplier;
            return score;
        }
    }
}