using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DD2;

public class TowerStats : MonoBehaviour
{
    [SerializeField] float maxHealth;
    [SerializeField] float currentHealth;
    public ElementType elementType;

    public float range;
    public float radius;
    public float damage;
    public float attackSpeed;

    public GameObject projectile;
    [SerializeField] Transform centerTransform;
    [SerializeField] Transform fireTransform;

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

    public Vector3 GetPosition()
    {
        return centerTransform.position;
    }

    public Vector3 GetForward()
    {
        return centerTransform.forward;
    }

    public Vector3 GetFirePosition()
    {
        return fireTransform.position;
    }
}
