using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerVisuals : MonoBehaviour
{
    TowerTarget towerTarget;
    [SerializeField] Transform towerGraphics;
    [SerializeField] Transform towerVertical;

    void Start()
    {
        towerTarget = GetComponent<TowerTarget>();
    }

    void Update()
    {
        if (towerTarget == null)
        {
            return;
        }

        if (towerTarget.target != null)
        {
            Vector3 direction = (towerTarget.target.position - transform.position).normalized;
            Vector3 horizontal = Vector3.Scale(direction, new Vector3(1, 0, 1));
            Vector3 vertical = (towerTarget.target.position - towerVertical.position).normalized;
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
