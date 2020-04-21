using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using MEC;
using DD2.AI.Context;
using Apex.AI.Components;
using Apex.AI;
using System;

namespace DD2.AI
{
    public class EntityAI : Entity, IContextProvider
    {
        protected AIContext context;

        protected override void Awake()
        {
            base.Awake();
            context = new AIContext(this);
        }

        public IAIContext GetContext(Guid aiId)
        {
            return context;
        }
    }
}