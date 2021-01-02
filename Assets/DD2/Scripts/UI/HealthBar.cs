using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using MEC;

namespace DD2.UI
{
    public class HealthBar : FloatingUI
    {
        [SerializeField] bool showText;
        [SerializeField] Image foregroundFill;
        [SerializeField] Image backgroundFill;
        [SerializeField] TextMeshProUGUI healthNumber;
        [SerializeField] float backgroundDelay;

        Entity entity;
        CoroutineHandle damageHandle;
        bool isEnabled;

        protected override void Awake()
        {
            base.Awake();
            if (showText)
            {
                healthNumber.gameObject.SetActive(true);
            }
            else
            {
                healthNumber.gameObject.SetActive(false);
            }
            isEnabled = true;
        }

        public virtual void Show(Entity entity)
        {
            if (!isEnabled)
            {
                if (entity != null)
                {
                    this.entity = entity;
                    this.entity.healthUpdated += UpdateHealth;
                    float fillAmount = entity.CurrentHealth / entity.Stats.MaxHealth;
                    foregroundFill.fillAmount = fillAmount;
                    backgroundFill.fillAmount = fillAmount;
                }

                isEnabled = true;
            }
        }

        public override void Hide()
        {
            base.Hide();

            if (isEnabled)
            {
                if (entity != null)
                {
                    entity.healthUpdated -= UpdateHealth;
                }

                Timing.KillCoroutines(damageHandle);
                isEnabled = false;
            }
        }

        void OnDestroy()
        {
            if (entity != null)
            {
                entity.healthUpdated -= UpdateHealth;
            }
        }

        void LateUpdate()
        {
            if (isEnabled && entity != null)
            {
                Vector3 elementPosition = Camera.main.WorldToScreenPoint(entity.transform.position);
                transform.position = elementPosition;
                if (elementPosition.z > 0)
                {
                    CanvasGroup.alpha = 1;
                }
                else
                {
                    CanvasGroup.alpha = 0;
                }
            }
        }

        void UpdateHealth(object sender, float amount)
        {
            float fillAmount = entity.CurrentHealth / entity.Stats.MaxHealth;
            foregroundFill.fillAmount = fillAmount;
            healthNumber.text = (int)entity.CurrentHealth + "/" + entity.Stats.MaxHealth;

            Timing.KillCoroutines(damageHandle);
            damageHandle = Timing.CallDelayed(backgroundDelay, () =>
            {
                LeanTween.value(backgroundFill.fillAmount, foregroundFill.fillAmount, 1f).setOnUpdate((value) =>
                {
                    if (backgroundFill != null)
                    {
                        backgroundFill.fillAmount = value;
                    }
                });
            });
        }
    }
}