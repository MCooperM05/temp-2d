using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PrefabController : MonoBehaviour
{
    private PlayerStats player;
    private Slider healthBar;
    [SerializeField] public GameObject fishBossPrefab;


    public void Initialize(PlayerStats playerScene, Slider healthBarScene)
    {
        player = playerScene;
        healthBar = healthBarScene;
        Vector3 spawnPosition = new Vector3(5f, 0f, 0f);
        GameObject boss = Instantiate(fishBossPrefab, spawnPosition, Quaternion.identity);
        BossHealth bossHealth = boss.GetComponent<BossHealth>();
        if (bossHealth != null) {
            
            bossHealth.healthBar = healthBarScene;
            bossHealth.playerStats = player;
            
            if (healthBarScene != null) {
                healthBarScene.gameObject.SetActive(true);
                healthBarScene.value = 20f / 10f;
            }
        }
        else {
            Debug.Log("BossHealth component not found on boss prefab.");
        }
    }
}
