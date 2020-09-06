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

        CoroutineHandle lookHandle;

        protected override void Awake()
        {
            base.Awake();
            context = new AIContext(this);
        }

        protected override void Die(Entity entity)
        {
            base.Die(entity);

            StopLookAt();
        }

        public IAIContext GetContext(Guid aiId)
        {
            return context;
        }

        public virtual void LookAt(Entity entity)
        {
            StopLookAt();
            lookHandle = Timing.RunCoroutine(LookAtRoutine(entity));
        }

        public virtual void StopLookAt()
        {
            Timing.KillCoroutines(lookHandle);
        }

        protected virtual IEnumerator<float> LookAtRoutine(Entity entity)
        {
            while (true)
            {
                if (entity.IsAlive)
                {
                    Vector2 start = new Vector2(transform.position.x, transform.position.z);
                    Vector2 end = new Vector2(entity.transform.position.x, entity.transform.position.z);
                    Vector2 direction = Util.Utilities.Direction(start, end);
                    transform.forward = new Vector3(direction.x, 0, direction.y);
                    yield return Timing.WaitForOneFrame;
                }
                else
                {
                    break;
                }
            }
        }
    }
}