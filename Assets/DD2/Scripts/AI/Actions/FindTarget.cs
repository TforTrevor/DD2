using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RoboRyanTron.SearchableEnum;

namespace DD2.AI.Actions
{
    [CreateAssetMenu(menuName = "Scriptable Objects/AI/Actions/Find Target")]
    public class FindTarget : Action
    {
        //Layermask to check for targets
        [SerializeField] LayerMask layerMasks;

        public override void Act(StateController controller)
        {
            Collider[] colliders = Physics.OverlapSphere(controller.status.GetPosition(), controller.status.stats.GetSearchRange(), layerMasks);
            List<Status> statuses = new List<Status>();
            for (int i = 0; i < colliders.Length; i++)
            {
                float enemyDistance = Vector3.Distance(controller.status.GetPosition(), colliders[i].transform.position);
                if (enemyDistance < controller.status.stats.GetSearchRange())
                {
                    float enemyDot = Vector3.Dot(controller.status.GetForward(), Vector3.Normalize(colliders[i].transform.position - controller.status.GetPosition()));
                    float desiredDot = Mathf.Cos((Mathf.Deg2Rad * controller.status.stats.GetSearchAngle()) / 2f);
                    if (enemyDot > desiredDot)
                    {
                        statuses.Add(colliders[i].GetComponent<Status>());
                    }
                }
            }
            if (statuses.Count > 0)
            {
                int index = -1;
                float highestAggro = -1;
                for (int i = 0; i < statuses.Count; i++)
                {
                    //Uninitialized
                    if (highestAggro < 0)
                    {
                        index = i;
                        highestAggro = statuses[i].stats.GetAggro();
                    }
                    //Higher aggro
                    else if (statuses[i].stats.GetAggro() > highestAggro)
                    {
                        index = i;
                        highestAggro = statuses[i].stats.GetAggro();
                    }
                    //Same aggro, checks closer distance
                    else if (statuses[i].stats.GetAggro() < highestAggro)
                    {
                        float distance1 = Vector3.Distance(controller.transform.position, statuses[index].transform.position);
                        float distance2 = Vector3.Distance(controller.transform.position, statuses[i].transform.position);
                        if (distance2 < distance1)
                        {
                            index = i;
                            highestAggro = statuses[i].stats.GetAggro();
                        }
                    }
                }
                controller.status.target = statuses[index].transform;
            }
        }
    }

}