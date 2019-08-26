using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MEC;

namespace DD2.AI
{
    public class StateController : MonoBehaviour
    {
        [SerializeField] [Expandable] State currentState;
        [SerializeField] State destroyState;
        public State remainState;
        [SerializeField] float updateRate;

        [HideInInspector] public EnemyStats enemyStats;

        void Awake()
        {
            enemyStats = GetComponent<EnemyStats>();
        }

        void Start()
        {
            currentState.EnterState(this);
            Timing.RunCoroutine(UpdateRoutine().CancelWith(gameObject));
        }

        IEnumerator<float> UpdateRoutine()
        {
            while (true)
            {
                currentState.UpdateState(this);
                yield return Timing.WaitForSeconds(updateRate);
            }
        }

        public bool TransitionToState(State nextState)
        {
            if (nextState != remainState)
            {
                currentState.ExitState(this);
                currentState = nextState;
                currentState.EnterState(this);
                return true;
            }
            return false;
        }

        void OnDestroy()
        {
            TransitionToState(destroyState);
        }
    }
}

