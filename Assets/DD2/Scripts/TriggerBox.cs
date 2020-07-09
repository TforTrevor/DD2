using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DD2.Actions;
using NaughtyAttributes;

namespace DD2
{
    public class TriggerBox : MonoBehaviour
    {
        [SerializeField] bool onTriggerEnter = true;
        [SerializeField] bool onTriggerExit;
        [SerializeField] LayerMask layerMask;
        [SerializeField] [ReorderableList] List<Action> actions;

        void OnTriggerEnter(Collider collider)
        {
            if (onTriggerEnter)
            {
                Trigger(collider);
            }
        }

        void OnTriggerExit(Collider collider)
        {
            if (onTriggerExit)
            {
                Trigger(collider);
            }
        }

        void Trigger(Collider collider)
        {
            if (Util.Utilities.IsInLayer(collider.gameObject, layerMask))
            {
                Entity entity = collider.GetComponent<Entity>();
                if (entity != null)
                {
                    foreach (Action action in actions)
                    {
                        action.DoAction(entity, null);
                    }
                }
            }
        }
    }
}