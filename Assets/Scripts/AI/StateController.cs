using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MEC;

namespace DD2.AI
{
    public class StateController : MonoBehaviour
    {
        [SerializeField] State currentState;
        [SerializeField] State destroyState;
        [SerializeField] State remainState;
        [SerializeField] float updateRate;

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

        public void TransitionToState(State nextState)
        {
            if (nextState != remainState)
            {
                currentState.ExitState(this);
                currentState = nextState;
                currentState.EnterState(this);
            }
        }

        void OnDestroy()
        {
            TransitionToState(destroyState);
        }
    }
}

