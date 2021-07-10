using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DD2.AI;
using MEC;
using UnityAtoms.BaseAtoms;
using UnityEngine.InputSystem;

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
        CoroutineHandle moveRotHandle;
        Tower tower;

        public bool Begin(Tower tower)
        {
            if (!isUsing && tower.ManaCost <= player.CurrentMana)
            {
                this.tower = tower;
                Begin();
                return true;
            }
            return false;
        }

        protected override void Begin()
        {
            base.Begin();

            stage = Stage.position;
            isUsing = true;
            enableBuild = true;
            
            Continue();
        }

        protected override void Continue()
        {
            base.Continue();

            if (enableBuild && isUsing)
            {
                if (stage == Stage.position)
                {
                    instance = (Tower)EntityPool.Instance.GetObject(tower.ObjectPoolKey);
                    if (instance != null)
                    {
                        instanceColliders.Clear();
                        instanceColliders.AddRange(instance.GetComponents<Collider>());

                        instance.gameObject.SetActive(true);
                        instance.ToggleRangeIndicator(true);
                        moveRotHandle = Timing.RunCoroutine(MoveRoutine());
                    }                    
                }
                else if (stage == Stage.rotation)
                {
                    Timing.KillCoroutines(moveRotHandle);

                    player.ToggleMovement(false);
                    moveRotHandle = Timing.RunCoroutine(RotateRoutine());
                }
                else if (stage == Stage.build)
                {
                    Timing.KillCoroutines(moveRotHandle);

                    instance.ToggleRangeIndicator(false);
                    instance.Build();
                    player.SpendMana(tower.ManaCost);
                    player.ToggleMovement(true);
                    isUsing = false;
                    tower = null;
                    instance = null;
                    base.Cancel();
                }
                stage++;
            }
        }

        protected override void Cancel()
        {
            base.Cancel();
            if (isUsing && instance != null)
            {
                Timing.KillCoroutines(moveRotHandle);
                EntityPool.Instance.ReturnObject(instance.ObjectPoolKey, instance);
                player.ToggleMovement(true);
                isUsing = false;
                tower = null;
                instance = null;
            }
        }

        IEnumerator<float> MoveRoutine()
        {
            while (true)
            {
                RaycastHit hit;
                if (Physics.Raycast(new Vector3(cursor.position.x, Camera.main.transform.position.y, cursor.position.z), Vector3.down, out hit, 1000, buildMask))
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
                instance.transform.Rotate(Vector3.up, lookInput.x * Time.deltaTime * rotSensitivity);
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
