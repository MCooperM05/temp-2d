using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] public float health = 3f;
    
    [SerializeField] public GameObject[] haveLife;
    [SerializeField] public GameObject[] noLife;
    
    [SerializeField] public Material damageMaterial;
    [SerializeField] public Material defaultMaterial;

    [SerializeField] public static float totalGold = 0f;

    [SerializeField] public TMP_Text playerGoldAmount;

    [SerializeField] public GameObject deathScreen;

    void Start()
    {
        UpdateTotalGold(0f);
        health = 3f;
        deathScreen.SetActive(false);
        Debug.Log("Player Health: " + health);
    }

    public void TakeDamage(float damage)
    {
        if (health <= 0f) return;

        health -= damage;

        if (health < 0f) health = 0f;

        haveLife[(int)health].SetActive(false);
        noLife[(int)health].SetActive(true);

        if (health <= 0f) {
            Die();
            return;
        }

        StartCoroutine(FlashDamageMaterial());
    }

    void Die() {
        gameObject.SetActive(false);
        deathScreen.SetActive(true);
        Time.timeScale = 0f;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    IEnumerator FlashDamageMaterial()
    {
        GetComponent<Renderer>().material = damageMaterial;
        yield return new WaitForSeconds(0.2f);
        GetComponent<Renderer>().material = defaultMaterial;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            TakeDamage(1f);
        }
    }

    public void UpdateTotalGold(float amount)
    {
        totalGold += amount;
        playerGoldAmount.text = totalGold.ToString();
    }
}
