using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using MEC;
using DD2.Actions;

namespace DD2.Abilities
{
    public class HitboxAbility : Ability
    {
        [SerializeField] [BoxGroup("Hitboxes")]
        protected float hitboxTickRate;
        [SerializeField] [BoxGroup("Hitboxes")]
        protected bool showHitbox;
        [SerializeField] [ReorderableList] [BoxGroup("Hitboxes")]
        protected Hitbox[] hitboxes;
        [SerializeField] [ReorderableList] [Expandable]
        protected Action[] oneTimeActions;
        [SerializeField] [ReorderableList]
        protected ParticleSystem[] particleSystems;

        protected Collider[] collisions;
        protected int collisionCount;

        CoroutineHandle abilityHandle;

        protected override void Awake()
        {
            base.Awake();
            int maxCollisions = 0;
            //GameObject parentObject = new GameObject(name);
            foreach (Hitbox hitbox in hitboxes)
            {
                if (hitbox.MaxCollisions > maxCollisions)
                {
                    maxCollisions = hitbox.MaxCollisions;
                }
                //hitbox.HitboxObject.transform.parent = parentObject.transform;
            }
            collisions = new Collider[maxCollisions];
        }

        protected override void Tick(Entity target, object payload)
        {
            base.Tick(target, payload);
            Timing.KillCoroutines(abilityHandle);
            abilityHandle = Timing.RunCoroutine(AbilityRoutine(target, payload));
        }

        IEnumerator<float> AbilityRoutine(Entity target, object payload)
        {
            foreach (Hitbox hitbox in hitboxes)
            {
                yield return Timing.WaitForSeconds(hitbox.Delay);
                CoroutineHandle hitboxHandle = Timing.RunCoroutine(HitboxRoutine(target, payload, hitbox));
                yield return Timing.WaitForSeconds(hitbox.Duration);
                Timing.KillCoroutines(hitboxHandle);
            }
        }

        IEnumerator<float> HitboxRoutine(Entity target, object payload, Hitbox hitbox)
        {
            while (true)
            {
                Util.Utilities.ClearArray(collisions, collisionCount);
                collisionCount = hitbox.GetCollisionNonAlloc(target.GetPosition(), LayerMask, collisions);
                HitboxTick(target, hitbox.transform.position, collisions);
                yield return hitboxTickRate;
            }
        }

        protected virtual void HitboxTick(Entity target, object payload, Collider[] colliders)
        {
            if (colliders[0] != null)
            {
                for (int i = 0; i < collisionCount; i++)
                {
                    Collider collider = colliders[i];
                    Entity entity = collider.GetComponent<Entity>();
                    if (entity != null)
                    {
                        ApplyEffects(entity, payload);
                    }
                }
                foreach (Action action in oneTimeActions)
                {
                    action.DoAction(target, entity, payload);
                }
                foreach (ParticleSystem system in particleSystems)
                {
                    system.Play();
                }
            }
        }
    }
}