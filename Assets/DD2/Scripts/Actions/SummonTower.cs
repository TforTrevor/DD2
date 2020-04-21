using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MEC;
using DD2.SOArchitecture;
using DD2.AI;

namespace DD2.Actions
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Actions/Summon Tower")]
    public class SummonTower : Action
    {
        [SerializeField] Tower tower;
        [SerializeField] LayerMask mask;
        [SerializeField] CancelAction cancelAction;
        [SerializeField] Stage stage;
        [SerializeField] float sensitivity;

        GameObject instance;
        CoroutineHandle handle;

        public override void DoAction(Transform target, Entity entity, Vector3 position)
        {
            if (entity == target)
            {
                stage = Stage.none;
            }
            if (stage == Stage.none)
            {
                stage = Stage.position;
                Position(target, entity, position);
                Player player = (Player)entity;
                player.SetPrimaryFire(this);
                cancelAction.action = this;
                player.SetSecondaryFire(cancelAction);
                player.SetAbility1(null);
            }
            else if (stage == Stage.position)
            {
                stage = Stage.rotation;
                Timing.KillCoroutines(handle);
                Rotation(target, entity, position);
            }
            else if (stage == Stage.rotation)
            {
                stage = Stage.build;
                Timing.KillCoroutines(handle);
                Build(target, entity, position);
            }
            else if (stage == Stage.build)
            {
                
            }
        }

        public override void Cancel(Transform target, Entity entity, Vector3 position)
        {
            Player player = (Player)entity;
            Timing.KillCoroutines(handle);
            Destroy(instance);
            player.SetPrimaryFire(null);
            player.SetSecondaryFire(null);
            player.SetAbility1(this);
            if (stage == Stage.rotation)
            {
                player.ToggleLook(true);
                player.ToggleMovement(true);
            }
            stage = Stage.none;
        }

        private void Position(Transform target, Entity entity, Vector3 position)
        {
            Player player = (Player)entity;
            instance = Instantiate(tower.gameObject);
            handle = Timing.RunCoroutine(MoveRoutine(target));
        }

        private void Rotation(Transform target, Entity entity, Vector3 position)
        {
            Player player = (Player)entity;
            player.ToggleLook(false);
            player.ToggleMovement(false);
            handle = Timing.RunCoroutine(RotateRoutine(player.GetLookInput()));
        }

        private void Build(Transform target, Entity entity, Vector3 position)
        {
            Player player = (Player)entity;
            instance.GetComponent<Tower>().Build();
            player.SetPrimaryFire(null);
            player.SetSecondaryFire(null);
            player.SetAbility1(this);
            player.ToggleLook(true);
            player.ToggleMovement(true);
            stage = Stage.none;
        }

        IEnumerator<float> MoveRoutine(Transform camera)
        {
            while (true)
            {
                RaycastHit hit;
                if (Physics.Raycast(camera.position, camera.forward, out hit, 5, mask))
                {
                    instance.transform.position = hit.point;
                }
                yield return Timing.WaitForOneFrame;
            }       
        }

        IEnumerator<float> RotateRoutine(Vector3Variable mouseInput)
        {
            while (true)
            {
                instance.transform.Rotate(Vector3.up, mouseInput.Value.x * Time.deltaTime * sensitivity);
                yield return Timing.WaitForOneFrame;
            }
        }

        void OnEnable()
        {
            stage = Stage.none;
        }

        void OnDisable()
        {
            stage = Stage.none;
        }

        enum Stage { none, position, rotation, build }
    }
}