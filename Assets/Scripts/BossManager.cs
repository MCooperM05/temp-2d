using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossManager : MonoBehaviour
{
    [SerializeField] public string sceneName = "";

    void Update()
    {
        GameObject Enemy = GameObject.FindGameObjectWithTag("Enemy");

        if (Enemy == null)
        {
            StartCoroutine(BackToScene());
        }
    }

    IEnumerator BackToScene()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(sceneName);
    }
}
