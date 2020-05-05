using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Apex.AI.Components;

namespace DD2.AI
{
    public class Tower : EntityAI
    {
        [SerializeField] int manaCost;
        [SerializeField] Transform towerGraphics;
        [SerializeField] Transform towerVertical;
        [SerializeField] Transform towerSummonGraphics;
        [SerializeField] Color errorColor = Color.red;
        [SerializeField] Light summonLight;
        [SerializeField] MeshRenderer summonRenderer;
        UtilityAIComponent aiComponent;
        Color defaultColor;
        Color currentColor;

        protected override void Awake()
        {
            base.Awake();
            aiComponent = GetComponent<UtilityAIComponent>();
            if (summonRenderer != null)
            {
                defaultColor = summonRenderer.material.GetColor("_Color");
            }
        }

        public override void Respawn()
        {
            base.Respawn();
            towerGraphics.gameObject.SetActive(false);
            towerSummonGraphics.gameObject.SetActive(true);
            aiComponent.enabled = false;
            alive = false;
        }

        public void Build()
        {
            towerSummonGraphics.gameObject.SetActive(false);
            towerGraphics.gameObject.SetActive(true);
            aiComponent.enabled = true;
            alive = true;
        }

        void Update()
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
            if (color != currentColor)
            {
                if (summonLight != null)
                {
                    summonLight.color = color;
                }
                if (summonRenderer != null)
                {
                    summonRenderer.material.SetColor("_Color", color);
                }
                currentColor = color;
            }            
        }

        public Color GetDefaultColor()
        {
            return defaultColor;
        }

        public Color GetErrorColor()
        {
            return errorColor;
        }

        public int GetManaCost()
        {
            return manaCost;
        }
    }
}