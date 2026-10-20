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

    [SerializeField] public float totalGold = 10f;

    [SerializeField] public TMP_Text playerGoldAmount;

    void Start()
    {
        UpdateTotalGold(totalGold);
        health = 3f;
        Debug.Log("Player Health: " + health);
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        haveLife[(int)health].SetActive(false);
        noLife[(int)health].SetActive(true);

        if (health <= 0f)
        {
            gameObject.SetActive(false);
        }

        StartCoroutine(FlashDamageMaterial());
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
        totalGold = amount;
        playerGoldAmount.text = totalGold.ToString();
    }
}
