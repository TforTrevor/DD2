using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Apex.AI.Components;
using UnityEngine.VFX;
using MEC;
using UnityEngine.InputSystem;

namespace DD2.AI
{
    public class Tower : EntityAI
    {
        [SerializeField] bool buildOnStart;
        [SerializeField] int manaCost;
        [SerializeField] int buildTime;
        [SerializeField] protected Transform towerGraphics;
        [SerializeField] Transform towerVertical;
        [SerializeField] protected Transform towerSummonGraphics;
        [SerializeField] Light summonLight;
        [SerializeField] protected MeshRenderer summonRenderer;
        [SerializeField] new Collider collider;
        [SerializeField] VisualEffect upgradeEffect;
        [SerializeField] VisualEffect buildEffect;
        [SerializeField] LineRenderer rangeLineRenderer;
        [SerializeField] EntityList towerList;

        protected UtilityAIComponent aiComponent;
        protected int level = 0;
        Color defaultColor;
        Color currentColor;
        Color errorColor = Color.red;

        public Color DefaultColor { get => defaultColor; protected set => defaultColor = value; }
        public Color CurrentColor { get => currentColor; private set => currentColor = value; }
        public Color ErrorColor { get => errorColor; }
        public int ManaCost { get => manaCost; private set => manaCost = value; }
        public VisualEffect UpgradeEffect { get => upgradeEffect; private set => upgradeEffect = value; }

        int progressShaderHash;


        protected override void Awake()
        {
            base.Awake();
            aiComponent = GetComponent<UtilityAIComponent>();
            if (summonRenderer != null)
            {
                DefaultColor = summonRenderer.material.GetColor("_Color");
                summonRenderer.material.SetFloat("_Height", summonRenderer.bounds.size.y);
                summonRenderer.transform.localScale *= 1.01f;
                summonRenderer.material.SetInt("_IsBuilt", 0);

                progressShaderHash = Shader.PropertyToID("_Progress");
                summonRenderer.material.SetFloat(progressShaderHash, 0);
            }
            if (buildEffect != null)
            {
                buildEffect.Stop();
                buildEffect.SetFloat("Height", summonRenderer.bounds.size.y);
            }

            CreateRangeLines();
            ToggleRangeIndicator(false);
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

        protected virtual void OnEnable()
        {
            InputManager.Instance.Actions.Player.ShowTowerRange.performed += OnShowRange;
        }

        protected virtual void OnDisable()
        {
            InputManager.Instance.Actions.Player.ShowTowerRange.performed -= OnShowRange;
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

        protected override void Die(Entity entity)
        {
            base.Die(entity);
            towerList.Entities.Remove(this);
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
            CreateRangeLines();
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

            if (buildEffect != null)
            {
                buildEffect.SetFloat("Progress", 0);
                buildEffect.Play();
            }            

            float buildProgress = 0;
            while (buildProgress < buildTime)
            {
                if (buildEffect != null)
                {
                    buildEffect.SetFloat("Progress", buildProgress / buildTime);
                }
                
                summonRenderer.material.SetFloat(progressShaderHash, buildProgress / buildTime);
                yield return Timing.WaitForOneFrame;
                buildProgress += Time.deltaTime;
            }

            if (buildEffect != null)
            {
                buildEffect.Stop();
            }
            
            towerGraphics.gameObject.SetActive(true);
            if (aiComponent != null)
            {
                aiComponent.enabled = true;
            }
            IsAlive = true;
            towerList.Entities.Add(this);

            summonRenderer.material.SetInt("_IsBuilt", 1);

            float hideProgress = 1;
            while (hideProgress > 0)
            {
                summonRenderer.material.SetFloat(progressShaderHash, hideProgress);
                yield return Timing.WaitForOneFrame;
                hideProgress -= Time.deltaTime * 2;
            }

            towerSummonGraphics.gameObject.SetActive(false);
        }

        void CreateRangeLines()
        {
            if (rangeLineRenderer != null)
            {
                int resolution = 16;
                rangeLineRenderer.positionCount = resolution + 2;
                rangeLineRenderer.SetPosition(0, Vector3.zero);
                for (int i = 0; i < resolution; i++)
                {
                    float tempAngle = -Stats.AttackAngle / 2;
                    tempAngle += i * Stats.AttackAngle / (resolution - 1);
                    Vector3 direction = Quaternion.AngleAxis(tempAngle, Vector3.up) * Vector3.forward;
                    Vector3 point = direction * Stats.AttackRange;
                    rangeLineRenderer.SetPosition(i + 1, point);
                }
                rangeLineRenderer.SetPosition(resolution + 1, Vector3.zero);
            }            
        }

        void OnShowRange(InputAction.CallbackContext context)
        {
            if (context.ReadValueAsButton())
            {
                ToggleRangeIndicator(true);
            }
            else
            {
                ToggleRangeIndicator(false);
            }
        }

        public void ToggleRangeIndicator(bool value)
        {
            if (rangeLineRenderer != null)
            {
                rangeLineRenderer.enabled = value;
            }            
        }
    }
}