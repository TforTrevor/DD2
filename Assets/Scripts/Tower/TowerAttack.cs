using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MEC;

public class TowerAttack : MonoBehaviour
{
    TowerStats towerStats;
    TowerTarget towerTarget;

    [SerializeField] bool reloading;

    void Start()
    {
        towerStats = GetComponent<TowerStats>();
        towerTarget = GetComponent<TowerTarget>();
    }

    IEnumerator<float> ReloadRoutine()
    {
        while (true)
        {
            reloading = true;
            yield return Timing.WaitForSeconds(1f / towerStats.attackSpeed);
            reloading = false;
            if (towerTarget.target)
            {
                Fire();
            }
            yield break;
        }
    }

    public void Fire()
    {
        if (!reloading)
        {
            GameObject projectile = Instantiate(towerStats.projectile);
            projectile.transform.position = towerStats.GetFirePosition();
            TowerProjectile towerProjectile = projectile.GetComponent<TowerProjectile>();
            towerProjectile.target = towerTarget.target;

            Timing.RunCoroutine(ReloadRoutine().CancelWith(gameObject));
        }
    }
}
