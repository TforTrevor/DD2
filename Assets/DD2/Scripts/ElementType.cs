using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DD2
{
    public enum ElementType
    {
        None = 0,
        Physical = 1 << 0,
        Fire = 1 << 1,
        Lightning = 1 << 2,
        Energy = 1 << 3,
        Water = 1 << 4,
        Ice = 1 << 5
    }
}
