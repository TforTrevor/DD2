using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Apex.AI;
using DD2.Abilities;

namespace DD2.AI.Context
{
    public class AIContext : IAIContext
    {
        public EntityAI entity;
        public List<Entity> targetList;
        public Entity target;

        public Entity pathTarget;
        public float attention;

        public AIContext(EntityAI entity)
        {
            this.entity = entity;
            targetList = new List<Entity>();
        }
    }
}