using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DD2.Actions;
using DD2.Abilities;
using UnityAtoms.BaseAtoms;

namespace DD2
{
    public class Player : Entity
    {
        [SerializeField] Ability primaryFire;
        [SerializeField] Action secondaryFire;
        [SerializeField] Action ability1;
        [SerializeField] Action ability2;
        [SerializeField] PlayerMovement movement;
        [SerializeField] new PlayerCamera camera;
        [SerializeField] float manaOrbRadius;
        [SerializeField] LayerMask manaOrbLayerMask;
        [SerializeField] BoolVariable enableMove;
        [SerializeField] BoolVariable enableLook;

        Collider[] manaOrbs;
        int manaOrbsCount;

        protected override void Awake()
        {
            base.Awake();
            manaOrbs = new Collider[100];
        }

        protected override void Die(Entity entity)
        {
            if (entity != null)
            {
                Debug.Log(entity.name + " killed " + name);
            }
            else
            {
                Debug.Log("null killed " + name);
            }
        }

        public override void AddForce(Vector3 force, ForceMode forceMode)
        {
            base.AddForce(force, forceMode);
        }

        protected virtual void FixedUpdate()
        {
            Util.Utilities.ClearArray(manaOrbs, manaOrbsCount);
            manaOrbsCount = Physics.OverlapSphereNonAlloc(GetPosition(), manaOrbRadius, manaOrbs, manaOrbLayerMask);
            for (int i = 0; i < manaOrbsCount; i++)
            {
                ManaOrb orb = manaOrbs[i].GetComponent<ManaOrb>();
                if (orb != null && CurrentMana + orb.GetAmount() <= Stats.MaxMana)
                {
                    orb.PickUp(this);
                }                            
            }
        }

        public void DoPrimaryFire(bool value)
        {
            if (!primaryFire.ToggleState && value)
            {
                primaryFire.UseAbility(this, null);
            }
            else if (primaryFire.ToggleState && !value)
            {
                primaryFire.UseAbility(this, null);
            }
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

        public void DoAbility2()
        {
            ability2?.DoAction(null, this, null);
        }

        public void DoAbility2(Entity target)
        {
            ability2?.DoAction(target, this, null);
        }

        public void SetAbility2(Action action)
        {
            ability2 = action;
        }

        public void ToggleMovement(bool value)
        {
            enableMove.Value = value;
        }

        public void ToggleLook(bool value)
        {
            enableLook.Value = value;
        }

        void Update()
        {
            Vector3 velocity = transform.InverseTransformDirection(rb.velocity);
            Animator.SetFloat("Blend_X", velocity.x / Stats.MoveSpeed);
            Animator.SetFloat("Blend_Y", velocity.z / Stats.MoveSpeed);
            Animator.SetBool("Is_Moving", movement.IsMoving);
            Animator.SetBool("Is_Grounded", movement.IsGrounded);
        }
    }
}