using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScreen : MonoBehaviour
{
    public void Respawn() {
        Debug.Log("Respawn");
        Time.timeScale = 1f; //unfreeze game
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); //reload current scene
    }

    public void QuitGame() {
        Debug.Log("Quit Game");
        Application.Quit();
    }
}
