using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DD2.Actions;
using DD2.SOArchitecture;

namespace DD2
{
    public class Player : Entity
    {
        [SerializeField] Action primaryFire;
        [SerializeField] Action secondaryFire;
        [SerializeField] Action ability1;
        [SerializeField] Vector3Variable lookInput;

        PlayerMovement movement;

        protected override void Awake()
        {
            base.Awake();
            movement = GetComponent<PlayerMovement>();
        }

        public void DoPrimaryFire()
        {
            primaryFire?.DoAction(null, this, null);
        }

        public void SetPrimaryFire(Action action)
        {
            primaryFire = action;
        }

        public void DoSecondaryFire()
        {
            secondaryFire?.DoAction(null, this, null);
        }

        public void SetSecondaryFire(Action action)
        {
            secondaryFire = action;
        }

        public void DoAbility1()
        {
            ability1?.DoAction(null, this, null);
        }

        public void DoAbility1(Entity target)
        {
            ability1?.DoAction(target, this, null);
        }

        public void SetAbility1(Action action)
        {
            ability1 = action;
        }

        public Vector3Variable GetLookInput()
        {
            return lookInput;
        }

        public void ToggleMovement(bool value)
        {
            movement.enableMovement = value;
        }

        public void ToggleLook(bool value)
        {
            movement.enableLook = value;
        }
    }
}

