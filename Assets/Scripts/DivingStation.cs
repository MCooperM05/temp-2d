using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using System.Runtime.CompilerServices;

public class DivingStation : MonoBehaviour
{
    public string sceneToLoad = "prototype-explore-ocean";
    public GameObject pressEPrompt;

    bool playerNearby = false;

    void Start()
    {
        if (pressEPrompt != null)
        {
            pressEPrompt.SetActive(false);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = true;
            if (pressEPrompt != null)
            {
                pressEPrompt.SetActive(true);
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = false;
            if (pressEPrompt != null)
            {
                pressEPrompt.SetActive(false);
            }
        }
    }

    void Update()
    {
        if (playerNearby && Keyboard.current.eKey.wasPressedThisFrame)
        {
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
