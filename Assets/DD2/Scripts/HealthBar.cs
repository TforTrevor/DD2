using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace DD2
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] Slider healthSlider;
        [SerializeField] TextMeshProUGUI healthNumber;
        [SerializeField] Canvas canvas;
        [SerializeField] float heightOffset = 2;

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
            healthSlider.maxValue = this.entity.GetStats().GetMaxHealth();
            UpdateHealth(0);
        }

        void UpdateHealth(float amount)
        {
            healthSlider.value = entity.GetCurrentHealth();
            healthNumber.text = entity.GetCurrentHealth() + "/" + entity.GetStats().GetMaxHealth();
        }

        void OnDestroy()
        {
            if (entity != null)
            {
                entity.healthUpdated -= UpdateHealth;
            }            
        }

        public float GetHeightOffset()
        {
            return heightOffset;
        }

        public Canvas GetCanvas()
        {
            return canvas;
        }
    }
}