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

    public int keys;

    void Update()
    {
        GameObject Boss = GameObject.FindGameObjectWithTag("Boss");
        keys = PlayerStats.totalKeys;
        if (Boss == null && keys != 1)
        {
            PlayerStats player = FindFirstObjectByType<PlayerStats>();
            Slider hpBar = FindFirstObjectByType<Slider>();
            PrefabController newInstance = Instantiate(prefabToSpawn);
            newInstance.Initialize(player, hpBar);
        }
    }

    IEnumerator BackToScene()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(sceneName);
    }
}
