using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class BossManager : MonoBehaviour
{
    [SerializeField] public PrefabController prefabToSpawn;

    
    [SerializeField] public string sceneName = "prototype-explore-ocean";
    
    [SerializeField] public GameObject fishBossPrefab;
    [SerializeField] public PlayerStats playerStats;
    public GameObject[] diveStations;

    public int keys;

    bool bossDefeated = false;

    void Update()
    {
        if (bossDefeated) return;

        GameObject boss = GameObject.FindGameObjectWithTag("Boss");
        keys = PlayerStats.totalKeys;

        if (boss == null && keys != 1)
        {
            SpawnBoss();
            return;
        }
        if (boss != null && boss.GetComponent<BossHealth>().health <= 0)
        {
            BossDefeated();
        }
    }

    void SpawnBoss()
    {
        PlayerStats player = FindFirstObjectByType<PlayerStats>();
        GameObject sliderObj = GameObject.FindGameObjectWithTag("healthBar");
        if (sliderObj == null){
            Debug.Log("sliderObj is null");
            return;
        }
        Slider hpBar = sliderObj.GetComponent<Slider>();
        PrefabController newInstance = Instantiate(prefabToSpawn);
        newInstance.Initialize(player, hpBar);
    }

    public void BossDefeated()
    {
        if (bossDefeated) return;
        bossDefeated = true;

        for (int i = 0; i < diveStations.Length; i++)
        {
            diveStations[i].SetActive(true);
        }

        Debug.Log("Boss Defeated");
    }

    // IEnumerator BackToScene()
    // {
    //     yield return new WaitForSeconds(3f);
    //     SceneManager.LoadScene(sceneName);
    // }
}
