using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerProjectile : MonoBehaviour
{
    public Transform target;
    public float damage;
    public float speed;
    public float radius;

    void Update()
    {
        if (target != null)
        {
            transform.position += Vector3.Normalize(target.position - transform.position) * speed * Time.deltaTime;
            if (Vector3.Distance(transform.position, target.position) <= radius)
            {
                target.GetComponent<EnemyStats>().Damage(damage, ElementType.Physical);
                Destroy(this.gameObject);
            }
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
