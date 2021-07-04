using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DD2.AI;
using DD2.Actions;
using MEC;
using UnityEngine.InputSystem;

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

        protected override void OnEnable()
        {
            base.OnEnable();
            InputManager.Instance.Actions.Player.RepairTower.performed += OnRepair;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            InputManager.Instance.Actions.Player.RepairTower.performed -= OnRepair;
        }

        void OnRepair(InputAction.CallbackContext context)
        {
            if (context.ReadValueAsButton())
            {
                Begin();
            }
        }

        protected override void Continue()
        {
            base.Continue();
            if (isRepairing)
            {
                Timing.KillCoroutines(repairHandle);
                isRepairing = false;
            }
            else
            {
                RaycastHit hit;
                if (Physics.Raycast(new Vector3(cursor.position.x, Camera.main.transform.position.y, cursor.position.z), Vector3.down, out hit, 1000, repairMask))
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