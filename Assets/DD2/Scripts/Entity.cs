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
        [SerializeField] protected int currentMana;
        [ReadOnly] [SerializeField] protected bool alive;

        [SerializeField] protected Transform fireTransform;
        [ReadOnly] [SerializeField] protected bool grounded;
        [ReadOnly] [SerializeField] protected bool ragdolled;
        [SerializeField] [ReorderableList] protected Ability[] abilities;

        public delegate void UpdateHealthHandler(float amount);
        public event UpdateHealthHandler healthUpdated;
        public event UpdateHealthHandler manaUpdated;
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

        public void Heal(Entity entity, float amount)
        {
            currentHealth += amount;
            if (currentHealth > stats.GetMaxHealth())
            {
                currentHealth = stats.GetMaxHealth();
            }
            healthUpdated?.Invoke(amount);
        }

        public void GiveMana(int amount)
        {
            currentMana += amount;
            if (currentMana > GetStats().GetMaxMana())
            {
                currentMana = GetStats().GetMaxMana();
            }
            manaUpdated?.Invoke(amount);
        }

        public void SpendMana(int amount)
        {
            currentMana -= amount;
            if (currentMana < 0)
            {
                currentMana = 0;
            }
            manaUpdated?.Invoke(amount);
        }

        public virtual void Respawn()
        {
            if (stats != null)
            {
                currentHealth = stats.GetMaxHealth();
                healthUpdated?.Invoke(currentHealth);
                manaUpdated?.Invoke(currentMana);
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
                Vector3 direction = new Vector3(Random.Range(-1f, 1f), Random.Range(-0.2f, 0.2f), Random.Range(-1f, 1f));
                orb.GetRigidbody().AddForce(direction.normalized * Random.Range(0f, 3f), ForceMode.Impulse);
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
