using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DD2.AI;

using MEC;
using UnityAtoms.BaseAtoms;

namespace DD2
{
    public class BuildTower : TopDownCursor
    {
        [SerializeField] LayerMask buildMask;
        [SerializeField] Stage stage;
        [SerializeField] float rotSensitivity;
        [SerializeField] LayerMask towerCollisionMask;
        [SerializeField] LayerMask trapCollisionMask;

        bool enableBuild;
        bool isUsing;
        Tower instance;
        List<Collider> instanceColliders = new List<Collider>();
        CoroutineHandle handle;
        Tower tower;

        public bool Begin(Tower tower)
        {
            if (!isUsing && tower.ManaCost <= player.CurrentMana)
            {
                Toggle();
                stage = Stage.position;
                isUsing = true;
                enableBuild = true;
                this.tower = tower;
                Continue(false);
                return true;
            }
            return false;
        }

        public void Continue(bool value)
        {
            //Only call on mouse up event
            if (enableBuild && isUsing && !value)
            {
                if (stage == Stage.position)
                {
                    Position();
                }
                else if (stage == Stage.rotation)
                {
                    Timing.KillCoroutines(handle);
                    Rotation();
                }
                else if (stage == Stage.build)
                {
                    Timing.KillCoroutines(handle);
                    Build();
                }
                stage++;
            }
        }

        public override void Cancel()
        {
            base.Cancel();
            if (isUsing && instance != null)
            {
                Timing.KillCoroutines(handle);
                EntityPool.Instance.ReturnObject(instance.ObjectPoolKey, instance);
                player.ToggleLook(true);
                player.ToggleMovement(true);
                isUsing = false;
                tower = null;
                instance = null;
            }
        }

        void Position()
        {
            instance = (Tower)EntityPool.Instance.GetObject(tower.ObjectPoolKey);
            if (instance != null)
            {
                instanceColliders.Clear();
                instanceColliders.AddRange(instance.GetComponents<Collider>());

                instance.gameObject.SetActive(true);
                handle = Timing.RunCoroutine(MoveRoutine());
            }
        }

        void Rotation()
        {
            player.ToggleLook(false);
            player.ToggleMovement(false);
            handle = Timing.RunCoroutine(RotateRoutine());
        }

        void Build()
        {
            instance.Build();
            player.SpendMana(tower.ManaCost);
            player.ToggleLook(true);
            player.ToggleMovement(true);
            isUsing = false;
            tower = null;
            instance = null;
            base.Cancel();
        }

        IEnumerator<float> MoveRoutine()
        {
            while (true)
            {
                RaycastHit hit;
                if (Physics.Raycast(new Vector3(cursor.position.x, LevelManager.Instance.Camera.transform.position.y, cursor.position.z), Vector3.down, out hit, 1000, buildMask))
                {
                    instance.transform.position = hit.point;
                    CollisionCheck();
                }
                else
                {
                    //instance.transform.position = LevelManager.Instance.Camera.transform.position + LevelManager.Instance.Camera.transform.forward * 5;
                    instance.SetColor(instance.ErrorColor);
                    enableBuild = false;
                }
                instance.transform.localEulerAngles = new Vector3(0, player.transform.localEulerAngles.y, 0);
                yield return Timing.WaitForOneFrame;
            }
        }

        IEnumerator<float> RotateRoutine()
        {
            while (true)
            {
                instance.transform.Rotate(Vector3.up, lookInput.Value.x * Time.deltaTime * rotSensitivity);
                CollisionCheck();
                yield return Timing.WaitForOneFrame;
            }
        }

        void CollisionCheck()
        {
            LayerMask mask = towerCollisionMask;
            if (instance.GetType() == typeof(Trap))
            {
                mask = trapCollisionMask;
            }
            Collider[] colliders = Physics.OverlapSphere(instanceColliders[0].bounds.center, Util.Utilities.GetLargestDimension(instanceColliders[0].bounds.extents), mask);
            foreach (Collider collider in colliders)
            {
                if (!instanceColliders.Contains(collider) && instanceColliders[0].bounds.Intersects(collider.bounds))
                {
                    instance.SetColor(instance.ErrorColor);
                    enableBuild = false;
                    return;
                }
            }
            instance.SetColor(instance.DefaultColor);
            enableBuild = true;
        }

        enum Stage { position, rotation, build }
    }
}
