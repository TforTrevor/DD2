using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RoboRyanTron.SearchableEnum;

namespace DD2.AI.Decisions
{
    [CreateAssetMenu(menuName = "Scriptable Objects/AI/Decisions/Return Bool")]
    public class ReturnBool : Decision
    {
        enum Bool { True, False };
        [SerializeField] [SearchableEnum] Bool value;
        public override bool Decide(StateController controller)
        {
            if (value == Bool.True)
            {
                return true;
            }
            return false;
        }
    }
}