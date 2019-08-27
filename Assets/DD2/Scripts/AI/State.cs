using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

namespace DD2.AI
{
    [CreateAssetMenu(menuName = "Scriptable Objects/AI/State")]
    public class State : ScriptableObject
    {
        [SerializeField] [ReorderableList] [Expandable] Action[] enterActions;
        [SerializeField] [ReorderableList] [Expandable] Action[] updateActions;
        [SerializeField] [ReorderableList] [Expandable] Action[] exitActions;
        [SerializeField] [ReorderableList] Transition[] transitions;
        public Transition transition;

        public void EnterState(StateController controller)
        {
            DoActions(enterActions, controller);
        }
        public void UpdateState(StateController controller)
        {
            DoActions(updateActions, controller);
            CheckTransitions(controller);
        }
        public void ExitState(StateController controller)
        {
            DoActions(exitActions, controller);
        }

        void DoActions(Action[] actions, StateController controller)
        {
            for (int i = 0; i < actions.Length; i++)
            {
                actions[i].Act(controller);
            }
        }

        void CheckTransitions(StateController controller)
        {
            for (int i = 0; i < transitions.Length; i++)
            {
                bool decisionSucceeded = transitions[i].decision.Decide(controller);
                if (decisionSucceeded)
                {
                    if (controller.TransitionToState(transitions[i].trueState))
                        return;
                }
                else
                {
                    if (controller.TransitionToState(transitions[i].falseState))
                        return;
                }
            }
        }
    }
}