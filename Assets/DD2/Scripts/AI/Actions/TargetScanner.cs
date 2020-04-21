using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Apex.AI;
using Apex.Serialization;
using DD2.AI.Context;

namespace DD2.AI.Actions
{
    public class TargetScanner : ActionBase
    {
        [ApexSerialization] public LayerMask layerMasks;
        public override void Execute(IAIContext context)
        {
            AIContext ctx = (AIContext)context;
            Entity entity = ctx.entity;

            ctx.targetList.Clear();
            Collider[] colliders = Physics.OverlapSphere(entity.GetPosition(), entity.stats.GetSearchRange(), layerMasks);
            for (int i = 0; i < colliders.Length; i++)
            {
                float enemyDistance = Vector3.Distance(entity.GetPosition(), colliders[i].transform.position);
                if (enemyDistance < entity.stats.GetSearchRange())
                {
                    float enemyDot = Vector3.Dot(entity.GetForward(), Vector3.Normalize(colliders[i].transform.position - entity.GetPosition()));
                    float desiredDot = Mathf.Cos((Mathf.Deg2Rad * entity.stats.GetSearchAngle()) / 2f);
                    if (enemyDot > desiredDot)
                    {
                        ctx.targetList.Add(colliders[i].GetComponent<Entity>());
                    }
                }
            }
        }
    }
}

