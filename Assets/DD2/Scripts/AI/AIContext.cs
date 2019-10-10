using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Apex.AI;
using DD2.Abilities;

namespace DD2.AI.Context
{
    public class AIContext : IAIContext
    {
        public AIContext(AIStatus status)
        {
            entity = status;
            targetList = new List<Status>();
        }

        public AIStatus entity;
        public List<Status> targetList;
        public Status target;
    }
}