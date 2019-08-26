using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DD2.AI.Decisions
{
    [CreateAssetMenu(menuName = "Scriptable Objects/AI/Decisions/Test Decision")]
    public class TestDecision : Decision
    {
        public override bool Decide(StateController controller)
        {
            if (controller.enemyStats.GetCurrentHealth() < controller.enemyStats.GetMaxHealth())
            {
                return true;
            }
            return false;
        }
    }
}