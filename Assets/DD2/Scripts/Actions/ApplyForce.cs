using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RoboRyanTron.SearchableEnum;
using NaughtyAttributes;

namespace DD2.Actions
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Actions/Apply Force")]
    public class ApplyForce : Action
    {
        [SerializeField] [SearchableEnum] ElementType elementType;
        [SerializeField] float force;
        [SerializeField] Vector3 direction;
        [SerializeField] bool relativeToPosition;
        [SerializeField] [SearchableEnum] ForceMode forceMode;
        [SerializeField] bool cancelForce;

        public override void DoAction(Entity target, Entity caller, object payload)
        {
            Vector3 position = (Vector3)payload;
            Vector3 vector;
            if (cancelForce)
            {
                caller.ClearVelocity(true, true, true);
            }
            if (relativeToPosition)
            {
                vector = new Vector3
                {
                    x = direction.x != 0 ? direction.x : position.x,
                    y = direction.y != 0 ? direction.y : position.y,
                    z = direction.z != 0 ? direction.z : position.z
                };
                vector = vector.normalized;
                Debug.Log(vector);
            }
            else
            {
                vector = direction.normalized;
            }
            caller.AddForce(vector * force, forceMode);
        }
    }
}
