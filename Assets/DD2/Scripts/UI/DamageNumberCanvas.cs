using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DD2.Util;

namespace DD2.UI
{
    public class DamageNumberCanvas : FloatingUICanvas<DamageNumber>
    {
        [SerializeField] float range;
        [SerializeField] LayerMask damageMask;

        Dictionary<Collider, Entity> entities = new Dictionary<Collider, Entity>();

        void FixedUpdate()
        {
            Collider[] colliders = Physics.OverlapSphere(LevelManager.Instance.Camera.transform.position, range, damageMask);
            Dictionary<Collider, Entity> copy = new Dictionary<Collider, Entity>(entities);
            
            //Remove entities
            foreach (KeyValuePair<Collider, Entity> pair in copy)
            {
                if (!Utilities.ArrayContains(colliders, pair.Key))
                {
                    Entity entity = pair.Value;
                    entity.healthUpdated -= HealthUpdated;
                    entities.Remove(pair.Key);
                }
            }

            //Add entities
            foreach (Collider collider in colliders)
            {
                if (!entities.ContainsKey(collider))
                {
                    Entity entity = collider.GetComponent<Entity>();
                    entity.healthUpdated += HealthUpdated;
                    entities.Add(collider, entity);
                }
            }
        }

        void HealthUpdated(object sender, float difference)
        {
            DamageNumber damageNumber = GetElement();
            Entity entity = (Entity)sender;
            damageNumber.ToggleVisible(true, difference, entity.transform.position);
        }
    }
}
