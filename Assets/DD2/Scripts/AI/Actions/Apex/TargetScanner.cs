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
            AIContext c = (AIContext)context;

            c.targetList.Clear();
            Collider[] colliders = Physics.OverlapSphere(c.entity.GetPosition(), c.entity.stats.GetSearchRange(), layerMasks);
            for (int i = 0; i < colliders.Length; i++)
            {
                float enemyDistance = Vector3.Distance(c.entity.GetPosition(), colliders[i].transform.position);
                if (enemyDistance < c.entity.stats.GetSearchRange())
                {
                    float enemyDot = Vector3.Dot(c.entity.GetForward(), Vector3.Normalize(colliders[i].transform.position - c.entity.GetPosition()));
                    float desiredDot = Mathf.Cos((Mathf.Deg2Rad * c.entity.stats.GetSearchAngle()) / 2f);
                    if (enemyDot > desiredDot)
                    {
                        c.targetList.Add(colliders[i].GetComponent<Status>());
                    }
                }
            }
        }
    }
}

