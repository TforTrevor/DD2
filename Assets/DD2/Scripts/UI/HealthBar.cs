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

        public override void ToggleVisible(bool value)
        {
            ToggleVisible(value, null);
        }

        public virtual void ToggleVisible(bool value, Entity entity)
        {
            if (!isEnabled && value && entity != null)
            {
                CanvasGroup.alpha = 1;

                this.entity = entity;
                this.entity.healthUpdated += UpdateHealth;
                float fillAmount = entity.CurrentHealth / entity.Stats.MaxHealth;
                foregroundFill.fillAmount = fillAmount;
                backgroundFill.fillAmount = fillAmount;

                isEnabled = true;
            }
            else if (isEnabled && !value)
            {
                CanvasGroup.alpha = 0;
                
                if (entity != null)
                {
                    this.entity = entity;
                    this.entity.healthUpdated -= UpdateHealth;
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

        //public override void ToggleVisible(bool value, System.Action onComplete)
        //{              
        //    if (!CanvasGroup.enabled && value)
        //    {
        //        CanvasGroup.enabled = value;
        //        transform.localScale = Vector3.zero;
        //        LeanTween.scale(gameObject, Vector3.one, 0.4f).setOnComplete(() =>
        //        {
        //            onComplete?.Invoke();
        //        });
        //    }
        //    else if (CanvasGroup.enabled && !value)
        //    {
        //        LeanTween.scale(gameObject, Vector3.zero, 0.4f).setOnComplete(() =>
        //        {
        //            CanvasGroup.enabled = false;
        //            onComplete?.Invoke();
        //        });
        //    }
        //}
    }
}