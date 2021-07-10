using MEC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DD2.AI;
using UnityEngine.VFX;
using UnityAtoms.BaseAtoms;
using UnityEngine.InputSystem;

namespace DD2
{
    public class UpgradeTower : TopDownCursor
    {
        [SerializeField] int manaCost;
        [SerializeField] float upgradeTime;
        [SerializeField] LayerMask upgradeMask;

        CoroutineHandle upgradeHandle;
        bool isUpgrading;

        protected override void Start()
        {
            base.Start();
            isUpgrading = false;
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            InputManager.Instance.Actions.Player.UpgradeTower.performed += OnUpgrade;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            InputManager.Instance.Actions.Player.UpgradeTower.performed -= OnUpgrade;
        }

        void OnUpgrade(InputAction.CallbackContext context)
        {
            if (context.ReadValueAsButton())
            {
                Begin();
            }
        }

        protected override void Continue()
        {
            if (isUpgrading)
            {
                Timing.KillCoroutines(upgradeHandle);
                isUpgrading = false;
            }
            else if (player.CurrentMana >= manaCost)
            {
                RaycastHit hit;
                if (Physics.Raycast(new Vector3(cursor.position.x, Camera.main.transform.position.y, cursor.position.z), Vector3.down, out hit, 1000, upgradeMask))
                {
                    if (!hit.collider.isTrigger)
                    {
                        Tower tower = hit.transform.GetComponent<Tower>();
                        if (tower != null)
                        {
                            upgradeHandle = Timing.RunCoroutine(UpgradeRoutine(tower));
                            isUpgrading = true;
                            Cancel();
                        }
                    }
                }
            }
        }

        IEnumerator<float> UpgradeRoutine(Tower tower)
        {
            player.SpendMana(manaCost);
            //temporary, refactor upgrade effect code later
            VisualEffect effect = tower.UpgradeEffect;
            if (effect != null)
            {
                tower.UpgradeEffect.SendEvent("OnPlay");
                yield return Timing.WaitForSeconds(upgradeTime);
                tower.UpgradeEffect.SendEvent("OnStop");
            }            
            tower.Upgrade();            
        }
    }
}