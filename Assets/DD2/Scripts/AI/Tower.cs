using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Apex.AI.Components;
using UnityEngine.VFX;
using NaughtyAttributes;
using MEC;

namespace DD2.AI
{
    public class Tower : EntityAI
    {
        [BoxGroup("Tower")] [SerializeField] bool buildOnStart;
        [BoxGroup("Tower")] [SerializeField] int manaCost;
        [BoxGroup("Tower")] [SerializeField] float buildTime;
        [BoxGroup("Tower")] [SerializeField] protected Transform towerGraphics;
        [BoxGroup("Tower")] [SerializeField] Transform towerVertical;
        [BoxGroup("Tower")] [SerializeField] protected Transform towerSummonGraphics;
        [BoxGroup("Tower")] [SerializeField] Light buildLight;
        [BoxGroup("Tower")] [SerializeField] protected MeshRenderer buildRenderer;
        [BoxGroup("Tower")] [SerializeField] new Collider collider;
        [BoxGroup("Tower")] [SerializeField] VisualEffect upgradeEffect;
        [BoxGroup("Tower")] [SerializeField] VisualEffect buildEffect;

        protected UtilityAIComponent aiComponent;
        protected int level = 0;

        int shaderBuildProgress;
        int shaderBuildTime;
        int shaderIsError;
        int shaderColor;
        int shaderErrorColor;
        int shaderHeight;
        CoroutineHandle upgradeHandle;

        public int ManaCost { get => manaCost; private set => manaCost = value; }

        protected override void Awake()
        {
            base.Awake();
            aiComponent = GetComponent<UtilityAIComponent>();
            shaderBuildProgress = Shader.PropertyToID("_BuildProgress");
            shaderBuildTime = Shader.PropertyToID("_BuildTime");
            shaderIsError = Shader.PropertyToID("_IsError");
            shaderColor = Shader.PropertyToID("_Color");
            shaderErrorColor = Shader.PropertyToID("_ErrorColor");
            shaderHeight = Shader.PropertyToID("_Height");
        }

        protected override void Start()
        {
            base.Start();
            if (collider != null)
            {
                collider.isTrigger = true;
            }
            if (buildOnStart)
            {
                Build();
            }
        }

        public override void Respawn()
        {
            base.Respawn();
            towerGraphics.gameObject.SetActive(false);
            towerSummonGraphics.gameObject.SetActive(true);
            aiComponent.enabled = false;
            IsAlive = false;
        }

        protected override void Die(Entity entity)
        {
            base.Die(entity);
            Timing.KillCoroutines(upgradeHandle);
        }

        public virtual void Sell(Player player)
        {
            player.GiveMana(Mathf.CeilToInt(CurrentHealth / Stats.MaxHealth * manaCost));
            Die(null);
        }

        public virtual void Build()
        {
            Timing.RunCoroutine(BuildRoutine());
        }

        public virtual void Upgrade()
        {
            upgradeHandle = Timing.RunCoroutine(UpgradeRoutine());
        }

        protected virtual void Update()
        {
            if (context != null && context.target != null)
            {
                Entity target = context.target;
                Vector3 direction = (target.transform.position - transform.position).normalized;
                Vector3 horizontal = Vector3.Scale(direction, new Vector3(1, 0, 1));
                Vector3 vertical = (target.transform.position - towerVertical.position).normalized;
                towerGraphics.rotation = Quaternion.LookRotation(horizontal);
                towerVertical.rotation = Quaternion.LookRotation(vertical);
            }
            else
            {
                towerGraphics.forward = transform.forward;
                towerVertical.forward = transform.forward;
            }
        }

        public void SetError(bool value)
        {
            if (buildLight != null & buildRenderer != null)
            {
                if (value)
                {
                    buildLight.color = buildRenderer.material.GetColor(shaderErrorColor);
                    buildRenderer.material.SetInt(shaderIsError, 1);
                }
                else
                {
                    buildLight.color = buildRenderer.material.GetColor(shaderColor);
                    buildRenderer.material.SetInt(shaderIsError, 0);
                }
            }                    
        }

        IEnumerator<float> BuildRoutine()
        {
            if (collider != null) collider.isTrigger = false;

            buildRenderer.material.SetFloat(shaderHeight, buildRenderer.bounds.size.y);
            buildRenderer.material.SetFloat(shaderBuildTime, buildTime);

            if (buildEffect != null) buildEffect.SendEvent("OnPlay");
            float currentTime = 0;

            while (currentTime < buildTime)
            {
                buildRenderer.material.SetFloat(shaderBuildProgress, currentTime);
                yield return Timing.WaitForOneFrame;
                currentTime += Time.deltaTime;
            }

            if (buildEffect != null) buildEffect.SendEvent("OnStop");

            towerSummonGraphics.gameObject.SetActive(false);
            towerGraphics.gameObject.SetActive(true);
            aiComponent.enabled = true;
            IsAlive = true;
        }

        IEnumerator<float> UpgradeRoutine()
        {
            upgradeEffect.SendEvent("OnPlay");
            yield return Timing.WaitForSeconds(buildTime);
            Stats.TowerLevel();
            level++;
            upgradeEffect.SendEvent("OnStop");
        }

        public override void AddForce(Vector3 force, ForceMode forceMode) { }

        public override void ClearVelocity(bool x, bool y, bool z) { }
    }
}