using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DD2
{
    public class EntityPool : ComponentPool<Entity>
    {
        public override Entity GetObject(string key)
        {
            Entity entity = base.GetObject(key);
            entity?.Respawn();
            return entity;
        }
    }
}