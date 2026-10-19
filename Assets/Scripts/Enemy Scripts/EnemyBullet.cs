using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    private float damage = 0f;

    void Awake()
    {
        damage = EnemyAI.projectileDamage;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Debug.Log("Bullet hit");
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerHealthAndGold>().TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
