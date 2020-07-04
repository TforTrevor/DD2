﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Apex.AI.Components;
using UnityEngine.VFX;

namespace DD2.AI
{
    public class Tower : EntityAI
    {
        [SerializeField] int manaCost;
        [SerializeField] protected Transform towerGraphics;
        [SerializeField] Transform towerVertical;
        [SerializeField] protected Transform towerSummonGraphics;
        [SerializeField] Color errorColor = Color.red;
        [SerializeField] Light summonLight;
        [SerializeField] protected MeshRenderer summonRenderer;
        [SerializeField] new Collider collider;
        [SerializeField] bool buildOnStart;
        [SerializeField] VisualEffect upgradeEffect;
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
            towerSummonGraphics.gameObject.SetActive(false);
            towerGraphics.gameObject.SetActive(true);
            if (collider != null)
            {
                collider.isTrigger = false;
            }            
            if (aiComponent != null)
            {
                aiComponent.enabled = true;
            }                
            IsAlive = true;
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
                Vector3 direction = (target.GetPosition() - transform.position).normalized;
                Vector3 horizontal = Vector3.Scale(direction, new Vector3(1, 0, 1));
                Vector3 vertical = (target.GetPosition() - towerVertical.position).normalized;
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
    }
}