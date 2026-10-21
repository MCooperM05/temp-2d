using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] public PlayerInputHandler playerInputHandler;
    [SerializeField] public GameObject pauseScreen;

    private bool paused = false;

    void Update()
    {
        if (playerInputHandler.menuAction.WasPressedThisFrame())
        {
            Debug.Log("Pause Menu");
            Pause();
        }
    }

    public void Pause()
    {
        if (PlayerStats.savedHealth <= 0) return;
        if (paused)
        {
            // gameObject.SetActive(true);
            pauseScreen.SetActive(false);
            Time.timeScale = 1f;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            paused = false;
        }
        else
        {
            // gameObject.SetActive(false);
            pauseScreen.SetActive(true);
            Time.timeScale = 0f;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            paused = true;
        }
    }

    public void QuitGame() {
        Debug.Log("Quit Game");
        Application.Quit();
    }
}
