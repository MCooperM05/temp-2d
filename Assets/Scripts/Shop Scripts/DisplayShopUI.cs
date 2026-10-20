using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DisplayShopUI : MonoBehaviour
{
    [SerializeField] private GameObject shopUI;
    [SerializeField] private Animator shopAnimator;
    
    // On start, script checks which animator to use for the shop based on the scene name
    void Start()
    {
        Scene scene = SceneManager.GetActiveScene();
        if (scene.name == "WorldHub")
        {
            shopAnimator.SetBool("IsOnLand", true);
        }
        else
        {
            shopAnimator.SetBool("IsOnLand", false);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            shopUI.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            shopUI.SetActive(false);
        }
    }
}
