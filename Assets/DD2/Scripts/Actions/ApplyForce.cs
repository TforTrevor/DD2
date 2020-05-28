using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

namespace DD2.Actions
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Actions/Apply Force")]
    public class ApplyForce : Action
    {
        [SerializeField] float force;
        [SerializeField] Vector3 direction;
        [SerializeField] bool relativeToPosition;
        [SerializeField] ForceMode forceMode;
        [SerializeField] bool clearVelocity;

        public override void DoAction(Entity target, Entity caller, object payload)
        {
            Vector3 position = caller.GetPosition();
            Vector3 vector;
            if (clearVelocity)
            {
                target.ClearVelocity(true, true, true);
            }
            if (relativeToPosition)
            {
                if (payload != null)
                {
                    vector = Vector3.Normalize(target.GetPosition() - (Vector3)payload);
                }
                else if (caller != null)
                {
                    vector = Vector3.Normalize(target.GetPosition() - caller.GetPosition());
                }
                else
                {
                    vector = Vector3.zero;
                }
            }
            else
            {
                vector = direction.normalized;
            }
            if (vector == Vector3.zero)
            {
                vector = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)).normalized;
            }
            if (target.IsGrounded && (vector.y < 0.1f || vector.y > -0.1f))
            {
                vector += Vector3.up * 0.5f;
                vector.Normalize();
            }
            target.AddForce(vector * force, forceMode);
        }
    }
}
