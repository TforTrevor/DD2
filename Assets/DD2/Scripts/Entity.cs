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
        [SerializeField] protected string objectPoolKey;
        [SerializeField] protected Stats stats;
        [SerializeField] protected float currentHealth;
        [ReadOnly] [SerializeField] protected bool alive;

        [SerializeField] protected Transform fireTransform;
        [ReadOnly] [SerializeField] protected bool grounded;
        [ReadOnly] [SerializeField] protected bool ragdolled;
        [SerializeField] [ReorderableList] protected Action[] actions;
        [SerializeField] [ReorderableList] protected Ability[] abilities;

        public delegate void UpdateHealthHandler(float amount);
        public event UpdateHealthHandler healthUpdated;
        protected Rigidbody rb;

        protected virtual void Awake()
        {
            rb = GetComponent<Rigidbody>();
            for (int i = 0; i < abilities.Length; i++)
            {
                abilities[i].SetEntity(this);
            }
        }

        protected virtual void Start()
        {
            Respawn();
        }

        public void Damage(Entity entity, float damage)
        {
            currentHealth -= damage;
            healthUpdated?.Invoke(-damage);
            if (currentHealth <= 0)
            {
                Die(entity);
            }
        }

        public virtual void Respawn()
        {
            if (stats != null)
            {
                currentHealth = stats.GetMaxHealth();
                healthUpdated?.Invoke(currentHealth);
            }
            alive = true;
        }

        protected virtual void Die(Entity entity)
        {
            foreach (Ability ability in abilities)
            {
                if (ability.GetToggleState())
                {
                    ability.UseAbility(null, null);
                }
            }
            Debug.Log(entity.name + " killed " + name);
            alive = false;
            EntityPool.Instance.ReturnObject(objectPoolKey, this);
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
            actions[0]?.DoAction(null, this, null);
        }

        public void DoAction(int index)
        {
            actions[index]?.DoAction(null, this, null);
        }

        public void DoAction(Entity target)
        {
            actions[0]?.DoAction(target, this, null);
        }

        public void DoAction(int index, Entity target)
        {
            actions[index]?.DoAction(target, this, null);
        }



        //ACCESSORS

        public float GetCurrentHealth()
        {
            return currentHealth;
        }

        public bool IsAlive()
        {
            return alive;
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

        public Stats GetStats()
        {
            return stats;
        }

        public string GetObjectPoolKey()
        {
            return objectPoolKey;
        }
    }
}
