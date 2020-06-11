using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace DD2.UI
{
    public class HealthBar : FloatingUI
    {
        [SerializeField] Slider healthSlider;
        [SerializeField] TextMeshProUGUI healthNumber;

        Entity entity;

        void Awake()
        {
            healthSlider.minValue = 0;
            healthSlider.maxValue = 100; 
        }

        public void SetEntity(Entity entity)
        {
            this.entity = entity;
            this.entity.healthUpdated += UpdateHealth;
            healthSlider.maxValue = this.entity.Stats.MaxHealth;
            UpdateHealth(null, 0);
        }

        void UpdateHealth(object sender, float amount)
        {
            healthSlider.value = entity.CurrentHealth;
            healthNumber.text = (int)entity.CurrentHealth + "/" + entity.Stats.MaxHealth;
        }

        void OnDestroy()
        {
            if (entity != null)
            {
                entity.healthUpdated -= UpdateHealth;
            }            
        }
    }
}