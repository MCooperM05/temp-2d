using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject projectilePrefab;

    [Header("Combat Settings")]
    [SerializeField] private float attackCooldown = 1f;
    private bool isOnAttackCooldown;
    [SerializeField] private float speed = 10f;
    [SerializeField] public static float projectileDamage = 1f;

    private GameObject playerObj;
    private Rigidbody2D projectileRb;

    void Awake()
    {
        if (playerTransform == null)
        {
            GameObject playerObj = GameObject.Find("Player");
            if (playerObj != null)
            {
                playerTransform = playerObj.transform;
            }
        }
    }

    void FireProjectile()
    {
        if (projectilePrefab == null || firePoint == null) return;
        
        firePoint.LookAt(playerTransform.position);
        Debug.Log(playerTransform.position);

        projectileRb = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity).GetComponent<Rigidbody2D>();
        // projectileRb.linearVelocity = new Vector2(speed, speed);
        Vector2 direction = (playerTransform.position - firePoint.position).normalized;
        projectileRb.linearVelocity = direction * speed;

        Destroy(projectileRb.gameObject, 3f);
    }

    private IEnumerator AttackCooldownRoutine()
    {
        isOnAttackCooldown = true;
        yield return new WaitForSeconds(attackCooldown);
        isOnAttackCooldown = false;
    }

    void PerformAttack()
    {
        if (!isOnAttackCooldown)
        {
            FireProjectile();
            StartCoroutine(AttackCooldownRoutine());
        }
    }

    void Update()
    {
        if (playerTransform == null || playerTransform != null)
        {
            GameObject playerObj = GameObject.Find("Player");
            if (playerObj != null)
            {
                playerTransform = playerObj.transform;
            }
        }

        PerformAttack();
    }
}