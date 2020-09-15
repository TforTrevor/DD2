﻿using System.Collections;
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
        [BoxGroup("Tower")] [SerializeField] int buildTime;
        [BoxGroup("Tower")] [SerializeField] protected Transform towerGraphics;
        [BoxGroup("Tower")] [SerializeField] Transform towerVertical;
        [BoxGroup("Tower")] [SerializeField] protected Transform towerSummonGraphics;
        [BoxGroup("Tower")] [SerializeField] protected Material towerSummonMaterial;
        [BoxGroup("Tower")] [SerializeField] Color errorColor = Color.red;
        [BoxGroup("Tower")] [SerializeField] Light summonLight;
        [BoxGroup("Tower")] [SerializeField] protected MeshRenderer summonRenderer;
        [BoxGroup("Tower")] [SerializeField] new Collider collider;
        [BoxGroup("Tower")] [SerializeField] VisualEffect upgradeEffect;

        protected UtilityAIComponent aiComponent;
        protected int level = 0;
        Color defaultColor;
        Color currentColor;

        public Color DefaultColor { get => defaultColor; protected set => defaultColor = value; }
        public Color CurrentColor { get => currentColor; private set => currentColor = value; }
        public Color ErrorColor { get => errorColor; private set => errorColor = value; }
        public int ManaCost { get => manaCost; private set => manaCost = value; }
        public VisualEffect UpgradeEffect { get => upgradeEffect; private set => upgradeEffect = value; }

        protected override void Awake()
        {
            base.Awake();
            aiComponent = GetComponent<UtilityAIComponent>();
            if (summonRenderer != null)
            {
                DefaultColor = summonRenderer.material.GetColor("_Color");
            }
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
            if (aiComponent != null)
            {
                aiComponent.enabled = false;
            }
            IsAlive = false;
        }

        public override void AddForce(Vector3 force, ForceMode forceMode)
        {
            
        }

        public override void ClearVelocity(bool x, bool y, bool z)
        {
            
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
            Stats.TowerLevel();
            level++;
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

        public void SetColor(Color color)
        {
            if (color != CurrentColor)
            {
                if (summonLight != null)
                {
                    summonLight.color = color;
                }
                if (summonRenderer != null)
                {
                    summonRenderer.material.SetColor("_Color", color);
                }
                CurrentColor = color;
            }            
        }

        IEnumerator<float> BuildRoutine()
        {
            if (collider != null)
            {
                collider.isTrigger = false;
            }

            if (towerSummonMaterial != null)
            {
                towerSummonMaterial.SetFloat("_BuildTime", buildTime);
                towerSummonMaterial.SetFloat("_StartTime", Time.timeSinceLevelLoad);
            }           

            yield return Timing.WaitForSeconds(buildTime);

            towerSummonGraphics.gameObject.SetActive(false);
            towerGraphics.gameObject.SetActive(true);
            if (aiComponent != null)
            {
                aiComponent.enabled = true;
            }
            IsAlive = true;
        }
    }
}