using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DD2.AI;
using DD2.Actions;
using MEC;

namespace DD2
{
    public class RepairTower : MonoBehaviour
    {
        [SerializeField] Player player;
        [SerializeField] new Transform camera;
        [SerializeField] LayerMask layerMask;
        [SerializeField] float healAmount;

        CoroutineHandle handle;
        bool isRepairing;

        void Start()
        {
            isRepairing = false;
        }

        public void Begin()
        {
            if (isRepairing)
            {
                Timing.KillCoroutines(handle);
                isRepairing = false;
            }
            else
            {
                RaycastHit hit;
                if (Physics.Raycast(camera.transform.position, camera.forward, out hit, 5f, layerMask))
                {
                    Tower tower = hit.transform.GetComponent<Tower>();
                    if (tower != null)
                    {
                        handle = Timing.RunCoroutine(RepairRoutine(tower));
                        isRepairing = true;
                    }
                }
            }            
        }

        IEnumerator<float> RepairRoutine(Tower tower)
        {
            while (tower.GetCurrentHealth() < tower.Stats.MaxHealth)
            {
                if (player.GetCurrentMana() >= healAmount * Time.deltaTime)
                {
                    tower.Heal(player, healAmount * Time.deltaTime);
                    player.SpendMana(Mathf.CeilToInt(healAmount * Time.deltaTime));
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