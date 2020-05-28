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
        [SerializeField] TextMeshProUGUI waveCountText;

        void Start()
        {
            healthSlider.maxValue = player.Stats.MaxHealth;
            manaSlider.maxValue = player.Stats.MaxMana;
            player.healthUpdated += UpdateHealth;
            player.manaUpdated += UpdateMana;
            LevelManager.Instance.waveUpdated += UpdateEnemyCount;
            LevelManager.Instance.waveStarted += UpdateWaveCount;
            LevelManager.Instance.waveEnded += UpdateWaveCount;
            LevelManager.Instance.waveStarted += (sender, amount) =>
            {
                enemyCountSlider.gameObject.SetActive(true);
            };
            LevelManager.Instance.waveEnded += (sender, amount) =>
            {
                enemyCountSlider.gameObject.SetActive(false);
            };
            enemyCountSlider.gameObject.SetActive(false);
            UpdateWaveCount(this, LevelManager.Instance.CurrentWave);
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

        void UpdateEnemyCount(object sender, Wave wave)
        {
            enemyCountSlider.maxValue = wave.MaxCount;
            enemyCountSlider.value = wave.CurrentCount;
            enemyCountText.text = wave.CurrentCount + "/" + wave.MaxCount;
        }

        void UpdateWaveCount(object sender, int count)
        {
            waveCountText.text = count + 1 + "/" + LevelManager.Instance.WaveCount;
        }
    }
}

