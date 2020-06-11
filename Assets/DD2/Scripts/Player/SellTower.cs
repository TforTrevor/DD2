using DD2.AI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DD2
{
    public class SellTower : MonoBehaviour
    {
        [SerializeField] Player player;
        [SerializeField] LayerMask layerMask;

        bool enableRemove;

        public void Begin()
        {
            RaycastHit hit;
            if (Physics.Raycast(LevelManager.Instance.Camera.transform.position, LevelManager.Instance.Camera.transform.forward, out hit, 5f, layerMask))
            {
                Tower tower = hit.collider.GetComponent<Tower>();
                if (tower != null && tower.IsAlive)
                {
                    tower.Sell(player);
                }
            }
        }

        public void Continue()
        {
            if (enableRemove)
            {
                
            }
        }
    }
}