using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DD2.Util;

namespace DD2.UI
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
        [SerializeField] TextMeshProUGUI waveCountText;
        [SerializeField] WaveInfo waveInfo;

        Canvas canvas;

        void Awake()
        {
            canvas = GetComponent<Canvas>();
        }

        void Start()
        {
            healthSlider.maxValue = player.Stats.MaxHealth;
            manaSlider.maxValue = player.Stats.MaxMana;
            player.healthUpdated += UpdateHealth;
            player.manaUpdated += UpdateMana;
            enemyCountSlider.gameObject.SetActive(false);
            UpdateWaveCount();
        }

        public void Show()
        {
            canvas.enabled = true;
        }

        public void Hide()
        {
            canvas.enabled = false;
        }

        void UpdateHealth(object sender, float amount)
        {
            healthSlider.value = player.CurrentHealth;
            healthText.text = player.CurrentHealth + "/" + player.Stats.MaxHealth;
        }

        void UpdateMana(object sender, float amount)
        {
            manaSlider.value = player.CurrentMana;
            manaText.text = player.CurrentMana + "/" + player.Stats.MaxMana;
        }

        public void ToggleEnemyCount(bool value)
        {
            enemyCountSlider.gameObject.SetActive(value);
        }

        public void UpdateEnemyCount()
        {
            enemyCountSlider.maxValue = waveInfo.TotalEnemyCount;
            enemyCountSlider.value = waveInfo.CurrentEnemyCount;
            enemyCountText.text = waveInfo.CurrentEnemyCount + "/" + waveInfo.TotalEnemyCount;
        }

        public void UpdateWaveCount()
        {
            if (waveInfo.TotalWaves > 0)
            {
                waveCountText.text = waveInfo.WaveIndex + 1 + "/" + waveInfo.TotalWaves;
            }
            else
            {
                waveCountText.text = "";
            }
        }
    }
}

