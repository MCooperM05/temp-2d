using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealth : MonoBehaviour
{
    [SerializeField] public static float health = 20f;
    [SerializeField] public Material damageMaterial;
    [SerializeField] public Material defaultMaterial;

    [SerializeField] public Slider healthBar;
    [SerializeField] public GameObject keyPrefab;

    [SerializeField] public PlayerStats playerStats;

    void Awake()
    {
        healthBar.gameObject.SetActive(true);
        healthBar.value = health / 10f;
        Debug.Log("Enemy Health: " + health);
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        healthBar.value = health / 10f;
        Debug.Log("Enemy Health: " + health);
        if (health <= 0f)
        {
            GiveKey();
        }

        StartCoroutine(FlashDamageMaterial());
    }

    IEnumerator FlashDamageMaterial()
    {
        GetComponent<Renderer>().material = damageMaterial;
        yield return new WaitForSeconds(0.2f);
        GetComponent<Renderer>().material = defaultMaterial;
    }

    void GiveKey()
    {
        Instantiate(keyPrefab, transform.position, Quaternion.identity);
       
        DisableEnemy();
        healthBar.value = health;
        healthBar.gameObject.SetActive(false);
    }

    void DisableEnemy()
    {
        GetComponent<BossAI>().enabled = false;
        GetComponent<BossMovement>().enabled = false;
        GetComponent<Collider2D>().enabled = false;
        GetComponent<SpriteRenderer>().enabled = false;
    }
}