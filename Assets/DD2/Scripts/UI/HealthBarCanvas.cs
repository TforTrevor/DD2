using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DD2.UI
{
    public class HealthBarCanvas : FloatingUICanvas
    {
        protected override FloatingUI GetFloatingUI(Transform transform)
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
    }
}