using DD2.AI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DD2
{
    public class SellTower : TopDownCursor
    {
        [SerializeField] LayerMask sellMask;

        protected override void Action()
        {
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

        public override void Cancel()
        {
            base.Cancel();
            player.ToggleRepair(true);
        }

        protected override void Begin()
        {
            base.Begin();
            player.ToggleRepair(false);
        }
    }
}