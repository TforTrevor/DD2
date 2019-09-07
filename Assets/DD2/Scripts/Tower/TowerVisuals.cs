using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DD2.AI;

namespace DD2.Tower
{
    public class TowerVisuals : MonoBehaviour
    {
        AIStatus status;
        [SerializeField] Transform towerGraphics;
        [SerializeField] Transform towerVertical;

        void Awake()
        {
            status = GetComponent<AIStatus>();
        }

        void Update()
        {
            if (status == null)
            {
                return;
            }

            if (status.target != null)
            {
                Vector3 direction = (status.target.position - transform.position).normalized;
                Vector3 horizontal = Vector3.Scale(direction, new Vector3(1, 0, 1));
                Vector3 vertical = (status.target.position - towerVertical.position).normalized;
                towerGraphics.rotation = Quaternion.LookRotation(horizontal);
                towerVertical.rotation = Quaternion.LookRotation(vertical);
            }
            else
            {
                towerGraphics.forward = transform.forward;
                towerVertical.forward = transform.forward;
            }
        }
    }
}