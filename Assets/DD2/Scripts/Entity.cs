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
        [SerializeField] private string objectPoolKey;
        [SerializeField] private Stats stats;
        [SerializeField] private float currentHealth;
        [SerializeField] private int currentMana;
        [SerializeField] private float radius;
        [ReadOnly] [SerializeField] private bool isAlive;

        [SerializeField] private Transform fireTransform;
        [SerializeField] private Transform eyeTransform;
        [ReadOnly] [SerializeField] private bool isGrounded;
        [SerializeField] Animator animator;
        [SerializeField] [ReorderableList] protected Ability[] abilities;

        public event EventHandler<float> healthUpdated;
        public event EventHandler<float> manaUpdated;
        public event EventHandler<Entity> onDeath;
        protected Rigidbody rb;
        private Collider[] colliders;
        protected InstancedStats instancedStats;

        public float Radius { get => radius; protected set => radius = value; }
        public InstancedStats Stats { get => instancedStats; protected set => instancedStats = value; }
        public string ObjectPoolKey { get => objectPoolKey; protected set => objectPoolKey = value; }
        public bool IsAlive { get => isAlive; protected set => isAlive = value; }
        public float CurrentHealth { get => currentHealth; protected set => currentHealth = value; }
        public int CurrentMana { get => currentMana; protected set => currentMana = value; }
        public bool IsGrounded { get => isGrounded; protected set => isGrounded = value; }
        public Collider[] Colliders { get => colliders; protected set => colliders = value; }
        public Animator Animator { get => animator; private set => animator = value; }
        public Transform FireTransform { get => fireTransform; private set => fireTransform = value; }
        public Vector3 EyePosition { get => eyeTransform != null ? eyeTransform.position : transform.position; }

        protected virtual void Awake()
        {
            rb = GetComponent<Rigidbody>();
            Colliders = GetComponents<Collider>();
            for (int i = 0; i < abilities.Length; i++)
            {
                abilities[i].SetEntity(this);
            }
            if (stats != null)
                Stats = new InstancedStats(stats);
        }

        protected virtual void Start()
        {
            Respawn();
        }

        public void Damage(Entity entity, float damage)
        {
            if (IsAlive)
            {
                CurrentHealth -= damage;
                healthUpdated?.Invoke(this, -damage);
                if (CurrentHealth <= 0)
                {
                    Die(entity);
                }
            }
        }

        protected virtual void OnKilledEntity(Entity entity)
        {

        }

        public void Heal(Entity entity, float amount)
        {
            CurrentHealth += amount;
            if (CurrentHealth > Stats.MaxHealth)
            {
                CurrentHealth = Stats.MaxHealth;
            }
            healthUpdated?.Invoke(this, amount);
        }

        public void GiveMana(int amount)
        {
            CurrentMana += amount;
            if (CurrentMana > Stats.MaxMana)
            {
                CurrentMana = Stats.MaxMana;
            }
            manaUpdated?.Invoke(this, amount);
        }

        public void SpendMana(int amount)
        {
            CurrentMana -= amount;
            if (CurrentMana < 0)
            {
                CurrentMana = 0;
            }
            manaUpdated?.Invoke(this, amount);
        }

        public virtual void Respawn()
        {
            if (Stats != null)
            {
                CurrentHealth = Stats.MaxHealth;
                healthUpdated?.Invoke(this, CurrentHealth);
                manaUpdated?.Invoke(this, CurrentMana);
            }
            IsAlive = true;
        }

        protected virtual void Die(Entity entity)
        {
            foreach (Ability ability in abilities)
            {
                if (ability.ToggleState)
                {
                    ability.UseAbility(null, null);
                }
            }
            List<ManaOrb> manaOrbs = ((ManaOrbPool)ManaOrbPool.Instance).GetManaOrbs(CurrentMana);
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
            IsAlive = false;
            onDeath?.Invoke(this, this);
            entity?.OnKilledEntity(this);
            EntityPool.Instance.ReturnObject(ObjectPoolKey, this);
        }

        public virtual Vector3 GetVelocity()
        {
            return rb.velocity;
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
            if (value && !IsGrounded)
            {
                IsGrounded = value;
                OnGrounded();
            } 
            else
            {
                IsGrounded = value;
            }
        }

        protected virtual void OnGrounded()
        {
            
        }


        //ACCESSORS

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
    }
}
