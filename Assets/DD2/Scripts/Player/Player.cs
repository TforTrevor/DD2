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
            primaryFire?.DoAction(transform, this, GetPosition());
        }

        public void SetPrimaryFire(Action action)
        {
            primaryFire = action;
        }

        public void DoSecondaryFire()
        {
            secondaryFire?.DoAction(transform, this, GetPosition());
        }

        public void SetSecondaryFire(Action action)
        {
            secondaryFire = action;
        }

        public void DoAbility1()
        {
            ability1?.DoAction(transform, this, GetPosition());
        }

        public void DoAbility1(Transform target)
        {
            ability1?.DoAction(target, this, GetPosition());
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

