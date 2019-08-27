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
            Collider[] colliders = Physics.OverlapSphere(controller.status.GetPosition(), controller.status.stats.GetRange(), layerMasks);
            List<Status> statuses = new List<Status>();
            for (int i = 0; i < colliders.Length; i++)
            {
                statuses.Add(colliders[i].GetComponent<Status>());
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
                        float distance1 = Vector3.Distance(controller.transform.position, statuses[index].GetPosition());
                        float distance2 = Vector3.Distance(controller.transform.position, statuses[i].GetPosition());
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