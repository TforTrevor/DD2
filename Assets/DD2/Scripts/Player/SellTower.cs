using DD2.AI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace DD2
{
    public class SellTower : TopDownCursor
    {
        [SerializeField] LayerMask sellMask;

        protected override void OnEnable()
        {
            base.OnEnable();
            InputManager.Instance.Actions.Player.SellTower.performed += OnSell;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            InputManager.Instance.Actions.Player.SellTower.performed -= OnSell;
        }

        void OnSell(InputAction.CallbackContext context)
        {
            if (context.ReadValueAsButton())
            {
                Begin();
            }
        }

        protected override void Continue()
        {
            base.Continue();
            RaycastHit hit;
            if (Physics.Raycast(new Vector3(cursor.position.x, Camera.main.transform.position.y, cursor.position.z), Vector3.down, out hit, 1000, sellMask))
            {
                if (!hit.collider.isTrigger)
                {
                    Tower tower = hit.transform.GetComponent<Tower>();
                    if (tower != null)
                    {
                        tower.Sell(player);
                        Cancel();
                    }
                }
            }
        }
    }
}