using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DD2.UI
{
    public class DamageUICanvas : Singleton<DamageUICanvas>
    {
        [SerializeField] DamageUI uiPrefab;
        [SerializeField] float maxDistance;

        Dictionary<DamageUI, Entity> uiDictionary = new Dictionary<DamageUI, Entity>();

        public void ShowDamage(Entity entity, float damage)
        {
            DamageUI tempUI = DamageUIPool.Instance.GetObject(uiPrefab.PoolKey);
            tempUI.transform.SetParent(transform);
            uiDictionary.Add(tempUI, entity);
            tempUI.Play(damage);
        }

        void LateUpdate()
        {
            foreach (KeyValuePair<DamageUI, Entity> entry in uiDictionary)
            {
                Entity entity = entry.Value;
                DamageUI ui = entry.Key;

                Vector3 pos = LevelManager.Instance.Camera.WorldToScreenPoint(entity.EyePosition);
                if (pos.z > maxDistance || pos.z < 0)
                {
                    ui.gameObject.SetActive(false);
                }
                else
                {
                    ui.gameObject.SetActive(true);
                    ui.transform.position = pos;
                }
            }
        }

        public void Return(DamageUI ui)
        {
            uiDictionary.Remove(ui);
            DamageUIPool.Instance.ReturnObject(uiPrefab.PoolKey, ui);
        }
    }
}