﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Apex.AI;
using Apex.Serialization;
using DD2.AI.Context;

namespace DD2.AI.Scorers
{
    public class Health : OptionScorerBase<Status>
    {
        [ApexSerialization] float multiplier = 1;

        public override float Score(IAIContext context, Status status)
        {
            AIContext c = (AIContext)context;
            float score = status.GetCurrentHealth() / status.GetMaxHealth() * multiplier;
            return score;
        }
    }
}