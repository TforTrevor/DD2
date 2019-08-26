using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MEC;

public class EnemeyTarget : MonoBehaviour
{
    public Transform target;
    TowerStats towerStats;
    [SerializeField] string targetTag = "Enemy";

    [SerializeField] float checkRate;

    void Start()
    {
        towerStats = GetComponent<TowerStats>();
        Timing.RunCoroutine(FindTargetRoutine().CancelWith(gameObject));
    }

    IEnumerator<float> FindTargetRoutine()
    {
        while (true)
        {
            if (target == null)
            {
                //Get enemies in range
                List<Transform> transforms = GetEnemiesInRange(towerStats.range, towerStats.radius);

                //Get the index of the enemy closest to the tower
                //Set target
                target = GetClosestEnemy(transforms);

                //Exit routine
                //if (target != null)
                //{
                //    break;
                //}
            }
            yield return Timing.WaitForSeconds(checkRate);
        }
    }

    Transform GetClosestEnemy(List<Transform> transforms)
    {
        if (transforms.Count < 1)
        {
            return null;
        }

        float smallestDistance = -1;
        int index = 0;
        for (int i = 0; i < transforms.Count; i++)
        {
            float distance = Vector3.Distance(transform.position, transforms[i].position);
            //No previous smallest distance
            if (smallestDistance < 0)
            {
                smallestDistance = distance;
                index = i;
            }
            else if (distance < smallestDistance)
            {
                smallestDistance = distance;
                index = i;
            }
        }

        return transforms[index];
    }

    List<Transform> GetEnemiesInRange(float range, float radius)
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, range);
        List<Transform> transforms = new List<Transform>();
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].transform.CompareTag(targetTag))
            {
                float enemyDot = Vector3.Dot(transform.forward, colliders[i].transform.position - transform.position);
                float desiredDot = Mathf.Cos((Mathf.Deg2Rad * radius) / 2f);
                if (enemyDot > desiredDot)
                {
                    transforms.Add(colliders[i].transform);
                }
            }
        }
        return transforms;
    }
}
