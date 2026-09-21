using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GunManager : MonoBehaviour
{
    [Header("Gun Settings")]
    [SerializeField] public PlayerInputHandler playerInputHandler;
    [SerializeField] public GameObject gunPrefab;
    [SerializeField] public Transform firePoint;
    [SerializeField] public float damage = 1f;
    [SerializeField] public float speed = 100f;
    [SerializeField] public float cooldown = 0.5f;

    void Update()
    {
        if (playerInputHandler.ShootTriggered)
        {
            Debug.Log("Shot fired");
            FireGun();
            playerInputHandler.shootAction.Disable();
            StartCoroutine(ShootCooldown());
        }
    }

    IEnumerator ShootCooldown()
    {
        yield return new WaitForSeconds(cooldown);
        playerInputHandler.shootAction.Enable();
    }

    private void FireGun()
    {
        if (gunPrefab == null || firePoint == null) return;

        Rigidbody2D gunRb = Instantiate(gunPrefab, firePoint.position, Quaternion.identity).GetComponent<Rigidbody2D>();
        gunRb.linearVelocity = new Vector2(speed, 0);
        gunRb.GetComponent<Bullet>().SetDamage(damage);

        Destroy(gunRb.gameObject, 0.5f);
    }
}
