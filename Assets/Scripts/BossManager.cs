using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossManager : MonoBehaviour
{
    [SerializeField] public string sceneName = "prototype-explore-ocean";

    void Update()
    {
        GameObject Boss = GameObject.FindGameObjectWithTag("Boss");

        if (Boss == null)
        {
            StartCoroutine(BackToScene());
        }
    }

    IEnumerator BackToScene()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(sceneName);
    }
}
