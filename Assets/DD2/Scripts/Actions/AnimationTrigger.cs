using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DD2.Actions
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Actions/Animation Trigger")]
    public class AnimationTrigger : Action
    {
        [SerializeField] string animationTrigger;
        [SerializeField] ApplyTo applyTo;

        public override void DoAction(Entity target, Entity caller, object payload)
        {
            if (applyTo == ApplyTo.Target)
            {
                target.Animator.SetTrigger(animationTrigger);
            }
            else
            {
                caller.Animator.SetTrigger(animationTrigger);
            }
        }

        enum ApplyTo { Target, Caller }
    }
}

