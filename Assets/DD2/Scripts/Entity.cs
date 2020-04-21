using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using DD2.Abilities;
using DD2.Actions;

namespace DD2
{
    public class Entity : MonoBehaviour
    {
        public Stats stats;
        [SerializeField] float currentHealth;

        [SerializeField] Transform fireTransform;
        [ReadOnly] [SerializeField] protected bool grounded;
        [ReadOnly] [SerializeField] protected bool ragdolled;
        [SerializeField] [ReorderableList] protected Action[] actions;
        [SerializeField] [ReorderableList] protected Ability[] abilities;

        protected Rigidbody rb;

        protected virtual void Awake()
        {
            rb = GetComponent<Rigidbody>();
            for (int i = 0; i < abilities.Length; i++)
            {
                abilities[i].SetEntity(this);
            }
        }

        void Start()
        {
            Initialize();
        }

        void Initialize()
        {
            currentHealth = stats.GetMaxHealth();
        }

        public void Damage(float damage)
        {
            currentHealth -= damage;
        }

        public virtual void Ragdoll()
        {

        }

        public virtual void AddForce(Vector3 force, ForceMode forceMode)
        {
            rb.AddForce(force, forceMode);
        }

        public virtual void ClearVelocity(bool x, bool y, bool z)
        {
            Vector3 velocity = rb.velocity;
            if (x)
            {
                velocity.x = 0;
            }
            if (y)
            {
                velocity.y = 0;
            }
            if (z)
            {
                velocity.z = 0;
            }
            rb.velocity = velocity;
        }

        public void SetGrounded(bool value)
        {
            if (value && !grounded)
            {
                grounded = value;
                OnGrounded();
            } 
            else
            {
                grounded = value;
            }
        }

        protected virtual void OnGrounded()
        {
            
        }

        public void DoAction()
        {
            actions[0]?.DoAction(transform, this, transform.position);
        }

        public void DoAction(int index)
        {
            actions[index]?.DoAction(transform, this, transform.position);
        }

        public void DoAction(Transform target)
        {
            actions[0]?.DoAction(target, this, transform.position);
        }

        public void DoAction(int index, Transform target)
        {
            actions[index]?.DoAction(target, this, transform.position);
        }



        //ACCESSORS

        public float GetCurrentHealth()
        {
            return currentHealth;
        }

        public float GetMaxHealth()
        {
            return stats.GetMaxHealth();
        }

        public Ability GetAbility(int index)
        {
            return abilities[index];
        }

        public Ability[] GetAllAbilities()
        {
            return abilities;
        }

        public virtual Vector3 GetPosition()
        {
            return transform.position;
        }

        public virtual Vector3 GetForward()
        {
            return transform.forward;
        }

        public Vector3 GetFirePosition()
        {
            return fireTransform.position;
        }

        public float GetPresence()
        {
            return stats.GetPresence();
        }

        public float GetAttackRange()
        {
            return stats.GetAttackRange();
        }

        public float GetSearchRange()
        {
            return stats.GetSearchRange();
        }

        public float GetSearchAngle()
        {
            return stats.GetSearchAngle();
        }
    }
}
