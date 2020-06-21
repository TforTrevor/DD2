using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DD2.Actions
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Actions/Animation Trigger")]
    public class AnimationTrigger : Action
    {
        [SerializeField] string animationTrigger;

        public override void DoAction(Entity target, Entity caller, object payload)
        {
            target.Animator.SetTrigger(animationTrigger);
        }
    }
}

