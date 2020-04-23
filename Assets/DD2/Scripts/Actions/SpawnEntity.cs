using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DD2.Actions
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Actions/Spawn Entity")]
    public class SpawnEntity : Action
    {
        [SerializeField] string key;
        [SerializeField] Vector3 offset;

        public override void DoAction(Entity target, Entity caller, object payload)
        {
            Entity entity = EntityPool.Instance.GetObject(key);
            if (entity != null)
            {
                entity.transform.rotation = Quaternion.LookRotation(caller.GetForward(), Vector3.up);
                entity.transform.position = caller.transform.TransformPoint(offset);
                entity.gameObject.SetActive(true);
            }            
        }
    }
}