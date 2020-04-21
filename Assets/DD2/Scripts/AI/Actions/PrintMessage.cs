using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Apex.AI;
using Apex.Serialization;
using DD2.AI.Context;

namespace DD2.AI.Actions
{
    public class PrintMessage : ActionBase
    {
        [ApexSerialization] string message;
        public override void Execute(IAIContext context)
        {
            AIContext c = (AIContext)context;
            Debug.Log(c.entity + ": " + message);
        }
    }
}