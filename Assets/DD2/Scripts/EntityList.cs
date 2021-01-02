using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DD2.AI;

namespace DD2
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Entity List")]
    public class EntityList : ScriptableObject
    {
        List<Entity> entities;

        public List<Entity> Entities { get => entities; }

        void OnEnable()
        {
            entities = new List<Entity>();
        }
    }
}
