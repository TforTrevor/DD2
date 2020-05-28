using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace DD2.AI 
{
    public class Trap : Tower
    {
        [SerializeField] protected Collider trigger;
        [SerializeField] LayerMask triggerLayerMask;
        List<Collider> triggerColliders = new List<Collider>();        

        protected override void Start()
        {
            base.Start();
            transform.localScale *= Stats.AttackRange;
        }

        protected override void Update()
        {
            
        }

        public override void Build()
        {
            base.Build();
            trigger.enabled = false;
            trigger.enabled = true;
        }

        protected void OnTriggerEnter(Collider other)
        {
            if (IsAlive)
            {
                if (Util.Utilities.IsInLayer(other.gameObject, triggerLayerMask))
                {
                    triggerColliders.Add(other);
                    UseAbility(true);
                }                
            }            
        }

        protected void OnTriggerExit(Collider other)
        {
            if (IsAlive)
            {
                triggerColliders.Remove(other);
                if (triggerColliders.Count < 1)
                {
                    UseAbility(false);
                }
            }            
        }

        protected override void OnKilledEntity(Entity entity)
        {
            foreach (Collider collider in entity.Colliders)
            {
                triggerColliders.Remove(collider);
            }
            if (triggerColliders.Count < 1)
            {
                UseAbility(false);
            }
        }

        void UseAbility(bool value)
        {
            if (!abilities[0].GetToggleState() && value)
            {
                abilities[0].UseAbility(this, null);
            }
            else if (abilities[0].GetToggleState() && !value)
            {
                abilities[0].UseAbility(this, null);
            }
        }
    }
}