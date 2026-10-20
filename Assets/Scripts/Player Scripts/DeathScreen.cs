using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScreen : MonoBehaviour
{
    public void Respawn() {
        Debug.Log("Respawn");
        PlayerHealthAndGold.savedHealth = 3f;
        Time.timeScale = 1f; //unfreeze game
        SceneManager.LoadScene("WorldHub"); //reload current scene
    }

    public void QuitGame() {
        Debug.Log("Quit Game");
        Application.Quit();
    }
}
