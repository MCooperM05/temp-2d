using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHealthAndGold : MonoBehaviour
{
    [SerializeField] public float health = 3f;
    
    [SerializeField] public GameObject[] haveLife;
    [SerializeField] public GameObject[] noLife;
    
    [SerializeField] public Material damageMaterial;
    [SerializeField] public Material defaultMaterial;

    static public float totalGold = 0f;
    public static float savedHealth = 3f;

    [SerializeField] public TMP_Text playerGoldAmount;

    [SerializeField] public GameObject deathScreen;

    void Start()
    {
        UpdateTotalGold(0f);
        if(gameObject.scene.name == "WorldHub") savedHealth = 3f; 
        health = savedHealth;

        for (int i = 0; i < haveLife.Length; i++)
        {
            haveLife[i].SetActive(i < health);
            noLife[i].SetActive(i >= health);
        }

        deathScreen.SetActive(false);
        Debug.Log("Player Health: " + health);
        Debug.Log("PH&G on:" + gameObject.name + "in scene: " + gameObject.scene.name, gameObject);
    }

    public void TakeDamage(float damage)
    {
        if (health <= 0f) return;

        health -= damage;

        if (health < 0f) health = 0f;
        savedHealth = health;

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
        UpdateTotalGold(-Mathf.Floor(totalGold / 2f));
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
