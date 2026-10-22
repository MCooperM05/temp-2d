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

    void Update()
    {
        GameObject boss = GameObject.FindGameObjectWithTag("Boss");
        keys = PlayerStats.totalKeys;
        if (boss == null && keys != 1)
        {
            PlayerStats player = FindFirstObjectByType<PlayerStats>();
            GameObject sliderObj = GameObject.FindGameObjectWithTag("healthBar");
            if (sliderObj == null){
                Debug.Log("sliderObj is null");
            } 
            Slider hpBar = sliderObj.GetComponent<Slider>();
            PrefabController newInstance = Instantiate(prefabToSpawn);
            newInstance.Initialize(player, hpBar);
        }
        if (boss.GetComponent <BossHealth>().health == 0)
        {
            for (int i = 0; i < diveStations.Length; i++)
        {
            diveStations[i].SetActive(true);
        }
        }
    }

    // IEnumerator BackToScene()
    // {
    //     yield return new WaitForSeconds(3f);
    //     SceneManager.LoadScene(sceneName);
    // }
}
