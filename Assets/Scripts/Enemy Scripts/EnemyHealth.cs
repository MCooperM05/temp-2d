using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] public static float health = 10f;
    [SerializeField] public Material damageMaterial;
    [SerializeField] public Material defaultMaterial;
    [SerializeField] public float goldAmount = 10f;

    [SerializeField] public Slider healthBar;
    [SerializeField] public GameObject goldPrefab;

    [SerializeField] public PlayerHealthAndGold playerHealthGold;

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

        if (health <= 0f)
        {
            GiveGold();
        }

        StartCoroutine(FlashDamageMaterial());
    }

    IEnumerator FlashDamageMaterial()
    {
        GetComponent<Renderer>().material = damageMaterial;
        yield return new WaitForSeconds(0.2f);
        GetComponent<Renderer>().material = defaultMaterial;
    }

    void GiveGold()
    {
        GameObject gold = Instantiate(goldPrefab, transform.position, Quaternion.identity);
        gold.GetComponent<GoldManager>().SetGoldAmount(goldAmount);
        DisableEnemy();
        gold.GetComponent<GoldManager>().FadeGold();
        
        playerHealthGold.UpdateTotalGold(goldAmount);

        healthBar.gameObject.SetActive(false);

        Destroy(gameObject);
    }

    void DisableEnemy()
    {
        GetComponent<EnemyAI>().enabled = false;
        GetComponent<Collider2D>().enabled = false;
        GetComponent<SpriteRenderer>().enabled = false;
    }
}
