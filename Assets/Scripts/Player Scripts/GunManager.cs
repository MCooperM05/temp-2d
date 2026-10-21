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
    public static float bonusDamage = 0f;
    [SerializeField] public float speed = 100f;
    [SerializeField] public float cooldown = 0.5f;
    public static float coolChange = 0f;
    [SerializeField] private AudioSource gunShot;

    [Header("Trident Upgrade")]
    [SerializeField] public Sprite tridentSprite;
    [SerializeField] public float tridentRotation = -90f;
    [SerializeField] public float tridentScale = 10f;

    void Update()
    {
        if (Time.timeScale == 0f) return;
        if (playerInputHandler.ShootTriggered)
        {
            Debug.Log("Shot fired");
            gunShot.Play();
            FireGun();
            playerInputHandler.shootAction.Disable();
            StartCoroutine(ShootCooldown());
        }
    }

    IEnumerator ShootCooldown()
    {
        yield return new WaitForSeconds(cooldown - coolChange);
        playerInputHandler.shootAction.Enable();
    }

    private void FireGun()
    {
        if (gunPrefab == null || firePoint == null) return;

        bool facingLeft = firePoint.GetComponentInParent<SpriteRenderer>().flipX;
        float direction = 1f;
        if (facingLeft) direction = -1f;

        Vector3 shotPos = firePoint.position;
        if (facingLeft)
        {
            float offset = shotPos.x - firePoint.parent.position.x;
            shotPos.x = firePoint.parent.position.x - offset;
        }

        GameObject bullet = Instantiate(gunPrefab, shotPos, Quaternion.identity);
        Rigidbody2D gunRb = bullet.GetComponent<Rigidbody2D>();
        SpriteRenderer bulletRender = bullet.GetComponent<SpriteRenderer>();
        gunRb.linearVelocity = new Vector2(speed * direction, 0);
        gunRb.GetComponent<Bullet>().SetDamage(damage + bonusDamage);

        bulletRender.flipX = facingLeft;

        if (bonusDamage > 0 && tridentSprite != null)
        {
            bulletRender.sprite = tridentSprite;
            bullet.transform.localScale *= tridentScale;

            float angle = tridentRotation;
            if (facingLeft)
            {
                angle += 180;
            }
            bullet.transform.rotation = Quaternion.Euler(0, 0, angle);
            // gunRb.GetComponent<SpriteRenderer>().sprite = tridentSprite;
        } 
        // else
        // {
        //     bulletRender.flipX = facingLeft;
        // }

        Destroy(bullet, 0.5f);
    }
}
