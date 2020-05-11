using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using DD2.Abilities;
using DD2.Actions;
using System;

namespace DD2
{
    public class Entity : MonoBehaviour
    {
        [SerializeField] protected string objectPoolKey;
        [SerializeField] private Stats stats;
        [SerializeField] protected float currentHealth;
        [SerializeField] protected int currentMana;
        [SerializeField] protected float radius;
        [ReadOnly] [SerializeField] protected bool alive;

        [SerializeField] protected Transform fireTransform;
        [ReadOnly] [SerializeField] protected bool grounded;
        [ReadOnly] [SerializeField] protected bool ragdolled;
        [SerializeField] [ReorderableList] protected Ability[] abilities;

        public event EventHandler<float> healthUpdated;
        public event EventHandler<float> manaUpdated;
        public event EventHandler<Entity> onDeath;
        protected Rigidbody rb;

        public float Radius { get => radius; private set => radius = value; }
        public Stats Stats { get => stats; private set => stats = value; }

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
            if (alive)
            {
                currentHealth -= damage;
                healthUpdated?.Invoke(this, -damage);
                if (currentHealth <= 0)
                {
                    Die(entity);
                }
            }            
        }

        public void Heal(Entity entity, float amount)
        {
            currentHealth += amount;
            if (currentHealth > Stats.MaxHealth)
            {
                currentHealth = Stats.MaxHealth;
            }
            healthUpdated?.Invoke(this, amount);
        }

        public void GiveMana(int amount)
        {
            currentMana += amount;
            if (currentMana > Stats.MaxMana)
            {
                currentMana = Stats.MaxMana;
            }
            manaUpdated?.Invoke(this, amount);
        }

        public void SpendMana(int amount)
        {
            currentMana -= amount;
            if (currentMana < 0)
            {
                currentMana = 0;
            }
            manaUpdated?.Invoke(this, amount);
        }

        public virtual void Respawn()
        {
            if (Stats != null)
            {
                currentHealth = Stats.MaxHealth;
                healthUpdated?.Invoke(this, currentHealth);
                manaUpdated?.Invoke(this, currentMana);
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
            List<ManaOrb> manaOrbs = ((ManaOrbPool)ManaOrbPool.Instance).GetManaOrbs(currentMana);
            foreach (ManaOrb orb in manaOrbs)
            {
                orb.transform.position = GetPosition() + Vector3.up;
                orb.gameObject.SetActive(true);
                orb.Burst(3f);
            }
            if (entity != null)
            {
                Debug.Log(entity.name + " killed " + name);
            }
            else
            {
                Debug.Log("null killed " + name);
            }            
            alive = false;
            onDeath?.Invoke(this, this);
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


        //ACCESSORS

        public float GetCurrentHealth()
        {
            return currentHealth;
        }

        public int GetCurrentMana()
        {
            return currentMana;
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

        public string GetObjectPoolKey()
        {
            return objectPoolKey;
        }
    }
}
