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
        [SerializeField] Slider enemyCountSlider;
        [SerializeField] TextMeshProUGUI enemyCountText;

        void Start()
        {
            healthSlider.maxValue = player.Stats.MaxHealth;
            manaSlider.maxValue = player.Stats.MaxMana;
            player.healthUpdated += UpdateHealth;
            player.manaUpdated += UpdateMana;
            LevelManager.Instance.waveUpdated += UpdateEnemyCount;
        }

        void UpdateHealth(object sender, float amount)
        {
            healthSlider.value = player.GetCurrentHealth();
            healthText.text = player.GetCurrentHealth() + "/" + player.Stats.MaxHealth;
        }

        void UpdateMana(object sender, float amount)
        {
            manaSlider.value = player.GetCurrentMana();
            manaText.text = player.GetCurrentMana() + "/" + player.Stats.MaxMana;
        }

        void UpdateEnemyCount(object sender, Wave wave)
        {
            enemyCountSlider.maxValue = wave.MaxCount;
            enemyCountSlider.value = wave.CurrentCount;
            enemyCountText.text = wave.CurrentCount + "/" + wave.MaxCount;
        }
    }
}

