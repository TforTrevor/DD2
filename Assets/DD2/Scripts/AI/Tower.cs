using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Apex.AI.Components;

namespace DD2.AI
{
    public class Tower : EntityAI
    {
        [SerializeField] Transform towerGraphics;
        [SerializeField] Transform towerVertical;
        [SerializeField] Transform towerSummonGraphics;
        UtilityAIComponent aiComponent;

        protected override void Awake()
        {
            base.Awake();
            aiComponent = GetComponent<UtilityAIComponent>();
        }

        void Start()
        {
            towerGraphics.gameObject.SetActive(false);
            towerSummonGraphics.gameObject.SetActive(true);
            aiComponent.enabled = false;
        }

        public void Build()
        {
            towerSummonGraphics.gameObject.SetActive(false);
            towerGraphics.gameObject.SetActive(true);
            aiComponent.enabled = true;
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
    }
}