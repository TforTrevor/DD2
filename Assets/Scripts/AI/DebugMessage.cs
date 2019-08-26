using UnityEngine;

namespace DD2.AI.Actions
{
    [CreateAssetMenu(menuName = "Scriptable Objects/AI/Actions/Debug Message")]
    public class DebugMessage : Action
    {
        [SerializeField] string message;

        public override void Act(StateController controller)
        {
            if (!string.IsNullOrWhiteSpace(message))
            {
                Debug.Log(message);
            }
        }
    }
}

