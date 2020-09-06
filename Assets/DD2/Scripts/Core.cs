using System.Collections;
using System.Collections.Generic;
using UnityAtoms.BaseAtoms;
using UnityEngine;

namespace DD2
{
    public class Core : Entity
    {
        [SerializeField] new VoidEvent onDeath;

        public override void ClearVelocity(bool x, bool y, bool z)
        {
            
        }

        public override void AddForce(Vector3 force, ForceMode forceMode)
        {
            
        }

        protected override void Die(Entity entity)
        {
            onDeath.Raise();
        }
    }
}