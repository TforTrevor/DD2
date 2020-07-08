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
        [ApexSerialization] bool includeRadius = true;
        [ApexSerialization] bool includeTargetRadius = true;

        public override void Execute(IAIContext context)
        {
            AIContext ctx = (AIContext)context;
            Entity entity = ctx.entity;

            float range = this.range == Range.Attack ? entity.Stats.AttackRange : entity.Stats.SearchRange;                            
            float angle = this.range == Range.Attack ? entity.Stats.AttackAngle : entity.Stats.SearchAngle;

            Collider[] colliders = Physics.OverlapSphere(entity.GetPosition(), range, layerMasks);
            for (int i = 0; i < colliders.Length; i++)
            {
                Entity temp = colliders[i].GetComponent<Entity>();
                if (temp != null)
                {
                    float enemyDistance = Vector3.Distance(entity.GetPosition(), temp.GetPosition())
                                        - (includeRadius ? entity.Radius : 0)
                                        - (includeTargetRadius ? temp.Radius : 0);
                    if (enemyDistance < range)
                    {
                        if (useCone)
                        {
                            if (Util.Utilities.IsColliderInCone(colliders[i], entity.transform, angle, range, layerMasks))
                            {
                                if (temp != null)
                                {
                                    ctx.targetList.Add(temp);
                                }
                            }
                        }
                        else
                        {
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
}

