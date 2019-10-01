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
        [SerializeField] [SearchableEnum] ForceMode forceMode;
        [SerializeField] bool cancelForce;

        public override void ApplyEffect(Transform target)
        {
            Status status = target.GetComponent<Status>();
            if (cancelForce)
            {
                status.ClearVelocity(true, true, true);
            }
            status.AddForce(direction.normalized * force, forceMode);
        }
    }
}
