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
        healthBar = healthBar;
        Vector3 spawnPosition = new Vector3(5f, 0f, 0f);
        GameObject boss = fishBossPrefab;
        boss.GetComponent<BossHealth>().healthBar = healthBarScene;
        boss.GetComponent<BossHealth>().playerStats = playerScene;
        Instantiate(boss, spawnPosition, Quaternion.identity);
    }
}
