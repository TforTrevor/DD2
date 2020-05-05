using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DD2.Actions
{
    public class Heal : Action
    {
        [SerializeField] HealType healType;
        [SerializeField] float amount;

        public override void DoAction(Entity target, Entity caller, object payload)
        {
            if (healType == HealType.Flat)
                Flat(target, caller, payload);
            else if (healType == HealType.PercentMaxHealth)
                PercentMaxHealth(target, caller, payload);
        }

        void Flat(Entity target, Entity caller, object payload)
        {
            target.Heal(caller, amount);
        }

        void PercentMaxHealth(Entity target, Entity caller, object payload)
        {
            float amount = target.GetStats().GetMaxHealth() * this.amount;
            target.Heal(caller, amount);
        }

        enum HealType
        {
            Flat,
            PercentMaxHealth
        }
    }
}