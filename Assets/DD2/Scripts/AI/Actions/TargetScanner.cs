using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Apex.AI;
using Apex.Serialization;
using DD2.AI.Context;
using DD2.AI.Scorers;

namespace DD2.AI.Actions
{
    public class TargetScanner : ActionBase
    {
        [ApexSerialization] LayerMask layerMasks;
        [ApexSerialization] bool useCone;
        [ApexSerialization] Range range;

        public override void Execute(IAIContext context)
        {
            AIContext ctx = (AIContext)context;
            Entity entity = ctx.entity;

            float range = this.range == Range.Attack ? entity.GetStats().GetAttackRange() : entity.GetStats().GetSearchRange();
            float angle = this.range == Range.Attack ? entity.GetStats().GetAttackAngle() : entity.GetStats().GetSearchAngle();

            Collider[] colliders = Physics.OverlapSphere(entity.GetPosition(), range, layerMasks);
            for (int i = 0; i < colliders.Length; i++)
            {
                float enemyDistance = Vector3.Distance(entity.GetPosition(), colliders[i].transform.position);
                if (enemyDistance < range)
                {
                    if (useCone)
                    {
                        float enemyDot = Vector3.Dot(entity.GetForward(), Vector3.Normalize(colliders[i].transform.position - entity.GetPosition()));
                        float desiredDot = Mathf.Cos(Mathf.Deg2Rad * angle / 2f);
                        if (enemyDot > desiredDot)
                        {
                            Entity temp = colliders[i].GetComponent<Entity>();
                            if (temp != null)
                            {
                                ctx.targetList.Add(temp);
                            }                            
                        }
                    }
                    else
                    {
                        Entity temp = colliders[i].GetComponent<Entity>();
                        if (temp != null) 
                        {
                            ctx.targetList.Add(temp);
                        }
                    }                    
                }
            }
        }
    }
}

