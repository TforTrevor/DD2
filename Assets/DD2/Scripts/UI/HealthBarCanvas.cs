using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DD2.Util;

namespace DD2.UI
{
    public class HealthBarCanvas : FloatingUICanvas<HealthBar>
    {
        [SerializeField] float range;
        [SerializeField] LayerMask healthBarMask;

        Transform currentTransform;
        Entity currentEntity;
        HealthBar currentHealthBar;

        //Add and remove colliders from dictionary
        void FixedUpdate()
        {
            RaycastHit hit;
            Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
            if (Physics.Raycast(ray, out hit, range, healthBarMask))
            {
                if (hit.transform != currentTransform)
                {
                    Entity newEntity = hit.transform.GetComponent<Entity>();
                    if (newEntity != null)
                    {
                        HideHealthBar();

                        currentTransform = hit.transform;
                        currentEntity = newEntity;
                        currentHealthBar = GetElement();
                        currentHealthBar.Show(currentEntity);
                    }
                }
            }
            else
            {
                HideHealthBar();
            }
        }

        void HideHealthBar()
        {
            if (currentHealthBar != null)
            {
                currentHealthBar.Hide();
                ReturnElement(currentHealthBar);

                currentHealthBar = null;
                currentTransform = null;
                currentEntity = null;
            }
        }
    }
}