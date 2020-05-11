﻿using System.Collections;
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

        [SerializeField] protected Transform fireTransform;
        [ReadOnly] [SerializeField] protected bool grounded;
        [ReadOnly] [SerializeField] protected bool ragdolled;
        [SerializeField] [ReorderableList] protected Ability[] abilities;

        public event EventHandler<float> healthUpdated;
        public event EventHandler<float> manaUpdated;
        public event EventHandler<Entity> onDeath;
        protected Rigidbody rb;

        public float Radius { get => radius; protected set => radius = value; }
        public Stats Stats { get => stats; protected set => stats = value; }
        public string ObjectPoolKey { get => objectPoolKey; protected set => objectPoolKey = value; }
        public bool IsAlive { get => isAlive; protected set => isAlive = value; }
        public float CurrentHealth { get => currentHealth; protected set => currentHealth = value; }
        public int CurrentMana { get => currentMana; protected set => currentMana = value; }

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
                if (ability.GetToggleState())
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
            EntityPool.Instance.ReturnObject(ObjectPoolKey, this);
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
    }
}
