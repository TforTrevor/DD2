using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace DD2.Actions
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Actions/Apply Force")]
    public class ApplyForce : Action
    {
        [SerializeField] float force;
        [SerializeField] Vector3 direction;
        [SerializeField] bool relativeToPosition;
        [SerializeField] Vector3 additionalForce;
        [SerializeField] ForceMode forceMode;
        [SerializeField] bool clearVelocity;

        public override void DoAction(Entity target, Entity caller, object payload)
        {
            if (clearVelocity)
            {
                target.ClearVelocity(true, true, true);
            }
            if (relativeToPosition)
            {
                Vector3 direction;
                if (payload.GetType() == typeof(Vector3))
                {
                    direction = Util.Utilities.Direction((Vector3)payload, target.transform.position);
                }
                else
                {
                    direction = Util.Utilities.Direction(caller.transform.position, target.transform.position);
                }
                target.AddForce((direction * force) + additionalForce, forceMode);
            }
        }
    }
}
