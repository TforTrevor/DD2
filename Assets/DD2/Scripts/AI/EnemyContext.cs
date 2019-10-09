using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Apex.AI;

namespace DD2.AI.Context
{
    public class EnemyContext : IAIContext
    {
        public AIStatus enemy;
        public List<Status> targetList;
        public Status target;
    }
}