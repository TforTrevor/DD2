using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RoboRyanTron.SearchableEnum;
using NaughtyAttributes;

namespace DD2.Abilities.Effects
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Abilities/Effects/Apply Force")]
    public class ApplyForce : Effect
    {
        [SerializeField] [SearchableEnum] ElementType elementType;
        [SerializeField] float force;
        [SerializeField] Vector3 direction;
        [SerializeField] bool relativeToPosition;
        [SerializeField] [SearchableEnum] ForceMode forceMode;
        [SerializeField] bool cancelForce;

        public override void ApplyEffect(Transform target, Status status, Vector3 position)
        {
            Vector3 vector;
            if (cancelForce)
            {
                status.ClearVelocity(true, true, true);
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
            status.AddForce(vector * force, forceMode);
        }
    }
}
