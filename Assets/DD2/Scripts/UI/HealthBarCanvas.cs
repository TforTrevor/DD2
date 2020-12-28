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

        Dictionary<Collider, HealthBar> healthBarObjects = new Dictionary<Collider, HealthBar>();
        Dictionary<Collider, Entity> entities = new Dictionary<Collider, Entity>();

        //Add and remove colliders from dictionary
        void FixedUpdate()
        {

            Collider[] colliders = Physics.OverlapSphere(LevelManager.Instance.Camera.transform.position, range, healthBarMask);
            Dictionary<Collider, HealthBar> copy = new Dictionary<Collider, HealthBar>(healthBarObjects);

            //Remove colliders from dictionary
            foreach (KeyValuePair<Collider, HealthBar> pair in copy)
            {
                if (!Utilities.ArrayContains(colliders, pair.Key))
                {
                    HideElement(pair.Value);
                    healthBarObjects.Remove(pair.Key);
                    entities.Remove(pair.Key);
                }
            }

            //Add colliders to dictionary
            foreach (Collider collider in colliders)
            {
                if (!healthBarObjects.ContainsKey(collider))
                {
                    HealthBar uiElement = ShowElement();

                    Vector3 elementPosition = LevelManager.Instance.Camera.WorldToScreenPoint(collider.transform.position);
                    uiElement.transform.position = elementPosition;

                    healthBarObjects.Add(collider, uiElement);
                    Entity entity = collider.GetComponent<Entity>();
                    entities.Add(collider, entity);
                }
            }
        }

        void LateUpdate()
        {
            foreach (KeyValuePair<Collider, HealthBar> pair in healthBarObjects)
            {
                Vector3 elementPosition = LevelManager.Instance.Camera.WorldToScreenPoint(pair.Key.transform.position);
                pair.Value.transform.position = elementPosition;
                if (elementPosition.z < 0)
                {
                    pair.Value.ToggleVisible(false, entities[pair.Key]);
                }
                else
                {
                    pair.Value.ToggleVisible(true, entities[pair.Key]);
                }
            }
        }
    }
}