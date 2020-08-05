using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DD2.AI;
using DD2.Actions;
using MEC;

namespace DD2
{
    public class RepairTower : TopDownCursor
    {
        [SerializeField] LayerMask repairMask;
        [SerializeField] float healAmount;
        [SerializeField] float manaMultiplier;

        CoroutineHandle repairHandle;
        bool isRepairing;

        protected override void Start()
        {
            base.Start();
            isRepairing = false;
        }

        protected override void Action()
        {
            if (isRepairing)
            {
                Timing.KillCoroutines(repairHandle);
                isRepairing = false;
            }
            else
            {
                RaycastHit hit;
                if (Physics.Raycast(new Vector3(cursor.position.x, LevelManager.Instance.Camera.transform.position.y, cursor.position.z), Vector3.down, out hit, 1000, repairMask))
                {
                    if (!hit.collider.isTrigger)
                    {
                        Tower tower = hit.transform.GetComponent<Tower>();
                        if (tower != null)
                        {
                            repairHandle = Timing.RunCoroutine(RepairRoutine(tower));
                            isRepairing = true;
                            Cancel();
                        }
                    }
                }
            }
        }

        public override void Cancel()
        {
            base.Cancel();
            player.ToggleUpgrade(true);
        }

        protected override void Begin()
        {
            base.Begin();
            player.ToggleUpgrade(false);
        }

        IEnumerator<float> RepairRoutine(Tower tower)
        {
            while (tower.CurrentHealth < tower.Stats.MaxHealth)
            {
                if (player.CurrentMana >= healAmount * Time.deltaTime)
                {
                    tower.Heal(player, healAmount * Time.deltaTime);
                    player.SpendMana(Mathf.CeilToInt(healAmount * manaMultiplier * Time.deltaTime));
                }
                else
                {
                    break;
                }                
                yield return Timing.WaitForOneFrame;
            }
            isRepairing = false;
        }
    }
}