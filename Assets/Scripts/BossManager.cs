using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossManager : MonoBehaviour
{
    [SerializeField] public string mainMenuSceneName = "";

    void Update()
    {
        GameObject Enemy = GameObject.FindGameObjectWithTag("Enemy");

        if (Enemy == null)
        {
            StartCoroutine(BackToMainMenu());
        }
    }

    IEnumerator BackToMainMenu()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(mainMenuSceneName);
    }
}
