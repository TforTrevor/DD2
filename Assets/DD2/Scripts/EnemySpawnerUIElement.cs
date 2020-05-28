using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace DD2
{
    public class EnemySpawnerUIElement : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI enemyName;
        [SerializeField] TextMeshProUGUI enemyCount;

        public void SetEnemy(string name, int count)
        {
            enemyName.text = name;
            enemyCount.text = count.ToString();
        }
    }
}