using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace DD2
{
    public class PlayerUI : MonoBehaviour
    {
        [SerializeField] Player player;
        [SerializeField] Slider healthSlider;
        [SerializeField] TextMeshProUGUI healthText;
        [SerializeField] Slider manaSlider;
        [SerializeField] TextMeshProUGUI manaText;

        void Awake()
        {
            healthSlider.maxValue = player.GetStats().GetMaxHealth();
            manaSlider.maxValue = player.GetStats().GetMaxMana();
            player.healthUpdated += UpdateHealth;
            player.manaUpdated += UpdateMana;
        }

        void UpdateHealth(float amount)
        {
            healthSlider.value = player.GetCurrentHealth();
            healthText.text = player.GetCurrentHealth() + "/" + player.GetStats().GetMaxHealth();
        }

        void UpdateMana(float amount)
        {
            manaSlider.value = player.GetCurrentMana();
            manaText.text = player.GetCurrentMana() + "/" + player.GetStats().GetMaxMana();
        }
    }
}

