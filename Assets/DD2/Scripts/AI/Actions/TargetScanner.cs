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
        [ApexSerialization] LayerMask scanMask;
        [ApexSerialization] bool coneCheck;
        [ApexSerialization] bool losCheck;
        [ApexSerialization] LayerMask losBlock;
        [ApexSerialization] Range range;
        [ApexSerialization] bool includeRadius = true;
        [ApexSerialization] bool includeTargetRadius = true;

        public override void Execute(IAIContext context)
        {
            AIContext ctx = (AIContext)context;
            Entity entity = ctx.entity;

            float range = this.range == Range.Attack ? entity.Stats.AttackRange : entity.Stats.SearchRange;                            
            float angle = this.range == Range.Attack ? entity.Stats.AttackAngle : entity.Stats.SearchAngle;

            Collider[] colliders = Physics.OverlapSphere(entity.transform.position, range, scanMask);
            for (int i = 0; i < colliders.Length; i++)
            {
                Entity temp = colliders[i].GetComponent<Entity>();
                if (temp != null)
                {
                    float enemyDistance = Vector3.Distance(entity.transform.position, temp.transform.position)
                                        - (includeRadius ? entity.Radius : 0)
                                        - (includeTargetRadius ? temp.Radius : 0);
                    if (enemyDistance < range)
                    {
                        bool cone = false;
                        bool los = false;
                        if (coneCheck)
                        {
                            Vector2 start = new Vector2(entity.EyePosition.x, entity.EyePosition.z);
                            Vector2 direction = new Vector2(entity.transform.forward.x, entity.transform.forward.z);
                            Vector2 end = new Vector2(temp.EyePosition.x, temp.EyePosition.z);
                            cone = Util.Utilities.IsPositionInCone(start, direction, end, angle / 2);
                        }
                        if (losCheck)
                        {
                            los = !Physics.Raycast(entity.EyePosition, Util.Utilities.Direction(entity.EyePosition, temp.EyePosition), Vector3.Distance(entity.EyePosition, temp.EyePosition), losBlock);
                        }
                        if ((coneCheck ? cone : true) && (losCheck ? los : true))
                        {
                            ctx.targetList.Add(temp);
                        }
                    }
                }
            }
        }
    }
}

