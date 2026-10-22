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

    [SerializeField] private AudioSource audioSource;

    // [SerializeField] public Slider healthBar;
    [SerializeField] public GameObject goldPrefab;
    [SerializeField] public GameObject bubbleObj;
    [SerializeField] public Animator anim;

    [SerializeField] public PlayerStats playerStats;

    void Awake()
    {
        // healthBar.gameObject.SetActive(true);
        // healthBar.value = health / 10f;
        Debug.Log("Enemy Health: " + health);
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        // healthBar.value = health / 10f;

        if (health <= 0f)
        {
            DisableEnemy();
            StartCoroutine(EnemyDeath());
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
        gold.GetComponent<GoldManager>().FadeGold();
        
        playerStats.UpdateTotalGold(goldAmount);

        // healthBar.gameObject.SetActive(false);

        Destroy(gameObject);
    }

    void DisableEnemy()
    {
        // GetComponent<EnemyAI>().enabled = false;
        // GetComponent<BossAI>().enabled = false;
        GetComponent<Collider2D>().enabled = false;
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<EnemyMovement>().enabled = false;
        GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
    }

    IEnumerator EnemyDeath() // after death, play bubble popping and give gold to player
    {
        bubbleObj.SetActive(true);

        anim.SetBool("isDead", true);
        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length);

        bubbleObj.SetActive(false);

        GiveGold();
    }

    public void PlayPop()
    {
        audioSource.Play();
    }
}
