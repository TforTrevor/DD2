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
        [SerializeField] [ReorderableList] List<Tower> towerPrefabs;
        [SerializeField] LayerMask mask;
        [SerializeField] Stage stage;
        [SerializeField] float rotSensitivity;
        [SerializeField] LayerMask overlapMask;
        [SerializeField] bool enableBuild;
        [SerializeField] bool isUsing;

        Tower instance;
        Collider instanceCollider;
        CoroutineHandle handle;

        new Transform camera;
        Player player;
        int index;

        public void Begin(BuildTowerInfo info)
        {
            if (!isUsing)
            {
                camera = info.camera;
                player = info.player;
                index = info.index;
                stage = Stage.position;
                isUsing = true;
                enableBuild = true;
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
                EntityPool.Instance.ReturnObject(instance.GetObjectPoolKey(), instance);
                if (stage == Stage.rotation)
                {
                    player.ToggleLook(true);
                    player.ToggleMovement(true);
                }
                camera = null;
                player = null;
                index = 0;
                isUsing = false;
            }
        }

        void Position()
        {
            instance = (Tower)EntityPool.Instance.GetObject(towerPrefabs[index].GetObjectPoolKey());
            if (instance != null)
            {
                instanceCollider = instance.GetComponent<Collider>();

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
            instance = null;
            isUsing = false;
        }

        IEnumerator<float> MoveRoutine()
        {
            while (true)
            {
                RaycastHit hit;
                if (Physics.Raycast(camera.position, camera.forward, out hit, 5, mask))
                {
                    instance.transform.position = hit.point;
                    CollisionCheck();
                }
                else
                {
                    instance.transform.position = camera.position + camera.forward * 5;
                    instance.SetColor(instance.GetErrorColor());
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
            Collider[] colliders = Physics.OverlapSphere(instanceCollider.bounds.center, Util.Utilities.GetLargestDimension(instanceCollider.bounds.extents), overlapMask);
            foreach (Collider collider in colliders)
            {
                if (collider != instanceCollider && instanceCollider.bounds.Intersects(collider.bounds))
                {
                    instance.SetColor(instance.GetErrorColor());
                    enableBuild = false;
                    return;
                }
            }
            instance.SetColor(instance.GetDefaultColor());
            enableBuild = true;
        }

        enum Stage { position, rotation, build }
    }
}
