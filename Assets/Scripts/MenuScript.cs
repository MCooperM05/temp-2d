using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    void Start()
    {
        Debug.Log("Start Game");
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void LoadScene(string sceneName)
    {
        Debug.Log("Load Scene");
        SceneManager.LoadScene(sceneName);
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }
}
