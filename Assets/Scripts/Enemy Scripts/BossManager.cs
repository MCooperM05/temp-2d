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
    [SerializeField] public TextMeshProUGUI bossDialogue; 

    public GameObject[] diveStations;

    public int keys;

    bool bossDefeated = false;
    public static bool bossBeaten = false;

    [Header("Keys")]
    public static bool keyCollected = false;
    public GameObject keyPrefab;
    public Transform keySpawnPoint;
    private bool spawn = false;

    void Start() {
        if (bossBeaten)
        {
            BossDefeated();

            if (!keyCollected && keyPrefab != null)
            {
                Vector3 spot = transform.position;
                if (keySpawnPoint != null) spot = keySpawnPoint.position;
                GameObject key = Instantiate(keyPrefab, spot, Quaternion.identity);
                key.GetComponent<KeyManager>().isBossKey = true;
            }
        }
    }

    void Update()
    {
        if (bossDefeated) return;

        GameObject boss = GameObject.FindGameObjectWithTag("Boss");

        if (boss == null && spawn == false)
        {
            StartCoroutine(BossCutscene());
            spawn = true;
            return;
        }
        if (boss != null)
        {
            if (boss.GetComponent<BossHealth>().health <= 0)
            {
               BossDefeated();
            }           
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

        bossBeaten = true;
        GameObject bar = GameObject.FindGameObjectWithTag("healthBar");
        if (bar != null) bar.SetActive(false);
        Debug.Log("Boss Defeated");
    }

    IEnumerator BossCutscene() {
        SpawnBoss();
        GameObject boss = GameObject.FindGameObjectWithTag("Boss");
        boss.GetComponent<BossHealth>().DisableEnemy();
        boss.GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(3f);
        bossDialogue.enabled = false;
        boss.GetComponent<BossHealth>().EnableEnemy();
    }

    // IEnumerator BackToScene()
    // {
    //     yield return new WaitForSeconds(3f);
    //     SceneManager.LoadScene(sceneName);
    // }
}
