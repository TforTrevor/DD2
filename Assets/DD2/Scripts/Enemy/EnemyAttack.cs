using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MEC;

namespace DD2.Enemy
{
    public class EnemyAttack : MonoBehaviour
    {
        Status status;
        CoroutineHandle fireHandle;

        void Awake()
        {
            status = GetComponent<Status>();
        }

        void StartFiring()
        {
            fireHandle = Timing.RunCoroutine(FireRoutine().CancelWith(gameObject));
        }

        void StopFiring()
        {
            Timing.KillCoroutines(fireHandle);
        }

        IEnumerator<float> FireRoutine()
        {
            while (true)
            {
                Debug.Log("Attacking");
                yield return Timing.WaitForSeconds(status.stats.GetAttackRate());
            }
        }
    }
}