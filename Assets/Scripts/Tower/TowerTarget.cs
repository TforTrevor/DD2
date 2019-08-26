using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MEC;
using SmartData.SmartEvent;

public class TowerTarget : MonoBehaviour
{
    public Transform target;
    TowerStats towerStats;
    [SerializeField] string targetTag = "Enemy";

    [SerializeField] float findRate;
    [SerializeField] float checkRate;

    [SerializeField] EventDispatcher foundTarget;

    void Start()
    {
        towerStats = GetComponent<TowerStats>();
        Timing.RunCoroutine(FindTargetRoutine().CancelWith(gameObject));
    }

    IEnumerator<float> CheckTargetRoutine()
    {
        while (true)
        {
            yield return Timing.WaitForSeconds(checkRate);

            if (Vector3.Distance(towerStats.GetPosition(), target.position) <= towerStats.range)
            {
                float enemyDot = Vector3.Dot(towerStats.GetForward(), target.position - towerStats.GetPosition());
                float desiredDot = Mathf.Cos((Mathf.Deg2Rad * towerStats.radius) / 2f);
                if (enemyDot < desiredDot)
                {
                    target = null;
                }
            }
            else
            {
                target = null;
            }

            //Exit routine
            if (target == null)
            {
                Timing.RunCoroutine(FindTargetRoutine().CancelWith(gameObject));
                yield break;
            }
        }
    }

    IEnumerator<float> FindTargetRoutine()
    {
        while (true)
        {
            yield return Timing.WaitForSeconds(findRate);

            //Get enemies in range
            List<Transform> transforms = GetEnemiesInRange(towerStats.range, towerStats.radius);

            //Get the index of the enemy closest to the tower
            //Set target
            target = GetClosestEnemy(transforms);

            //Exit routine
            if (target != null)
            {
                foundTarget.Dispatch();
                Timing.RunCoroutine(CheckTargetRoutine().CancelWith(gameObject));
                yield break;
            }
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
            float distance = Vector3.Distance(towerStats.GetPosition(), transforms[i].position);
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
        Collider[] colliders = Physics.OverlapSphere(towerStats.GetPosition(), range);
        List<Transform> transforms = new List<Transform>();
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].transform.CompareTag(targetTag))
            {
                float enemyDot = Vector3.Dot(towerStats.GetForward(), colliders[i].transform.position - towerStats.GetPosition());
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
