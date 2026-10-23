using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Collections;
using System.Collections.Generic;

public class CreditManager : MonoBehaviour
{
    public string sceneToLoad = "MainMenu";
    void Start()
    {
        StartCoroutine(LeaveCreditsForMenu());
    }

    IEnumerator LeaveCreditsForMenu()
    {
        yield return new WaitForSeconds(15f);
        SceneManager.LoadScene(sceneToLoad);

    }
}
