﻿using System.Collections;
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

        Dictionary<Entity, HealthBar> healthBars = new Dictionary<Entity, HealthBar>();

        void Awake()
        {
            Entity.EntityEnabled += AddHealthBar;
            Entity.EntityDisabled += RemoveHealthBar;
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

                Vector3 pos = Camera.main.WorldToScreenPoint(entity.GetPosition() + Vector3.up * healthBar.GetHeightOffset());
                if (pos.z > maxDistance || pos.z < 0)
                {
                    if (healthBar != null)
                    {
                        healthBar.GetCanvas().enabled = false;
                        HealthBarPool.Instance.ReturnObject(poolKey, healthBar);
                    }                    
                }
                else
                {
                    if (healthBar == null)
                    {
                        healthBar = HealthBarPool.Instance.GetObject(poolKey);
                    }
                    if (healthBar != null)
                    {
                        healthBar.GetCanvas().enabled = true;
                        healthBar.GetCanvas().sortingOrder = (int)(pos.z * -10);
                        healthBar.transform.position = pos;
                        float ratio = Util.Utilities.Remap(pos.z, maxDistance, maxSizeDistance, 0, 1);
                        healthBar.transform.localScale = new Vector3(ratio, ratio, 1);
                    }                    
                }
            }
        }

        void OnDestroy()
        {
            Entity.EntityEnabled -= AddHealthBar;
            Entity.EntityDisabled -= RemoveHealthBar;
        }
    }
}