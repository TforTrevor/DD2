using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DD2.AI.Actions
{
    [CreateAssetMenu(menuName = "Scriptable Objects/AI/Actions/Send Message")]
    public class SendMessage : Action
    {
        [SerializeField] string message;
        public override void Act(StateController controller)
        {
            if (!string.IsNullOrWhiteSpace(message))
            {
                controller.SendMessage(message);
            }
        }
    }
}