using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

namespace DD2.UI
{
    public class FloatingUICanvas : MonoBehaviour
    {
        [SerializeField] HealthBar healthBarPrefab;
        [SerializeField] float maxDistance;
        [SerializeField] float maxSizeDistance;
        [SerializeField] string poolKey;
        [SerializeField] LayerMask layerMask;

        Dictionary<Transform, FloatingUI> floatingUI = new Dictionary<Transform, FloatingUI>();
        Dictionary<Transform, FloatingUI> overlap = new Dictionary<Transform, FloatingUI>();
        Dictionary<Transform, FloatingUI> remove = new Dictionary<Transform, FloatingUI>();
        Collider[] colliders;
        int colliderCount;

        void Start()
        {
            colliders = new Collider[FloatingUIPool.Instance.GetCount(poolKey)];
        }

        void FixedUpdate()
        {
            overlap.Clear();
            Util.Utilities.ClearArray(colliders, colliderCount);
            colliderCount = Physics.OverlapSphereNonAlloc(LevelManager.Instance.Camera.transform.position, maxDistance + 1, colliders, layerMask);
            for (int i = 0; i < colliderCount; i++)
            {
                Collider collider = colliders[i];
                if (floatingUI.ContainsKey(collider.transform))
                {
                    FloatingUI floating = floatingUI[collider.transform];
                    if (floating != null && !overlap.ContainsKey(collider.transform))
                    {
                        overlap.Add(collider.transform, floating);
                    }
                }
                else
                {
                    if (!overlap.ContainsKey(collider.transform))
                    {
                        overlap.Add(collider.transform, GetFloatingUI(collider.transform));
                    }
                }
            }
            remove.Clear();
            foreach (KeyValuePair<Transform, FloatingUI> entry in floatingUI)
            {
                if (!overlap.ContainsKey(entry.Key))
                {
                    remove.Add(entry.Key, entry.Value);
                }
            }
            foreach (KeyValuePair<Transform, FloatingUI> entry in remove)
            {
                FloatingUIPool.Instance.ReturnObject(poolKey, floatingUI[entry.Key]);
                floatingUI.Remove(entry.Key);
            }
            foreach (KeyValuePair<Transform, FloatingUI> entry in overlap)
            {
                if (entry.Value != null && !floatingUI.ContainsKey(entry.Key))
                {
                    floatingUI.Add(entry.Key, entry.Value);
                }
            }
        }

        void LateUpdate()
        {
            foreach (KeyValuePair<Transform, FloatingUI> entry in floatingUI)
            {
                Transform transform = entry.Key;
                FloatingUI floating = entry.Value;

                Vector3 pos = LevelManager.Instance.Camera.WorldToScreenPoint(transform.position + Vector3.up * floating.HeightOffset);
                if (pos.z > maxDistance || pos.z < 0)
                {
                    floating.Canvas.enabled = false;
                }
                else
                {
                    floating.Canvas.enabled = true;
                    floating.Canvas.sortingOrder = (int)(pos.z * -10);
                    floating.transform.position = pos;
                    float ratio = Util.Utilities.Remap(pos.z, maxDistance, maxSizeDistance, 0, 1);
                    floating.transform.localScale = new Vector3(ratio, ratio, 1);
                }
            }
        }

        protected virtual FloatingUI GetFloatingUI(Transform transform)
        {
            Entity entity = transform.GetComponent<Entity>();
            if (entity != null)
            {
                HealthBar healthbar = (HealthBar)FloatingUIPool.Instance.GetObject(poolKey);
                if (healthbar != null)
                {
                    healthbar.SetEntity(entity);
                    return healthbar;
                }                
            }
            return null;
        }

        void OnDestroy()
        {
            //Entity.EntityEnabled -= AddHealthBar;
            //Entity.EntityDisabled -= RemoveHealthBar;
        }
    }
}