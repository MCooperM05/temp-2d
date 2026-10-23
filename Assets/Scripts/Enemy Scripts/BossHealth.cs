using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealth : MonoBehaviour
{
    public float health = 20f;
    public Material damageMaterial;
    public Material defaultMaterial;

    public Slider healthBar;
    public GameObject keyPrefab;

    public PlayerStats playerStats;
    float maxHealth;

    public Sprite deadBoss;
    public GameObject bonePrefab;
    public int boneAmount;
    public float timeBetweenBones; // time between spawning bones

    void Awake()
    {
        maxHealth = health;
        Debug.Log("Enemy Health: " + health);
    }

    void Start()
    {
        if (healthBar != null)
        {
            healthBar.gameObject.SetActive(true);
            // healthBar.value = health / 10f;
            healthBar.maxValue = 1f;
            healthBar.value = 1f;
        }
    }

    public void TakeDamage(float damage)
    {
        if (health <= 0) return;

        health -= damage;
        // healthBar.value = health / 10f;
        healthBar.value = health / maxHealth;
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
        FindFirstObjectByType<BossManager>().BossDefeated();
        // GameObject key = Instantiate(keyPrefab, transform.position, Quaternion.identity);
        // key.GetComponent<KeyManager>().isBossKey = true;
       
        // DisableEnemy();
        PlayBossDeath();
        healthBar.value = health;
        healthBar.gameObject.SetActive(false);
    }

    void DisableEnemy()
    {
        GetComponent<BossAI>().enabled = false;
        GetComponent<BossMovement>().enabled = false;
        GetComponent<Collider2D>().enabled = false;
        GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
        // GetComponent<SpriteRenderer>().enabled = false;
    }

    public void PlayBossDeath()
    {
        DisableEnemy();
        GetComponent<SpriteRenderer>().sprite = deadBoss;
        SpawnBone(boneAmount);
    }

    public void SpawnBone(int amount)
    {
        StartCoroutine(SpawnBonesOverTime(amount));
    }

    IEnumerator SpawnBonesOverTime(int amount)
    {
        for (int i = 0; i < amount; ++i)
        {
            yield return new WaitForSeconds(timeBetweenBones);

            // bones will spawn in a random position relative to the boss's position
            Vector2 randomOffset = Random.insideUnitCircle * 1f;
            Vector3 spawnPosition = transform.position + (Vector3)randomOffset;

            Instantiate(bonePrefab, spawnPosition, Quaternion.identity);
            Debug.Log("Bone " + i + " spawned");
        }
        GetComponent<SpriteRenderer>().enabled = false; // disable renderer, appears like all bones have fallen

        // Spawn key after all bones have spawned
        GameObject key = Instantiate(keyPrefab, transform.position, Quaternion.identity);
        key.GetComponent<KeyManager>().isBossKey = true;
    }
}