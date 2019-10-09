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
            EnemyContext c = (EnemyContext)context;

            c.targetList.Clear();
            Collider[] colliders = Physics.OverlapSphere(c.enemy.GetPosition(), c.enemy.stats.GetSearchRange(), layerMasks);
            //List<Status> statuses = new List<Status>();
            for (int i = 0; i < colliders.Length; i++)
            {
                float enemyDistance = Vector3.Distance(c.enemy.GetPosition(), colliders[i].transform.position);
                if (enemyDistance < c.enemy.stats.GetSearchRange())
                {
                    float enemyDot = Vector3.Dot(c.enemy.GetForward(), Vector3.Normalize(colliders[i].transform.position - c.enemy.GetPosition()));
                    float desiredDot = Mathf.Cos((Mathf.Deg2Rad * c.enemy.stats.GetSearchAngle()) / 2f);
                    if (enemyDot > desiredDot)
                    {
                        c.targetList.Add(colliders[i].GetComponent<Status>());
                    }
                }
            }
        }
    }
}

