using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.InputSystem;

public class IntroComicScript : MonoBehaviour
{
    public string nextScene = "WorldHub"; // name of scene to load
    
    void Update()
    {
        if (Keyboard.current.enterKey.wasPressedThisFrame)
        {
            SceneManager.LoadScene(nextScene);
        }
        
    }
}
