using Apex.AI;
using Apex.Serialization;
using DD2.AI.Context;

namespace DD2.AI.Actions
{
    public class LookAtTarget : ActionBase
    {
        [ApexSerialization] State state;

        public override void Execute(IAIContext context)
        {
            AIContext ctx = (AIContext)context;
            EntityAI entity = ctx.entity;
            Entity target = ctx.target;
            
            if (state == State.Start && target != ctx.lookTarget)
            {
                ctx.lookTarget = target;
                entity.LookAt(target);
            }
            else
            {
                ctx.lookTarget = null;
                entity.StopLookAt();
            }
        }

        enum State { Start, Stop }
    }
}