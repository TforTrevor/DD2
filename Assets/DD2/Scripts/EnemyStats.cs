using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DD2;

public class EnemyStats : MonoBehaviour
{
    [SerializeField] float maxHealth;
    [SerializeField] float currentHealth;
    [SerializeField] float range;
    public ElementType elementType;
    public float aggro;
    public Transform target;

    public float GetMaxHealth()
    {
        return maxHealth;
    }

    public float GetCurrentHealth()
    {
        return currentHealth;
    }

    public void Damage(float amount, ElementType type)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }

    public float GetRange()
    {
        return range;
    }
}
