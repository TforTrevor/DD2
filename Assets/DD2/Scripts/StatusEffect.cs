using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DD2
{
    public enum StatusEffect
    {
        None = 0,
        Stun = 1 << 0,
        Freeze = 1 << 1,
        Burn = 1 << 2,
        Slow = 1 << 3,
        Speed = 1 << 4,
        Root = 1 << 5,
        Fear = 1 << 6,
        Confuse = 1 << 7
    }

    //Poison, exponential DOT
    //Stat buff / debuff
    //Taunt
}