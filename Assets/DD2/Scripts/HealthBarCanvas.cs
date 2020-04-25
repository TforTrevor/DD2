using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

namespace DD2
{
    public class HealthBarCanvas : MonoBehaviour
    {
        [SerializeField] HealthBar healthBarPrefab;
        [SerializeField] float maxDistance;
        [SerializeField] float maxSizeDistance;
        [SerializeField] string poolKey;
        [SerializeField] LayerMask layerMask;
        [SerializeField] int maxHealthBars;

        Dictionary<Entity, HealthBar> healthBars = new Dictionary<Entity, HealthBar>();
        Dictionary<Entity, HealthBar> healthbarUpdate = new Dictionary<Entity, HealthBar>();
        Dictionary<Entity, HealthBar> overlap = new Dictionary<Entity, HealthBar>();
        Collider[] colliders;
        int colliderCount;

        void Awake()
        {
            colliders = new Collider[maxHealthBars];
        }

        void FixedUpdate()
        {
            overlap.Clear();
            Util.Utilities.ClearArray(colliders, colliderCount);
            colliderCount = Physics.OverlapSphereNonAlloc(Camera.main.transform.position, maxDistance + 1, colliders, layerMask);
            for (int i = 0; i < colliderCount; i++)
            {
                Entity entity = colliders[i].GetComponent<Entity>();
                if (entity != null)
                {
                    if (healthBars.ContainsKey(entity))
                    {
                        HealthBar h = healthBars[entity];
                        if (h != null)
                        {
                            overlap.Add(entity, h);
                        }
                    }
                    else
                    {
                        overlap.Add(entity, null);
                    }
                }                           
            }
            foreach (KeyValuePair<Entity, HealthBar> entry in healthBars)
            {
                if (!overlap.ContainsKey(entry.Key) && entry.Value != null)
                {
                    HealthBarPool.Instance.ReturnObject(poolKey, entry.Value);
                }
            }
            healthBars.Clear();
            foreach (KeyValuePair<Entity, HealthBar> entry in overlap)
            {
                healthBars.Add(entry.Key, entry.Value);
            }
        }

        void AddHealthBar(Entity entity)
        {
            if (!healthBars.ContainsKey(entity))
            {
                healthBars.Add(entity, null);
            }
        }

        void RemoveHealthBar(Entity entity)
        {
            if (healthBars.ContainsKey(entity))
            {
                if (healthBars[entity] != null)
                {
                    HealthBarPool.Instance.ReturnObject(poolKey, healthBars[entity]);
                }
                healthBars.Remove(entity);
            }
        }

        void LateUpdate()
        {
            foreach (KeyValuePair<Entity, HealthBar> entry in healthBars)
            {
                Entity entity = entry.Key;
                HealthBar healthBar = entry.Value;

                if (healthBar == null)
                {
                    healthBar = HealthBarPool.Instance.GetObject(poolKey);
                    healthBar.SetEntity(entity);
                }
                if (healthBar != null)
                {
                    Vector3 pos = Camera.main.WorldToScreenPoint(entity.GetPosition() + Vector3.up * healthBar.GetHeightOffset());
                    if (pos.z > maxDistance || pos.z < 0)
                    {
                        HealthBarPool.Instance.ReturnObject(poolKey, healthBar);
                    }
                    else
                    {             
                        healthBar.GetCanvas().sortingOrder = (int)(pos.z * -10);
                        healthBar.transform.position = pos;
                        float ratio = Util.Utilities.Remap(pos.z, maxDistance, maxSizeDistance, 0, 1);
                        healthBar.transform.localScale = new Vector3(ratio, ratio, 1);
                    }
                    healthbarUpdate.Add(entity, healthBar);
                }
            }
            foreach (KeyValuePair<Entity, HealthBar> entry in healthbarUpdate)
            {
                healthBars[entry.Key] = entry.Value;
            }
            healthbarUpdate.Clear();
        }

        void OnDestroy()
        {
            //Entity.EntityEnabled -= AddHealthBar;
            //Entity.EntityDisabled -= RemoveHealthBar;
        }
    }
}