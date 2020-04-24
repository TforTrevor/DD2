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
        [SerializeField] protected Stats stats;
        [SerializeField] protected float currentHealth;

        [SerializeField] protected Transform fireTransform;
        [ReadOnly] [SerializeField] protected bool grounded;
        [ReadOnly] [SerializeField] protected bool ragdolled;
        [SerializeField] [ReorderableList] protected Action[] actions;
        [SerializeField] [ReorderableList] protected Ability[] abilities;

        public static event System.Action<Entity> EntityEnabled = delegate { };
        public static event System.Action<Entity> EntityDisabled = delegate { };
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
            if (stats != null)
            {
                currentHealth = stats.GetMaxHealth();
                healthUpdated?.Invoke(currentHealth);
            }
        }

        protected virtual void OnEnable()
        {
            EntityEnabled?.Invoke(this);
        }

        protected virtual void OnDisable()
        {
            EntityDisabled?.Invoke(this);
        }

        public void Damage(Entity entity, float damage)
        {
            currentHealth -= damage;
            healthUpdated?.Invoke(-damage);
            if (currentHealth < 0)
            {
                Die(entity);
            }
        }

        protected virtual void Die(Entity entity)
        {
            Debug.Log(entity.name + " killed " + name);
            Destroy(gameObject);
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

        public float GetAttackAngle()
        {
            return stats.GetAttackAngle();
        }

        public float GetSearchRange()
        {
            return stats.GetSearchRange();
        }

        public float GetSearchAngle()
        {
            return stats.GetSearchAngle();
        }

        public Stats GetStats()
        {
            return stats;
        }
    }
}
