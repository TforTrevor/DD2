using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DD2.AI;
using NaughtyAttributes;
using MEC;
using DD2.SOArchitecture;

namespace DD2
{
    public class BuildTower : MonoBehaviour
    {
        [SerializeField] Player player;
        [SerializeField] LayerMask mask;
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

        public void Begin(Tower tower)
        {
            if (!isUsing && tower.ManaCost <= player.CurrentMana)
            {
                stage = Stage.position;
                isUsing = true;
                enableBuild = true;
                this.tower = tower;
                Continue();
            }         
        }

        public void Continue()
        {
            if (enableBuild && isUsing)
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

        public void Cancel()
        {
            if (isUsing && instance != null)
            {
                Timing.KillCoroutines(handle);
                EntityPool.Instance.ReturnObject(instance.ObjectPoolKey, instance);
                if (stage == Stage.rotation)
                {
                    player.ToggleLook(true);
                    player.ToggleMovement(true);
                }
                isUsing = false;
                tower = null;
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
            handle = Timing.RunCoroutine(RotateRoutine(player.GetLookInput()));
        }

        void Build()
        {
            instance.Build();
            player.ToggleLook(true);
            player.ToggleMovement(true);
            player.SpendMana(tower.ManaCost);
            instance = null;
            isUsing = false;
            tower = null;
        }

        IEnumerator<float> MoveRoutine()
        {
            while (true)
            {
                RaycastHit hit;
                if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 5, mask))
                {
                    instance.transform.position = hit.point;
                    CollisionCheck();
                }
                else
                {
                    instance.transform.position = Camera.main.transform.position + Camera.main.transform.forward * 5;
                    instance.SetColor(instance.ErrorColor);
                    enableBuild = false;
                }
                instance.transform.localEulerAngles = new Vector3(0, player.transform.localEulerAngles.y, 0);
                yield return Timing.WaitForOneFrame;
            }
        }

        IEnumerator<float> RotateRoutine(Vector3Variable mouseInput)
        {
            while (true)
            {
                instance.transform.Rotate(Vector3.up, mouseInput.Value.x * Time.deltaTime * rotSensitivity);
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
