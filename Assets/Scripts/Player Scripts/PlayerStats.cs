using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] public float health = 3f;
    [SerializeField] public float maxHealth = 3f;
    [SerializeField] public HealthUI healthUI;
    
    [SerializeField] public Material damageMaterial;
    [SerializeField] public Material defaultMaterial;

    static public float totalGold = 0f;
    static public int totalKeys = 0;
    public static float savedHealth = 3f;
    public static float savedMaxHealth = 3f;

    [SerializeField] public TMP_Text playerGoldAmount;

    [SerializeField] public TMP_Text playerKeyAmount;
    [SerializeField] public GameObject keysVisual;

    [SerializeField] public GameObject deathScreen;
    [SerializeField] public GameObject pauseScreen;
    void Awake()
    {
        if(gameObject.scene.name == "WorldHub") savedHealth = savedMaxHealth; 

        maxHealth = savedMaxHealth;
        health = Mathf.Min(savedHealth, maxHealth);
    }

    void Start()
    {
        UpdateTotalGold(0f);
        UpdateTotalKeys(0);
       
        if (gameObject.scene.name == "WorldHub") // reset keys so the player cannot save up keys
        {
            totalKeys = 0;
            keysVisual.SetActive(false);
        }

        deathScreen.SetActive(false);
        pauseScreen.SetActive(false);
        Debug.Log("Player Health: " + health);
        Debug.Log("PH&G on:" + gameObject.name + "in scene: " + gameObject.scene.name, gameObject);
    }

    public void TakeDamage(float damage)
    {
        if (health <= 0f) return;

        health -= damage;

        if (health < 0f) health = 0f;
        savedHealth = health;

        healthUI.UpdateHearts();

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

    public void IncreaseHealth(float amount)
    {
        maxHealth += amount;
        health = maxHealth;

        savedMaxHealth = maxHealth;
        savedHealth = health;

        healthUI.AddHearts();
    }

    public void UpdateTotalGold(float amount)
    {
        totalGold += amount;
        playerGoldAmount.text = totalGold.ToString();
    }

    public void UpdateTotalKeys(int amount)
    {
        totalKeys += amount;
        playerKeyAmount.text = totalKeys.ToString();
    }
}
