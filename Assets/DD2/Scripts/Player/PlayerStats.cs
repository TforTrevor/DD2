using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SmartData.SmartFloat;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] float maxHealth;
    [SerializeField] float currentHealth;
    [SerializeField] float buildSpeed;
    [SerializeField] float buildRange;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void Damage(float amount, ElementType type)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    public float GetBuildRange()
    {
        return buildRange;
    }

    public float GetBuildSpeed()
    {
        return buildSpeed;
    }
}
