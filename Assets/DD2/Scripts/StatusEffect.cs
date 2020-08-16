using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DD2
{
    public enum StatusEffect
    {
        None = 0,
        Stunned = 1 << 0,
        Frozen = 1 << 1,
        Burned = 1 << 2,
        Slowed = 1 << 3
    }
}