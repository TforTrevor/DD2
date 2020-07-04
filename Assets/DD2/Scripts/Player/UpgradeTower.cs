using MEC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DD2.AI;
using UnityEngine.VFX;

namespace DD2
{
    public class UpgradeTower : MonoBehaviour
    {
        [SerializeField] Player player;
        [SerializeField] int manaCost;
        [SerializeField] float upgradeTime;
        [SerializeField] LayerMask layerMask;
        [SerializeField] VisualEffect upgradeEffect;

        CoroutineHandle upgradeHandle;
        bool isUpgrading;

        void Start()
        {
            isUpgrading = false;
        }

        public void Begin()
        {
            if (isUpgrading)
            {
                Timing.KillCoroutines(upgradeHandle);
                isUpgrading = false;
            }
            else if (player.CurrentMana >= manaCost)
            {
                RaycastHit hit;
                if (Physics.Raycast(LevelManager.Instance.Camera.transform.position, LevelManager.Instance.Camera.transform.forward, out hit, 5f, layerMask))
                {
                    if (!hit.collider.isTrigger)
                    {
                        Tower tower = hit.transform.GetComponent<Tower>();
                        if (tower != null)
                        {
                            upgradeHandle = Timing.RunCoroutine(UpgradeRoutine(tower));
                            isUpgrading = true;
                        }
                    }
                }
            }
        }

        IEnumerator<float> UpgradeRoutine(Tower tower)
        {
            player.SpendMana(manaCost);
            tower.UpgradeEffect.SendEvent("OnPlay");            
            yield return Timing.WaitForSeconds(upgradeTime);
            tower.UpgradeEffect.SendEvent("OnStop");
            tower.Upgrade();            
        }
    }
}