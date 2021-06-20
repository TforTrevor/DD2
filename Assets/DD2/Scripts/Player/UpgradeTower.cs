using MEC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DD2.AI;
using UnityEngine.VFX;
using UnityAtoms.BaseAtoms;

namespace DD2
{
    public class UpgradeTower : TopDownCursor
    {
        [SerializeField] int manaCost;
        [SerializeField] float upgradeTime;
        [SerializeField] LayerMask upgradeMask;

        CoroutineHandle upgradeHandle;

        protected override void Start()
        {
            base.Start();
        }

        protected override void Action()
        {
            player.ToggleRepair(false);
            if (player.CurrentMana >= manaCost)
            {
                RaycastHit hit;
                if (Physics.Raycast(new Vector3(cursor.position.x, Camera.main.transform.position.y, cursor.position.z), Vector3.down, out hit, 1000, upgradeMask))
                {
                    if (!hit.collider.isTrigger)
                    {
                        Tower tower = hit.transform.GetComponent<Tower>();
                        if (tower != null)
                        {
                            player.SpendMana(manaCost);
                            tower.Upgrade();
                            Cancel();
                        }
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